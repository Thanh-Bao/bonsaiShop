using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace ASP.NET_Backend
{
    public class Authentication : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                string phonePayload = claim[1].Value;
                string role = claim[1].Value;
                string URL = context.HttpContext.Request.Path;
                string username = URL.Split('/').Last();
                if (!phonePayload.Equals(username))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new 
                {
                    statusCode = 401,
                    message = "Lỗi xác định quyền truy cập, bạn không thể xem nội dung này"
                });
            }
        }

        public static string HashPasword(string passwordPlaintText, string phone)
        {
            // Step 1, calculate MD5 hash from passwordPlaintText + timeCreate
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(passwordPlaintText + phone);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string GenerateJwtToken(string username)
        {
            var securiryKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY_SECRET_KEY_IS_LONG_ENOUGH_12345"));
            var credentials = new SigningCredentials(securiryKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("username",username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
     
            var token = new JwtSecurityToken(
                issuer: "bao",
                audience: "baobao",
                claims,
                expires: DateTime.UtcNow.AddMinutes(99999),
                signingCredentials: credentials
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }




    }
}
