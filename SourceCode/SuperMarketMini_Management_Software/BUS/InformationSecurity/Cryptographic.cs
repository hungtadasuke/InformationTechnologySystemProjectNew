using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.InformationSecurity
{
    public abstract class Cryptographic
    {
        private String plainText;
        private String cipherText;
        private String privateKey;

        public string CipherText { get => cipherText; set => cipherText = value; }
        public string PlainText { get => plainText; set => plainText = value; }
        public string PrivateKey { get => privateKey; set => privateKey = value; }

        public abstract string encode(string plainText, string key);
        public abstract string decode(string cyberText, string key);
    }
}
