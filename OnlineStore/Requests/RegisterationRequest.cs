using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Requests
{
    public class RegisterationRequest
    {
        [Required, RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$") ]
        public string EmailAddress { get; set; } = null!;
        [Required, RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string RepeatedPassword { get; set; }
        [Required, MaxLength(50), MinLength(5)]
        public string? FirstName { get; set; }
        [Required, MaxLength(50), MinLength(5)]
        public string? MiddleName { get; set; }
        [Required, MaxLength(50), MinLength(5)]
        public string? LastName { get; set; }
        public string Source { get; set; }

    }
}
