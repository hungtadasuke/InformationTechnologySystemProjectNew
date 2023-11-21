using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAO.Information
{
    public class RSA
    {
        private long n;
        private long p, q;
        private long e;
        private long d;
        private long phiN;

        public long N { get => n; set => n = value; }
        public long P { get => p; set => p = value; }
        public long Q { get => q; set => q = value; }
        public long E { get => e; set => e = value; }
        public long D { get => d; set => d = value; }
        public long PhiN { get => phiN; set => phiN = value; }

        public RSA(long n, long e)
        {
            this.n = n;
            this.e = e;
        }

        public RSA(long p, long q, long e) : this(p, q)
        {
            this.e = e;
        }
        public void generateKey()
        {
            if (this.q == 0 || this.p == 0)
            {
                var r = Calculator.findPrimeFactors(n);
                this.q = r.Item1;
                this.p = r.Item2;
            }
            if (this.p != 0 && this.q != 0)
            {
                Calculator.findPrimeFactors(n);
                this.phiN = (p - 1) * (q - 1);
                var result = Calculator.calcExtendedEuclidean(e, phiN);
                if (result.Item2 < 0)
                {
                    result.Item2 = ((result.Item2 % phiN) + phiN) % phiN;
                }
                d = result.Item2;
            }
        }
        public String Encode(string plainText)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            int[] cipherTextArr = new int[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                if (this.n > (int)bytes[i])
                {
                    cipherTextArr[i] = (int)Calculator.modexp(bytes[i], this.e, this.n);
                }
                else
                {
                    cipherTextArr[i] = (int)bytes[i];
                }
            }

            String cypherText = "";
            for (int i = 0; i < cipherTextArr.Length; i++)
            {
                cypherText += cipherTextArr[i].ToString();
                if (i < cipherTextArr.Length - 1)
                {
                    cypherText += "0xFF";
                }
            }
            return cypherText;
        }

        public String Decode(String cypherText)
        {
            String[] cypherTextArr = Regex.Split(cypherText, "0xFF");
            byte[] plainTextArr = new byte[cypherTextArr.Length];
            for (int i = 0; i < cypherTextArr.Length; i++)
            {
                if (this.n <= int.Parse(cypherTextArr[i]))
                {
                    plainTextArr[i] = (byte)int.Parse(cypherTextArr[i]);
                }
                else
                {
                    plainTextArr[i] = (byte)Calculator.modexp(int.Parse(cypherTextArr[i]), this.d, this.n);
                }
            }
            return Encoding.UTF8.GetString(plainTextArr);
        }
    }
}
