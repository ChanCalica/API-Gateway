using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaApiUtilities.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Profile
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("middleName")]
        public string MiddleName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }
        [JsonProperty("dob")]
        public string DOB { get; set; }
        [JsonProperty("grCardNumPrim")]
        public long GRCardNumPrim { get; set; }
        [JsonProperty("primaryPhone")]
        public string PrimaryPhone { get; set; }
    }

    public class Password
    {
    }

    public class RecoveryQuestion
    {
        [JsonProperty("question")]
        public string Question { get; set; }
    }

    public class Provider
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Credentials
    {
        [JsonProperty("password")]
        public Password Password { get; set; }
        [JsonProperty("recovery_question")]
        public RecoveryQuestion RecoveryQuestion { get; set; }
        [JsonProperty("provider")]
        public Provider Provider { get; set; }
    }

    public class ResetPassword
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class ResetFactors
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class ExpirePassword
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class ForgotPassword
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class ChangeRecoveryQuestion
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class Deactivate
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class ChangePassword
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class Activate
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class Links
    {
        [JsonProperty("resetPassword")]
        public ResetPassword ResetPassword { get; set; }
        [JsonProperty("resetFactors")]
        public ResetFactors ResetFactors { get; set; }
        [JsonProperty("expirePassword")]
        public ExpirePassword ExpirePassword { get; set; }
        [JsonProperty("forgotPassword")]
        public ForgotPassword ForgotPassword { get; set; }
        [JsonProperty("changeRecoveryQuestion")]
        public ChangeRecoveryQuestion ChangeRecoveryQuestion { get; set; }
        [JsonProperty("deactivate")]
        public Deactivate Deactivate { get; set; }
        [JsonProperty("activate")]
        public Deactivate Activate { get; set; }
        [JsonProperty("changePassword")]
        public ChangePassword ChangePassword { get; set; }
        [JsonProperty("self")]
        public ChangePassword Self { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("activated")]
        public string Activated { get; set; }
        [JsonProperty("statusChanged")]
        public string StatusChanged { get; set; }
        [JsonProperty("lastLogin")]
        public string LastLogin { get; set; }
        [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }
        [JsonProperty("passwordChanged")]
        public string PasswordChanged { get; set; }
        [JsonProperty("profile")]
        public Profile Profile { get; set; }
        [JsonProperty("credentials")]
        public Credentials Credentials { get; set; }
        [JsonProperty("_links")]
        public Links _Links { get; set; }
    }


}
