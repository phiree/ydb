using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel
{
   public interface IEncryptService
    {
           string Encrypt(string toEncrypt, bool useHashing);
           string Decrypt(string cipherString, bool useHashing);
          string GetMD5Hash(string input);
    }
}
