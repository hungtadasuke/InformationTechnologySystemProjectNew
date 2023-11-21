using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Information
{
    public class Caesar : Cryptography
    {
        private static String alphabet = "aáàảãạăắằẳẵặâấầẩẫậbcdđeéèẻẽẹêếềểễệghiíìỉĩịklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵ";

        public Caesar(string privateKey) : base(privateKey)
        {
        }

        public override string decode(string cyberText)
        {
            this.PlainText = "";
            //Duyệt mảng kí tự message
            for (int i = 0; i < cyberText.Length; i++)
            {
                //Lấy ra kí tự
                char ch = cyberText[i];
                if (Char.IsLetter(ch))
                {
                    int index = alphabet.IndexOf(Char.ToLower(ch));
                    this.PlainText += Char.IsUpper(ch) ? Char.ToUpper(alphabet[(index - int.Parse(this.Key) % 89 + 89) % 89]) : alphabet[(index - int.Parse(this.Key) % 89 + 89) % 89];
                }
                else
                {
                    this.PlainText += ch;
                }

            }
            //Trả về chuỗi kết quả
            return this.PlainText;
        }

        public override string encode(string plainText)
        {
            this.CyberText = "";
            //Duyệt mảng kí tự message
            for (int i = 0; i < plainText.Length; i++)
            {
                //Lấy ra kí tự
                char ch = plainText[i];
                if (Char.IsLetter(ch))
                {
                    int index = alphabet.IndexOf(Char.ToLower(ch));
                    this.CyberText += Char.IsUpper(ch) ? Char.ToUpper(alphabet[(index + int.Parse(this.Key)) % 89]) : alphabet[(index + int.Parse(this.Key)) % 89];
                }
                else
                {
                    this.CyberText += ch;
                }

            }
            //Trả về chuỗi kết quả
            return this.CyberText;
        }
    }
}
