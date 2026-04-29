using System.Security.Cryptography;
using System.Text;

namespace Masroofy.Business.Services
{
    public class SecurityService
    {
        private string _storedHash = "";
        private int _failedAttempts = 0;
        private DateTime? _lockoutUntil = null;

        public void SetPin(string plainPin)
        {
            _storedHash = ComputeSha256Hash(plainPin);
        }

        public bool VerifyPin(string enteredPin)
        {
            if (_lockoutUntil.HasValue && DateTime.Now < _lockoutUntil.Value)
                return false;

            bool isValid = ComputeSha256Hash(enteredPin) == _storedHash;
            if (!isValid)
            {
                _failedAttempts++;
                if (_failedAttempts >= 3)
                {
                    _lockoutUntil = DateTime.Now.AddSeconds(30);
                    _failedAttempts = 0;
                }
                return false;
            }
            _failedAttempts = 0;
            _lockoutUntil = null;
            return true;
        }

        public bool IsLockedOut() => _lockoutUntil.HasValue && DateTime.Now < _lockoutUntil.Value;

        public bool HasPin() => !string.IsNullOrEmpty(_storedHash);

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes) builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}