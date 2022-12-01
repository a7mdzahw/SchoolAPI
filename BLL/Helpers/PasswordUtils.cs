using System.Security.Cryptography;
using System.Text;

namespace BLL.Helpers;

public class PasswordUtils
{
    public string EncryptPassword(string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

    }


    public string DecryptPassword(string encodedPassword)
    {
        return  Encoding.UTF8.GetString(Convert.FromBase64String(encodedPassword));
    }  
}