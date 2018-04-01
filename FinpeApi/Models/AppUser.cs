using Microsoft.AspNetCore.Identity;

namespace FinpeApi.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string PhotoUrl { get; set; }
        public string FacebookId { get; set; }

        public AppUser()
        {
        }

        public AppUser(string userName) : base(userName)
        {
        }
    }
}
