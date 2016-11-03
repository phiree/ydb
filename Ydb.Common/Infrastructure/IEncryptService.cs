using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Ydb.Common.Infrastructure
{
    public interface IEncryptService 
    {

            string Encrypt(string toEncrypt, bool useHashing)
       ;
            string Decrypt(string cipherString, bool useHashing)
        ;

            string GetMD5Hash(string input)
       ;
    }
}
