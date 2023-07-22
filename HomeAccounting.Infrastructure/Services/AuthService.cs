using HomeAccounting.Application.Interfaces.Identity;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Application.Models.Identity;
using HomeAccounting.Domain;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Infrastructure.Services
{

    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;

        public AuthService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userRepository.FindUserByLoginAsync(request.Login);

            if (user == null)
            {
                throw new Exception($"Login '{request.Login}' does not exist.");
            }
            else if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Wrong password.");
            }
            else
            {
                return new AuthResponse() { UserName = request.Login, Token = CreateToken(user) };
            }

            throw new NotImplementedException();
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userRepository.UserExists(request.Login);

            if(existingUser)
            {
                throw new Exception($"Login '{request.Login}' already exists.");
            }

            CreatePasswordHas(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new FamilyMember
            {
                Login = request.Login,
                Name = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            try
            {
                var user = await _userRepository.Add(newUser);

                return new RegistrationResponse() { UserId = user.Id };
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: {ex.Message} ");
            }
        }

        /// <summary>
        ///  Craeate Token, Claims, expires, creds
        /// </summary>
        /// <returns>
        /// return token value for bearer auth
        /// </returns>
        private string CreateToken(FamilyMember user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var secutrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var singinCreds = new SigningCredentials(secutrityKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: singinCreds,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.DurationInMinutes)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }


        private void CreatePasswordHas(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
