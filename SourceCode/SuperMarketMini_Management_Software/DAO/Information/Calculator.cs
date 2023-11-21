using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Information
{
    public class Calculator
    {
        public static bool isPrime(long n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        public static (long, long) findPrimeFactors(long n)
        {
            // Nếu n là số nguyên tố thì không có cách nào tìm được
            if (isPrime(n))
            {
                Console.WriteLine("Không có cách nào viết {0} thành tích của hai số nguyên tố", n);
                return (0, 0);
            }

            // Duyệt từ 2 đến căn bậc hai của n
            for (long i = 2; i * i <= n; i++)
            {
                // Nếu i là ước của n và là số nguyên tố
                if (n % i == 0 && isPrime(i))
                {
                    // Tìm thương của n và i
                    long j = n / i;

                    // Nếu j cũng là số nguyên tố thì ta đã tìm được hai số cần tìm
                    if (isPrime(j))
                    {
                        return (i, j);
                    }
                }
            }

            // Nếu không tìm được hai số nguyên tố thì in ra thông báo
            Console.WriteLine("Không có cách nào viết {0} thành tích của hai số nguyên tố", n);
            return (0, 0);
        }

        public static long modexp(long a, long x, long n)
        {
            long r = 1;
            while (x > 0)
            {
                if (x % 2 == 1)
                {
                    r = (r * a) % n;
                }
                a = (a * a) % n;
                x = x / 2;
            }
            return r;
        }

        public static (long, long, long) calcExtendedEuclidean(long a, long b)
        {
            /* if (a == 0)
             {
                 return (b, 0, 1);
             }*/
            if (b == 0)
            {
                return (a, 1, 0);
            }
            // Iterative case: use a while loop and simple variables to update the coefficients and the remainders
            // Initialize the variables with the initial values
            long x = 1, y = 0, x1 = 0, y1 = 1, r = a, r1 = b;
            // Loop until the remainder is zero
            while (r1 > 0)
            {
                // Calculate the quotient and the new remainder
                long q = r / r1;
                long r2 = r % r1;
                // Update the coefficients using the formula x2 = x - q * x1 and y2 = y - q * y1
                // where x2 and y2 are the new coefficients
                long x2 = x - q * x1;
                long y2 = y - q * y1;
                // Assign the new values to the old variables
                x = x1; y = y1; x1 = x2; y1 = y2; r = r1; r1 = r2;
            }
            // Return the gcd and the coefficients
            return (r, x, y);
        }
    }
}
