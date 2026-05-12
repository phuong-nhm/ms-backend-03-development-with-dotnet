using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI
{
    public class User
    {
        public int Id { get; set; }              // Unique identifier
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }    // User's first name
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }     // User's last name
        [Required]
        [EmailAddress]
        public string Email { get; set; }        // Work email address
        [Phone]
        public string PhoneNumber { get; set; }  // Contact number
        [Required]
        public string Department { get; set; }   // HR/IT department assignment
        [Required]
        public string Role { get; set; }         // Job role or title
    }
}
