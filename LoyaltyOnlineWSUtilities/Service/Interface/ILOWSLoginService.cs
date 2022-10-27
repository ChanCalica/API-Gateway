using LoyaltyOnlineWSLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyOnlineWSUtilities.Service.Interface
{
    public interface ILOWSLoginService
    {
        Task<UserLoginResponse> Login(string username, string password);
        Task<string> Login();
        Task Logout();

    }
}
