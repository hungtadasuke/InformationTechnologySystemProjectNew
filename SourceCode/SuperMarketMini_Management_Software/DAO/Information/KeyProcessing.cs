using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Information
{
    public class KeyProcessing
    {
        public static Substitution ActSubstitution()
        {
            KeyDAO keyDAO = new KeyDAO();
            KeyDTO key01 = keyDAO.getKey("K01");
            Caesar caesar = new Caesar(key01.Key_of_encrypted_key);
            string substitutionKey = caesar.decode(key01.Encrypted_key);
            Substitution substitution = new Substitution(substitutionKey);
            return substitution;
        }

        public static RSA ActRSA()
        {
            KeyDAO keyDAO = new KeyDAO();
            KeyDTO key02 = keyDAO.getKey("K02");
            String[] KnapsackKeyArr = key02.Key_of_encrypted_key.Split('#');
            String[] superIncreasinggArrStr = KnapsackKeyArr[0].Split(',');
            int[] superIncreasingg = Array.ConvertAll<String, int>(superIncreasinggArrStr, int.Parse);
            int m = int.Parse(KnapsackKeyArr[1]);
            int w = int.Parse(KnapsackKeyArr[2]);
            Knapsack knapsack = new Knapsack(superIncreasingg, m, w);
            String[] rsaKeyArrStr = knapsack.Decode(key02.Encrypted_key).Split('#');
            long n = long.Parse(rsaKeyArrStr[0]);
            long e = long.Parse(rsaKeyArrStr[1]);
            RSA rsa = new RSA(n, e);
            rsa.generateKey();
            return rsa;
        }
    }
}
