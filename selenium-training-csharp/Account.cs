using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selenium_training_csharp
{
    public class Account
    {
        public Account()
        {
            FirstName = GenerateRandomString(10);
            LastName = GenerateRandomString(10);
            Address = GenerateRandomString(10);
            City = GenerateRandomString(10);
            Email = GenerateRandomString(6) + "@gmail.com";
            Phone = "+" + GenerateRandomInt(11);
            Postcode = GenerateRandomInt(5);
            Password = GenerateRandomString(8);
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public static Random rnd = new Random();

        public static string GenerateRandomString(int count)
        {
            char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                builder.Append(letters[rnd.Next(0, letters.Length - 1)]);
            }
            return builder.ToString();
        }
        public static string GenerateRandomInt(int count)
        {
            char[] letters = "0123456789".ToCharArray();
            var builder = new StringBuilder();
            for (int i=0; i< count; i++)
            {
                builder.Append(letters[rnd.Next(0, letters.Length - 1)]);
            }
            return builder.ToString();
        }
    }
}
