namespace OpiSupport.Application.DTOs
{
    public class LoginRequestDto
    {
        public string Identifier { get; set; } = string.Empty; // Puede ser EmployeeCode o Email
        public string Password { get; set; } = string.Empty;
    }
}

