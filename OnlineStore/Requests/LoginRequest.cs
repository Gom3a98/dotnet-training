using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Requests
{
    public class LoginRequest
    {
        [Required, RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string EmailAddress { get; set; } = null!;
        [Required, RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
