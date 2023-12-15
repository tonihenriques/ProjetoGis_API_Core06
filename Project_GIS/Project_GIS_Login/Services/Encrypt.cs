using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Project_GIS_Login.Services
{
    public class Encrypt
    {
        #region Senhas

        [ComVisible(false)]
        public static string CreateHashFromPassword(string pstrOriginalPassword)
        {
            if (string.IsNullOrEmpty(pstrOriginalPassword))
                return string.Empty;

            string str3 = ConvertToHashedString(pstrOriginalPassword).Substring(0, 5);
            byte[] bytes = Encoding.UTF8.GetBytes(pstrOriginalPassword + str3);
            HashAlgorithm lobjHash = new MD5CryptoServiceProvider();
            return Convert.ToBase64String(lobjHash.ComputeHash(bytes));
        }

        [ComVisible(false)]
        public static string ConvertToHashedString(string pstrOriginal)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pstrOriginal);
            HashAlgorithm lobjHash = new MD5CryptoServiceProvider();
            return Convert.ToBase64String(lobjHash.ComputeHash(bytes));
        }

        #endregion
    }
}