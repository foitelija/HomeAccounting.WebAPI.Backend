using HomeAccounting.Application.Interfaces.Identity;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Application.Models.Identity;
using HomeAccounting.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Infrastructure.Services
{

    public class AuthService : IAuthService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly IUserRepository _userRepository;

        public AuthService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings;
            _userRepository = userRepository;
        }

        public Task<AuthResponse> Login(AuthRequest request)
        {
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
        private JwtSecurityToken GenerateToken()
        {
            var jwtSecutiryToken = new JwtSecurityToken();

            return jwtSecutiryToken;
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
