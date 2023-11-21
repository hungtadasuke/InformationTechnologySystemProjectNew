using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KeyDTO
    {
        private string keyId;
        private string encrypted_key;
        private string key_of_encrypted_key;

        public string KeyId { get => keyId; set => keyId = value; }
        public string Encrypted_key { get => encrypted_key; set => encrypted_key = value; }
        public string Key_of_encrypted_key { get => key_of_encrypted_key; set => key_of_encrypted_key = value; }

        public KeyDTO(string keyId, string encrypted_key, string key_of_encrypted_secret)
        {
            this.keyId = keyId;
            this.encrypted_key = encrypted_key;
            this.key_of_encrypted_key = key_of_encrypted_secret;
        }
    }
}
