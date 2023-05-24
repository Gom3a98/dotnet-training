using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Models;
using OnlineStore.Requests;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private static readonly User user = new User();
        private readonly IConfiguration configuration;
        private readonly BookstoresdbContext bookstoresdb;
        private readonly IEmailService emailService;

        public AuthController(IConfiguration _config , BookstoresdbContext _context , IEmailService _emailService)
        {
            configuration = _config;
            bookstoresdb = _context;
            emailService = _emailService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostRegister(RegisterationRequest request)
        {
            CreateEncryptPassword(request.Password, out byte[] encryptedPassword, out byte[] saltPassword);

            User user = new User()
            {
                EmailAddress = request.EmailAddress,
                Password = encryptedPassword,
                SaltPassword = saltPassword,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Source = request.Source
            };
            var VerificationToken = CreateVerificationToken(user);


            user.Refreshtokens.Add(VerificationToken);
            bookstoresdb.Users.Add(user);
            bookstoresdb.SaveChanges();
            
            string VerificationUrl = "https://localhost:7248/api/auth/verify/" + user.UserId + "/" + VerificationToken.Token;
            string emailBody = $"Dear {user.FirstName},<br><br><br>Thank you for registering with our service. To complete your registration, please use the following verification url:<br><br>{VerificationUrl}<br><br>If you did not create an account or believe this email was sent to you by mistake, please ignore this message.<br><br>If you have any questions or need assistance, please contact our support team at support@example.com.<br><br>Thank you,<br><br>Your Company Name";
            EmailRequest email = new EmailRequest()
            {
                To = request.EmailAddress,
                Subject = "Email Verfication",
                Body = emailBody
            };
            emailService.SendEmail(email);
            return Ok("Check your Mail box to verify the email and activate the account");
        }
       
        [HttpPost("login") , AllowAnonymous]
        public async Task<ActionResult<User>> PostLoginAsync(LoginRequest request)
        {
            User user = new User();
            if (bookstoresdb.Users == null)
                return NotFound("No Users Are Registered before");
             
            user = bookstoresdb.Users.SingleOrDefault(u => u.EmailAddress == request.EmailAddress);

            if (user==null || user.EmailAddress != request.EmailAddress)
            {
                return NotFound("User Not Found");
            }
            if (!VerifyCredentials(request.Password, user.Password, user.SaltPassword))
            {
                return NotFound("Password is incorrect");
            }
            if (!IsVerifiedEmail(user))
            {
                return NotFound("Email Not Verified");
                
            }
            var jwt = CreateJWT(user);
            return Ok((new { Token = jwt }));
        }
        [HttpGet("send_mail") , AllowAnonymous]
        public ActionResult SendMail(EmailRequest request)
        {
            emailService.SendEmail(request);
            return Ok();
        }
        [HttpGet("verify/{id}/{token}")]
        public ActionResult VerifyEmail(int id , string token)
        {
            var verifyToken = bookstoresdb.Refreshtokens.SingleOrDefault(a => a.UserId == id);
            if(verifyToken != null && verifyToken.Token.Equals( token))
            {
                bookstoresdb.Refreshtokens.Remove(verifyToken);
                bookstoresdb.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok("Verified");
        }
        [HttpGet("PasswordReset/{email}") , AllowAnonymous]
        public ActionResult PasswordReset(string email)
        {
            var user = bookstoresdb.Users.First(a => a.EmailAddress.Equals(email));
            if(user != null)
            {
                string VerificationUrl1 = $"https://localhost:7248/api/auth/PasswordReset/{user.UserId}";
                string VerificationUrl2 = $"https://localhost:5131/api/auth/PasswordReset/{user.UserId}";
                string emailBody = $"Dear {user.FirstName},<br><br><br>We have received a request to reset the password for your account. If you did not initiate this request, please disregard this email.<br><br><br>To proceed with the password reset, please click on the link below:<br><br><br>{VerificationUrl1}<br><br><br>If the above link does not work, you can copy and paste the following URL into your browser:<br><br><br>{VerificationUrl2}<br><br><br>Please note that the password reset link will expire after [expiry period]. If you do not reset your password within this time, you will need to submit another password reset request.<br><br><br>If you have any questions or need assistance, please contact our support team at support@example.com.<br>Thank you,<br>Your Company Name";

                EmailRequest MailToSend = new EmailRequest
                {
                    To = email,
                    Subject = "Password Reset Request",
                    Body = emailBody,
                };
                emailService.SendEmail(MailToSend);
                return Ok("Check your mailbox");
            }
           else
            {
                return NotFound();
            }
        }

        [HttpPost("PasswordReset/{id}") , AllowAnonymous]
        public ActionResult PasswordReset(int id , ResetPasswordRequest resetPassword)
        {
            var user = bookstoresdb.Users.Find(id);
            if(user != null)
            {
                CreateEncryptPassword(resetPassword.Password, out byte[] encryptedPassword, out byte[] saltPassword);
                user.Password = encryptedPassword;
                user.SaltPassword = saltPassword;
                bookstoresdb.Users.Update(user);
                bookstoresdb.SaveChanges();
                return Ok("Password Reseted");

            }
            return BadRequest("User Not Valid");
        }

        private string CreateJWT(User user)
        {
            var CurrentRole = bookstoresdb.Roles.FirstOrDefault(r => r.RoleId == user.RoleId).RoleDesc;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.EmailAddress),
                new Claim(ClaimTypes.Role , CurrentRole)

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Secret").Value));
            var cred = new SigningCredentials(key , SecurityAlgorithms.HmacSha384Signature);
            
            var token = new JwtSecurityToken(
                claims : claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials : cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private static void CreateEncryptPassword(string password, out byte[] encryptedPassword, out byte[] SaltPassword)
        {

            using (var cmd = new HMACSHA512())
            {
                SaltPassword = cmd.Key;
                encryptedPassword = cmd.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
        private static bool VerifyCredentials(string password, byte[] encryptedPassword, byte[] SaltPassword)
        {
            using (var cmd = new HMACSHA512(SaltPassword))
            {

                var computedPassword = cmd.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return encryptedPassword.SequenceEqual( computedPassword);
            }

        }
        private Refreshtoken CreateVerificationToken(User user )
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            Refreshtoken refreshtoken = new Refreshtoken
            {
                ExpiryDate = DateTime.Now.AddMinutes(15),
                Token = token,
                User = user,
                UserId = user.UserId
            };


            return refreshtoken;
        }
        private bool IsVerifiedEmail(User user)
        {
            var token = bookstoresdb.Refreshtokens.SingleOrDefault(a => a.UserId == user.UserId && a.isTokenVerified);
            return token == null;
        }
    }
}
