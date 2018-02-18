using System;
using System.Security.Cryptography;
using System.Text;
using EMX.JobyJobs.BL.Helpers;

namespace EMX.JobyJobs.ASPCoreFwk.Shared.Helpers
{
  public class AspCoreCryptographyHelper : ICryptographyHelper
  {
    public string GenerateHash256(string password)
    {
      // SHA512 is disposable by inheritance.
      using (var sha256 = SHA256.Create())
      {
        // Send a sample text to hash.
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Get the hashed string.
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
      }
    }

    //public string GenerateHash(string password)
    //{
    //  // generate a 128-bit salt using a secure PRNG
    //  byte[] salt = new byte[128 / 8];
    //  using (var rng = RandomNumberGenerator.Create())
    //  {
    //    rng.GetBytes(salt);
    //  }
    //  Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

    //  // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
    //  string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
    //    password: password,
    //    salt: salt,
    //    prf: KeyDerivationPrf.HMACSHA1,
    //    iterationCount: 10000,
    //    numBytesRequested: 256 / 8));

    //  return hashed;
    //}
  }
}
