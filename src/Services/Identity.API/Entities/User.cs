
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Entities
{
    public class User
    {
        // id is automatically assigned to as primary key in entity framework
        public int Id { get; set; }
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        public UserType UserType { get; set; }


    }
    public enum UserType
    {
        JobSeeker,
        Employer
    }

}