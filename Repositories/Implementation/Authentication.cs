using Domains.Data;
using Domains.ViewModels;
using Domains.ViewModels.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repositories.Implementation
{
    public class Authentication : IAuthentication
    {
        private readonly IMongoCollection<users> _collection;
        private readonly IConfiguration _config;
        public Authentication(MongoDbContext context, IConfiguration config)
        {
            _collection = context.GetMongoCollection<users>("users");
            _config = config;
        }
        public async Task<AuthValidationModel> VerifyUser(string userEmail, string password)
        {
            try
            {
                var filter = Builders<users>.Filter.Eq("email", userEmail);
                users user = await _collection.Find(filter).SingleOrDefaultAsync();
                AuthValidationModel authValidationModel = new AuthValidationModel();
                if (user is not null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, user?.Password))
                    {
                        authValidationModel.Status = 1;
                        authValidationModel.ValidResult = GenerateToken(userEmail, user.Id);
                        authValidationModel.UserId = user.Id.ToString();
                    }
                    else
                    {
                        authValidationModel.Status = 0;
                        authValidationModel.ErrorMessage = "Invalid Credentials!";
                    }
                }
                else
                {
                    authValidationModel.Status = 0;
                    authValidationModel.ErrorMessage = "Invalid Credentials!";
                }
                return authValidationModel;
            }
            catch
            {
                return new AuthValidationModel() { Status = 0, ValidResult = "", UserId = "", ErrorMessage = "Some error occured while generating the token!" };
            }
        }

        public string GenerateToken(string userEmail,string userId)
        {
            List<Claim> Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,userEmail),
                    new Claim(ClaimTypes.Sid,userId)
                };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _config.GetSection("JWT:key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                                   issuer: _config.GetSection("JWT:issuer").Value,
                                   audience: _config.GetSection("JWT:issuer").Value,
                                   claims: Claims,
                                   expires: DateTime.UtcNow.AddDays(1),
                                   signingCredentials: cred
   );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        public async Task<AuthValidationModel> CreateUser(users newUser)
        {
            var filter = Builders<users>.Filter.Eq("email", newUser.Email);
            users user = await _collection.Find(filter).SingleOrDefaultAsync();
            if (user is not null)
            {
                return new AuthValidationModel() { Status = 0, ErrorMessage = "User already exist!" };
            }
            else
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password, salt);
                newUser.Password = passwordHash;
                await _collection.InsertOneAsync(newUser);
                return new AuthValidationModel() { Status = 1, ValidResult = GenerateToken(newUser.Email,newUser.Id.ToString()), UserId = newUser.Id.ToString() };
            }
        }
        public AuthValidationModel VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("JWT:key").Value);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer= _config.GetSection("JWT:issuer").Value,
                    ValidateAudience = true,
                    ValidAudience= _config.GetSection("JWT:issuer").Value,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.Where(x => x.Type.Contains("sid")).FirstOrDefault()?.Value;

                // return user id from JWT token if validation successful
                return new AuthValidationModel() { Status = 1, ValidResult = "", UserId = userId };
            }
            catch
            {
                // return null if validation fails
                return new AuthValidationModel() { Status = 0, ValidResult = "", UserId = "",ErrorMessage="Some error occured while validating the token!" };
            }
        }
    }
}
