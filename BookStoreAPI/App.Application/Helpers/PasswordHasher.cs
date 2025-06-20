namespace App.Application.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Use a secure hashing algorithm like BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the password against the hashed password
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
