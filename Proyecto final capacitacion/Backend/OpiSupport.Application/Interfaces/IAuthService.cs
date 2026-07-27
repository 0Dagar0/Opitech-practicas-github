using OpiSupport.Application.DTOs;

namespace OpiSupport.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    }
}

