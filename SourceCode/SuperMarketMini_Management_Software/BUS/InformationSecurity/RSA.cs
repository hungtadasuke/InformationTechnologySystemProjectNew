using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.InformationSecurity
{
    public class RSA : Cryptographic
    {
        private String publicKey;

        public string PublicKey { get => publicKey; set => publicKey = value; }

        public override string decode(string cyberText, string privateKey)
        {
            throw new NotImplementedException();
        }

        public override string encode(string plainText, string publicKey)
        {
            throw new NotImplementedException();
        }
    }
}
