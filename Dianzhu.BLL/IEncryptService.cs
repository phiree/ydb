using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL
{
    public interface IEncryptService
    {
        string GetMD5Hash(string input);
        string Encrypt(string input);
        string Decrypt(string result);
    }
}
