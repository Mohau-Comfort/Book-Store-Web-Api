using BookStore.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Api.Repositry
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
    }
}