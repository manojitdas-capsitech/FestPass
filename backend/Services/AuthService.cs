using backend.Data;
using backend.DTOs;
using backend.Models.Entities;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace backend.Services
{
    public class AuthService
    {
        private readonly MongoContext _context;
        private readonly JwtService _jwtService;

        public AuthService(MongoContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            var email = dto.Email.ToLower();

            var exists = await _context.Users
                .Find(u => u.Email == email)
                .AnyAsync();

            if (exists)
                throw new Exception("User already exists");

            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                Email = email,
                PasswordHash = hasher.HashPassword(null!, dto.Password),
                Role = dto.Role
            };

            await _context.Users.InsertOneAsync(user);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .Find(u => u.Email == dto.Email.ToLower())
                .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("Invalid email or password");

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password
            );

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid email or password");

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Role = user.Role.ToString()
            };
        }

    }
}
