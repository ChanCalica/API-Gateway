using OktaApiUtilities.Models;
using OktaApiUtilities.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaUtilities.Service.Interface
{
    public interface IOktaClientService
    {
        Task<User> GetUser(string userId);
        Task<List<User>?> SearchUser(string? mobileNumber, string? firstName, string? lastName, string? email);
        Task<List<User>?> SearchUser(string? mobileNumber);
        Task<List<User>?> SearchUserUsingGRCardNumber(string? grCardNumber);
        Task<User> UpdateUser(string userId, UpdateUserRequest request);
    }
}
