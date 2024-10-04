using System.Security.Cryptography;
using System.Text;

namespace authmodule.Helpers
{

    public class CustomPasswordHasher
    {
        // Hash password with salt
        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt
            using (var hmac = new HMACSHA256(salt))
            {
                byte[] hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Combine salt and hashed password
                byte[] hashWithSalt = new byte[salt.Length + hashedPassword.Length];
                Buffer.BlockCopy(salt, 0, hashWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedPassword, 0, hashWithSalt, salt.Length, hashedPassword.Length);

                // Convert to Base64 string for storage
                return Convert.ToBase64String(hashWithSalt);
            }
        }

        // Verify the password by comparing with the hash
        public static bool VerifyPassword(string password, string storedHash)
        {
            // Get the hash as bytes from Base64 string
            byte[] hashWithSalt = Convert.FromBase64String(storedHash);

            // Extract salt from the stored hash
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashWithSalt, 0, salt, 0, salt.Length);

            // Hash the input password with the extracted salt
            using (var hmac = new HMACSHA256(salt))
            {
                byte[] hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Compare the stored hash with the hash of the input password
                for (int i = 0; i < hashedPassword.Length; i++)
                {
                    if (hashWithSalt[i + salt.Length] != hashedPassword[i])
                        return false;
                }
            }

            return true;
        }
    }
}