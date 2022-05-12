using System;
using System.Text;
using System.Security.Cryptography;

namespace TabPizzaRestaurant.Data
{
    public class passwordController
    {
        MD5CryptoServiceProvider md5;
        public passwordController()
        {
            md5 = new MD5CryptoServiceProvider();
        }

        public bool readHashAndCompare(string rawPassword, string hash)
        {
            var byteData = Encoding.ASCII.GetBytes(rawPassword);
            Byte[] md5hash = md5.ComputeHash(byteData);
            var hashed = new ASCIIEncoding().GetString(md5hash);
            return hashed.Equals(hash);
        }

        public string hashPassword(string rawPassword)
        {
            var byteData = Encoding.ASCII.GetBytes(rawPassword);
            Byte[] md5hash = md5.ComputeHash(byteData);
            var hashed = new ASCIIEncoding().GetString(md5hash);
            return hashed;
        }
    }
}
