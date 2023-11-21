using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Information
{
    public class Knapsack
    {
        // private key
        private int[] superIncreasing;
        private int m, w;

        // public key 
        private int[] publicKey;

        // getter & setter
        public int[] SuperIncreasing { get => superIncreasing; set => superIncreasing = value; }
        public int M { get => m; set => m = value; }
        public int W { get => w; set => w = value; }

        // constructor
        public Knapsack(int[] superIncreasing, int m, int w)
        {
            this.SuperIncreasing = superIncreasing;
            this.M = m;
            this.W = w;
        }

        public Knapsack()
        {
        }

        // method
        public int[] GeneratePublicKey(int[] superIncreasingg, int m, int w)
        {
            publicKey = new int[superIncreasingg.Length];

            for (int i = 0; i < superIncreasingg.Length; i++)
            {
                publicKey[i] = (superIncreasingg[i] * w) % m;
            }

            return publicKey;
        }

        public int[] Encode(string plainText)
        {
            if (publicKey == null) publicKey = GeneratePublicKey(SuperIncreasing, m, w);

            byte[] bytes = Encoding.UTF8.GetBytes(plainText);

            int[] cipherText = new int[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                int sum = 0;

                for (int j = 7; j >= 0; j--)
                {
                    if (((bytes[i] >> j) & 1) == 1)
                    {
                        sum += publicKey[7 - j];
                    }
                }

                cipherText[i] = sum;
            }

            return cipherText;
        }

        public int[] Encode(string plainText, int[] publicKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);

            int[] cipherText = new int[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                int sum = 0;

                for (int j = 7; j >= 0; j--)
                {
                    if (((bytes[i] >> j) & 1) == 1)
                    {
                        sum += publicKey[7 - j];
                    }
                }

                cipherText[i] = sum;
            }

            return cipherText;
        }

        // Convert integer cipher to string cipher to save into database
        public static string ToString(int[] cipherText)
        {
            string text = "";

            for (int i = 0; i < cipherText.Length; i++)
            {
                text += cipherText[i].ToString();

                if (i != cipherText.Length - 1) text += "/";
            }

            return text;
        }

        //Extended Euclid
        //Find inverseW from w and m
        public int Extended_GCD()
        {
            int a = W, b = M;
            int d = 0, x = 0, y = 0, x1, x2, y1, y2;
            int q, r;

            if (b == 0)
            {
                d = M;
                x = 1;
                y = 0;
            }
            else
            {
                x2 = 1; x1 = 0; y2 = 0; y1 = 1;
                while (b > 0)
                {
                    q = a / b;
                    r = a - q * b;
                    x = x2 - q * x1;
                    y = y2 - q * y1;

                    a = b;
                    b = r;
                    x2 = x1;
                    x1 = x;
                    y2 = y1;
                    y1 = y;
                }

                d = a;
                x = x2;
                y = y2;
            }

            if (!(d > 1)) return x;
            return -1;
        }

        static int BinaryToDec(string input)
        {
            char[] array = input.ToCharArray();
            Array.Reverse(array);
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '1')
                {
                    if (i == 0)
                    {
                        sum += 1;
                    }
                    else
                    {
                        sum += (int)Math.Pow(2, i);
                    }
                }
            }

            return sum;
        }

        public string Decode(string cipherText)
        {
            string[] arrListCipherText = cipherText.Split('/');
            byte[] p = new byte[arrListCipherText.Length];
            int inverseW = Extended_GCD();
            string plainText = "";

            for (int i = 0; i < arrListCipherText.Length; i++)
            {
                int c = (int.Parse(arrListCipherText[i]) * inverseW) % M;

                char[] s = new char[SuperIncreasing.Length];

                for (int j = SuperIncreasing.Length - 1; j >= 0; j--)
                {
                    if (c < SuperIncreasing[j])
                    {
                        s[j] = '0';
                    }
                    else
                    {
                        s[j] = '1';
                    }

                    c -= SuperIncreasing[j] * int.Parse(s[j].ToString());
                }
                string str = new string(s);
                p[i] = (byte)BinaryToDec(str);

            }
            plainText = Encoding.UTF8.GetString(p);
            return plainText;
        }
    }
}
