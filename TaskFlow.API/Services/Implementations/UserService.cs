using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace TaskFlow.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly TaskFlowDbContext _context;

        public UserService(TaskFlowDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(UserRegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Role = UserRole.User // Par défaut, tous les utilisateurs sont des utilisateurs normaux (User)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || user.PasswordHash != HashPassword(dto.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return "MOCK_TOKEN"; // Remplacer par une vraie génération de JWT plus tard
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashed);
        }
    }
}
