using System;
using System.Collections.Generic;
using System.Text;

namespace IceApp.Application.Interfaces
{
    public interface IEncryptionService
    {
        public string Encrypt(string clearText);
        public string Decrypt(string cipherText);

    }
}
