using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Requests
{
    public class ResetPasswordRequest
    {
        [Required, RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string RepeatedPassword { get; set; }

    }
}
