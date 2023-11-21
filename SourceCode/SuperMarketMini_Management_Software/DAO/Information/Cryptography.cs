using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Information
{
    public abstract class Cryptography
    {
        private String plainText;
        private String cyberText;
        private String privateKey;

        public string PlainText { get => plainText; set => plainText = value; }
        public string CyberText { get => cyberText; set => cyberText = value; }
        public string Key { get => privateKey; set => privateKey = value; }

        protected Cryptography(string privateKey)
        {
            Key = privateKey;
        }

        public abstract string encode(String plainText);
        public abstract string decode(String cyberText);
    }
}
