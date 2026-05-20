using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
                        //  Buscar usuario por email
            var users = await _userRepository.FindAsync(u => u.Email == email);
            var user = users.FirstOrDefault();
            if (user == null)
                return null; // Usuario no encontrado

                        // Verificar contraseña (por ahora en texto plano; en producción se usa hash)
            if (user.PasswordHash != password)
                return null; // Contraseña incorrecta

                    // Crear los claims del token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()) // "Admin" o "Reader"
            };

                        //  Leer configuración JWT
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Crear el token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // El token expira en 2 horas
                signingCredentials: creds
            );

                    // Devolver el token como string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}