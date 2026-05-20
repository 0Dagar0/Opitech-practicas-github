using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string email, string password);
    }
}

