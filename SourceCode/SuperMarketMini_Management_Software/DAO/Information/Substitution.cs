using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Information
{
    public class Substitution : Cryptography
    {
        private static String alphabet = "aáàảãạăắằẳẵặâấầẩẫậbcdđeéèẻẽẹêếềểễệghiíìỉĩịklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵ";

        public Substitution(string privateKey) : base(privateKey)
        {
        }

        public override string decode(string cyberText)
        {
            this.PlainText = "";
            for (int i = 0; i < cyberText.Length; i++)
            {
                if (Char.IsLetter(cyberText[i]))
                {
                    this.PlainText += Char.IsUpper(cyberText[i]) ? Char.ToUpper(alphabet[this.Key.IndexOf(Char.ToLower(cyberText[i]))]) : alphabet[this.Key.IndexOf(cyberText[i])];
                }
                else
                {
                    this.PlainText += cyberText[i];
                }
            }

            return this.PlainText;
        }

        public override string encode(string plainText)
        {
            this.CyberText = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                if (Char.IsLetter(plainText[i]))
                {
                    this.CyberText += Char.IsUpper(plainText[i]) ? Char.ToUpper(this.Key[alphabet.IndexOf(char.ToLower(plainText[i]))]) : this.Key[alphabet.IndexOf(plainText[i])];
                }
                else
                {
                    this.CyberText += plainText[i];
                }
            }
            return this.CyberText;
        }

        public String generateKey()
        {
            Random random = new Random();
            List<string> key = new List<string>();
            foreach (char c in alphabet)
            {
                key.Add(c.ToString());
            }
            for (int i = key.Count - 1; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                string tmp = key[i].ToString();
                key[i] = key[swapIndex];
                key[swapIndex] = tmp;
            }

            string target = "";
            foreach (string s in key)
            {
                target += s;
            }
            return target;
        }
    }
}
