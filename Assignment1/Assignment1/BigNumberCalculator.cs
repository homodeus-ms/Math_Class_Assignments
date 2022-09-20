using System.Text;
using System.Linq;
using System;

namespace Assignment1
{
    public class BigNumberCalculator
    {
        private int mBitCount;
        private EMode mMode;
        private static readonly byte BIN_HEX_MIN_LENGTH = 3;
        private static readonly byte BIN_HEX_MSB_INDEX = 2;
        private static StringBuilder mBuilder;

        public BigNumberCalculator(int bitCount, EMode mode)
        {
            mBitCount = bitCount;
            mMode = mode;
        }

        public static string GetOnesComplementOrNull(string num)
        {
            if (num.Length < BIN_HEX_MIN_LENGTH)
            {
                return null;
            }

            if (!isValidFormat(num) || isValidFormat(num) && num[1] != 'b')
            {
                return null;
            }

            mBuilder = new StringBuilder(num.Length);
            mBuilder.Append("0b");

            for (int i = BIN_HEX_MSB_INDEX; i < num.Length; i++)
            {
                mBuilder.Append(num[i] == '0' ? '1' : '0');
            }

            string result = mBuilder.ToString();

            return result;
        }

        public static string GetTwosComplementOrNull(string num)
        {
            if (num.Length < BIN_HEX_MIN_LENGTH)
            {
                return null;
            }

            if (!isValidFormat(num) || isValidFormat(num) && num[1] != 'b')
            {
                return null;
            }

            if (num.Substring(BIN_HEX_MSB_INDEX).All(n => n == '0'))
            {
                return num;
            }

            num = GetOnesComplementOrNull(num);

            char[] chars = num.ToCharArray();

            int lastZeroIndex = num.LastIndexOf('0');

            chars[lastZeroIndex] = '1';

            for (int i = lastZeroIndex + 1; i < chars.Length; i++)
            {
                chars[i] = '0';
            }

            string result = new string(chars);

            return result;
        }

        public static string ToBinaryOrNull(string num)
        {
            if (!isValidFormat(num))
            {
                return null;
            }

            if (num.Length >= BIN_HEX_MIN_LENGTH && num[1] == 'b')
            {
                return num;
            }

            string result;

            if (num.Length >= BIN_HEX_MIN_LENGTH && num[1] == 'x')
            {
                mBuilder = new StringBuilder(num.Length);
                mBuilder.Append("0b");

                for (int i = BIN_HEX_MSB_INDEX; i < num.Length; i++)
                {
                    if (num[i] <= '9')
                    {
                        mBuilder.Append(getBinBitRecursive(num[i] - '0').PadLeft(4, '0'));
                    }
                    else if (num[i] >= 'A')
                    {
                        mBuilder.Append(getBinBitRecursive(num[i] - 55).PadLeft(4, '0'));
                    }
                }

                result = mBuilder.ToString();

                return result;
            }

            bool bMinus = num[0] == '-' ? true : false;

            if (bMinus)
            {
                num = num.Substring(1);
            }

            long parseOut;
            string sum;

            if (long.TryParse(num, out parseOut))
            {
                result = getBinBitRecursive(parseOut);

                if (bMinus)
                {
                    result = GetTwosComplementOrNull("0b" + result);

                    if (result[2] == '0')
                    {
                        result = result.Insert(2, "1");

                        return result;
                    }

                    return result;
                }

                return result == "0" ? "0b0" : "0b0" + result;
            }

            else
            {
                string[] binaryDigits = new string[num.Length];

                for (int i = 0; i < num.Length; i++)
                {
                    binaryDigits[i] = getBinBitRecursive(int.Parse($"{num[i]}"));
                }

                sum = addOneBinBitTenTimes(binaryDigits[0]);

                for (int i = 1; i < binaryDigits.Length; i++)
                {
                    string temp = addTwoBinBits(sum, binaryDigits[i]);

                    if (i == binaryDigits.Length - 1)
                    {
                        sum = temp;
                        break;
                    }

                    sum = addOneBinBitTenTimes(temp);
                }

                if (bMinus)
                {
                    result = GetTwosComplementOrNull("0b" + sum);

                    if (result[2] == '0')
                    {
                        result = result.Insert(2, "1");

                        return result;
                    }

                    return result;
                }

                return "0b0" + sum;
            }
        }

        public static string ToHexOrNull(string num)
        {
            if (!isValidFormat(num))
            {
                return null;
            }

            if (num.Length >= BIN_HEX_MIN_LENGTH && num[1] == 'x')
            {
                return num;
            }

            if (num.Length < BIN_HEX_MIN_LENGTH || num.Length >= BIN_HEX_MIN_LENGTH && num[1] <= '9')
            {
                num = ToBinaryOrNull(num);
            }

            num = num.Substring(BIN_HEX_MSB_INDEX);

            int resultHexLength = num.Length % 4 == 0 ? num.Length / 4 : num.Length / 4 + 1;

            num = num[0] == '0' ? num.PadLeft(resultHexLength * 4, '0') : num.PadLeft(resultHexLength * 4, '1');

            char[] chars = new char[num.Length / 4];

            for (int i = 0; i < num.Length; i += 4)
            {
                string fourBits = num.Substring(i, 4);

                fourBits = fourBits[0] == '1' ? '0' + fourBits : fourBits;

                int temp = int.Parse(ToDecimalOrNull("0b" + fourBits));

                if (temp < 10)
                {
                    chars[i / 4] = (char)(temp + 48);
                }
                else
                {
                    chars[i / 4] = (char)(temp + 55);
                }
            }

            string result = "0x" + new string(chars);

            return result;
        }

        public static string ToDecimalOrNull(string num)
        {
            if (!isValidFormat(num))
            {
                return null;
            }

            if (num.Length < BIN_HEX_MIN_LENGTH)
            {
                return num;
            }

            if (num[1] != 'x' && num[1] != 'b')
            {
                return num;
            }

            if (num[1] == 'x')
            {
                num = ToBinaryOrNull(num);
            }


            bool bMinus = false;

            if (num[BIN_HEX_MSB_INDEX] == '1')
            {
                num = GetTwosComplementOrNull(num);
                bMinus = true;

                num = num[BIN_HEX_MSB_INDEX] == '1' ? num.Substring(BIN_HEX_MSB_INDEX) : num.Substring(BIN_HEX_MSB_INDEX + 1);
            }
            else
            {
                num = num.Substring(BIN_HEX_MSB_INDEX + 1);
            }

            if (num.Length <= 64) // 0b 빼고 64비트 이내의 2진수 처리
            {
                ulong sum = 0;

                char[] chars = num.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    sum += (ulong)((chars[i] - '0') * Math.Pow(2, chars.Length - 1 - i));
                }

                return bMinus == false ? $"{sum}" : "-" + sum;
            }

            else // 64넘어가는 2진수 string 자체로 처리
            {
                char[] chars = num.ToCharArray();

                string sum = "";

                for (int i = 0; i < chars.Length; i++)
                {
                    string temp = chars[i] == '0' ? sum : addTwoDecStrings(sum, $"{chars[i]}");

                    if (i == chars.Length - 1)
                    {
                        sum = temp;
                        break;
                    }

                    sum = addTwoDecStrings(temp, temp);
                }

                if (bMinus)
                {
                    return "-" + sum;
                }

                return sum;
            }
        }

        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;

            if (!isValidFormat(num1) || !isValidFormat(num2))
            {
                return null;
            }

            bool bSignsEqual = false;

            char originSignBit = (char)0;

            num1 = ToBinaryOrNull(num1).Substring(BIN_HEX_MSB_INDEX);
            num2 = ToBinaryOrNull(num2).Substring(BIN_HEX_MSB_INDEX);

            if (num1.Length > mBitCount || num2.Length > mBitCount)
            {
                return null;
            }

            num1 = num1.PadLeft(mBitCount, num1[0] == '0' ? '0' : '1');
            num2 = num2.PadLeft(mBitCount, num2[0] == '0' ? '0' : '1');

            if (num1[0] == num2[0])
            {
                bSignsEqual = true;
                originSignBit = num1[0];
            }

            string sum = addTwoBinBits(num1, num2);

            sum = sum.Substring(sum.Length - mBitCount);

            if (bSignsEqual && originSignBit != sum[0])
            {
                bOverflow = true;
            }

            string result = "0b" + sum;

            if (mMode == EMode.Decimal)
            {
                return ToDecimalOrNull(result);
            }

            return result;
        }

        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;

            if (!isValidFormat(num1) || !isValidFormat(num2))
            {
                return null;
            }

            num1 = ToBinaryOrNull(num1);
            num2 = ToBinaryOrNull(num2);
            num2 = GetTwosComplementOrNull(num2);

            string result = AddOrNull(num1, num2, out bOverflow);

            if (result == null)
            {
                return null;
            }

            return result;
        }

        private static bool isValidFormat(string num)
        {
            if (string.IsNullOrWhiteSpace(num))
            {
                return false;
            }

            if (num.Length == 1)
            {
                return num[0] >= '0' && num[0] <= '9';
            }

            if (num[0] == '0')
            {
                if (num.Length < BIN_HEX_MIN_LENGTH)   // Bin_Hex_Min_LENGTH == 3
                {
                    return false;
                }

                if (num[1] == 'b')
                {
                    for (int i = BIN_HEX_MSB_INDEX; i < num.Length; i++)
                    {
                        if (!(num[i] == '1' || num[i] == '0'))
                        {
                            return false;
                        }
                    }

                    return true;
                }

                if (num[1] == 'x')
                {
                    for (int i = BIN_HEX_MSB_INDEX; i < num.Length; i++)
                    {
                        if (num[i] < '0' || (num[i] > '9' && num[i] < 'A') || num[i] > 'F')
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }

            else if (num[0] == '-')
            {
                if (num[1] <= '0' || num[1] > '9')
                {
                    return false;
                }
                for (int i = 2; i < num.Length; i++)
                {
                    if (num[i] < '0' || num[i] > '9')
                    {
                        return false;
                    }
                }
                return true;
            }

            else
            {
                for (int i = 0; i < num.Length; i++)
                {
                    if (num[i] < '0' || num[i] > '9')
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private static string getBinBitRecursive(long num)
        {
            if (num <= 1)
            {
                return $"{num}";
            }

            return getBinBitRecursive(num / 2) + $"{num % 2}";
        }

        private static string addTwoBinBits(string numA, string numB)
        {
            if (numA.Length >= BIN_HEX_MIN_LENGTH && numA[1] == 'b' && numB.Length >= BIN_HEX_MIN_LENGTH && numB[1] == 'b')
            {
                numA = numA.Substring(BIN_HEX_MSB_INDEX);
                numB = numB.Substring(BIN_HEX_MSB_INDEX);
            }

            if (numA.Length > numB.Length)
            {
                numB = numB.PadLeft(numA.Length, '0');
            }
            else if (numA.Length < numB.Length)
            {
                numA = numA.PadLeft(numB.Length, '0');
            }

            char carry = '0';
            char[] sum = new char[numA.Length];

            for (int i = numA.Length - 1; i >= 0; i--)
            {
                if (carry == '0')
                {
                    if (numA[i] == numB[i])
                    {
                        sum[i] = '0';
                        carry = numA[i] == '1' ? '1' : '0';
                    }
                    else
                    {
                        sum[i] = '1';
                    }
                }
                else
                {
                    if (numA[i] == numB[i])
                    {
                        sum[i] = '1';
                        carry = numA[i] == '1' ? '1' : '0';
                    }
                    else
                    {
                        sum[i] = '0';
                        carry = '1';
                    }
                }
            }

            string result = new string(sum);

            if (carry == '1')
            {
                result = result.Insert(0, "1");
            }

            return result;

        }

        private static string addOneBinBitTenTimes(string numA)
        {
            string result;

            result = addTwoBinBits(numA + "000", numA + "0");

            return result;
        }

        private static string addTwoDecStrings(string A, string B)
        {
            bool bCarriedOver = false;

            if (A.Length > B.Length)
            {
                B = B.PadLeft(A.Length, '0');
            }
            else
            {
                A = A.PadLeft(B.Length, '0');
            }

            char[] sums = new char[A.Length];

            for (int i = A.Length - 1; i >= 0; i--)
            {
                char temp = (char)(A[i] + B[i] - '0');

                if (bCarriedOver)
                {
                    if (temp + '1' - '0' > '9')
                    {
                        sums[i] = (char)(temp + '1' - '0' - 10);
                    }
                    else
                    {
                        sums[i] = (char)(temp + '1' - '0');
                        bCarriedOver = false;
                    }
                }
                else
                {
                    if (temp > '9')
                    {
                        bCarriedOver = true;
                        sums[i] = (char)(temp - 10);
                    }
                    else
                    {
                        sums[i] = temp;
                    }
                }
            }

            string result = new string(sums);

            if (bCarriedOver)
            {
                return "1" + result;
            }

            return result;
        }
    }
}
