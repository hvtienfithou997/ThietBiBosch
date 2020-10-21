using System;
using System.Security.Cryptography;
using System.Text;

namespace FoodCleanB.Helpers
{
    public static class EncryptHelper
    {
        public static string Base64Encode(string plainText)
        {
            try
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception e)
            {
                return null;
            }
        }		
		
		public static string GenerateSHA256String(string inputString)
		{
		  SHA256 sha256 = SHA256Managed.Create();
		  byte[] bytes = Encoding.UTF8.GetBytes(inputString);
		  byte[] hash = sha256.ComputeHash(bytes);
		  return GetStringFromHash(hash);
		}

		public static string GenerateSHA512String(string inputString)
		{
		  SHA512 sha512 = SHA512Managed.Create();
		  byte[] bytes = Encoding.UTF8.GetBytes(inputString);
		  byte[] hash = sha512.ComputeHash(bytes);
		  return GetStringFromHash(hash);
		}
		  
		private static string GetStringFromHash(byte[] hash)
		{
			StringBuilder result = new StringBuilder();

			for (int i = 0; i < hash.Length; i++)
			{
				result.Append(hash[i].ToString("X2"));
			}
			return result.ToString();
		}
    }
}