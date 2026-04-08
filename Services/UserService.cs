using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserManagamentApi_1.Data;
using UserManagamentApi_1.DTOs;
using UserManagamentApi_1.Models;

namespace UserManagamentApi_1.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }


        // REGISTER

        public async Task<User> Register(UserRegisterDto dto)
        {
            if (dto == null)
                throw new Exception("Datos inválidos");
            // Verificamos si el correo ya existe para no duplicar usuarios
            var exists = await _context.Users
                .AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower()); if (exists)
                throw new Exception("El correo ya está registrado");

            CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // LOGIN

        public async Task<User> Login(UserLoginDto dto)
        {
            if (dto == null)
                throw new Exception("Datos inválidos");
            // Buscamos el usuario por correo
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (user == null)
                throw new Exception("Usuario no encontrado");

            // Verificamos la contraseña
            var isValid = VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt);

            if (!isValid)
                throw new Exception("Contraseña incorrecta");

            return user;
        }


        // GET

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }


        // DELETE

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }


        // UPDATE


        public async Task<User?> Update(int id, UserUpdateDto dto)
        {
            if (dto == null)
                throw new Exception("Datos inválidos");

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            // Verificamos si el nuevo email ya lo tiene otro usuario
            var exists = await _context.Users
                .AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower() && u.Id != id);

            if (exists)
                throw new Exception("El correo ya está en uso");

            // Actualizamos datos
            user.Name = dto.Name;
            user.Email = dto.Email.ToLower();

            await _context.SaveChangesAsync();

            return user;
        }

        // HASHING

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            // Genera automáticamente un salt único
            using var hmac = new HMACSHA512();

            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Comparación directa byte a byte
            return computedHash.SequenceEqual(hash);
        }
    }
}