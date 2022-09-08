using System.Text;
using System.Linq;

namespace Assignment1
{
    public class BigNumberCalculator
    {
        private int BitCount;
        private EMode Mode;
        private static StringBuilder builder;

        public BigNumberCalculator(int bitCount, EMode mode)
        {
            BitCount = bitCount;
            Mode = mode;
        }

        public static string GetOnesComplementOrNull(string num)
        {
            if (!IsValidFormat(num))
            {
                return null;
            }

            if (num[1] != 'b')
            {
                return null;
            }

            builder = new StringBuilder();

            builder.Append("0b");

            for (int i = 2; i < num.Length; i++)
            {
                builder.Append(num[i] == '0' ? '1' : '0');
            }

            string result = builder.ToString();

            return result;
        }

        public static string GetTwosComplementOrNull(string num)
        {
            if (!IsValidFormat(num))
            {
                return null;
            }

            if (num[1] != 'b')
            {
                return null;
            }

            if (num.Substring(2).All(n => n == '0'))
            {
                return num;
            }
            
            string onesComplement = GetOnesComplementOrNull(num);

            int lastZeroIndex = onesComplement.LastIndexOf('0');

            if (lastZeroIndex == onesComplement.Length - 1)
            {
                return onesComplement.Substring(0, lastZeroIndex) + '1';
            }

            else
            {
                char[] tempCharArray = onesComplement.ToCharArray();

                tempCharArray[lastZeroIndex]++;

                for (int i = lastZeroIndex + 1; i < tempCharArray.Length; i++)
                {
                    tempCharArray[i] = '0';
                }

                return new string(tempCharArray);
            }
        }

        public static string ToBinaryOrNull(string num)
        {
            if (!IsValidFormat(num))
            {
                return null;
            }
            if (num[0] == '0' && num.Length == 1)
            {
                return "0b0";
            }
            if (num.Length > 1)
            {
                if (num[1] == 'b')
                {
                    return num;
                }
                if (num[1] == 'x')
                {
                    return HexStrToBin(num);
                }
            }

            string result = DecStrToBin(num);

            return result;
        }

        public static string ToHexOrNull(string num)
        {
            if (!IsValidFormat(num))
            {
                return null;
            }

            if (num[0] == '0' && num.Length == 1)
            {
                return "0x0";
            }

            if (num.Length > 1)
            {
                if (num[1] == 'x')
                {
                    return num;
                }

                if (num[1] == 'b')
                {
                    return BinStrToHex(num);
                }
            }

            num = DecStrToBin(num);

            string result = BinStrToHex(num);

            return result;
        }

        public static string ToDecimalOrNull(string num)
        {
            if (!IsValidFormat(num))
            {
                return null;
            }

            if (num.Length == 1)
            {
                return num;
            }

            if (num[1] != 'b' && num[1] != 'x')
            {
                return num;
            }
            if (num[1] == 'b')
            {
                return BinStrToDecimal(num);
            }
            
            string result = HexStrToBin(num);

            return BinStrToDecimal(result);
        }


        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;

            if (!IsValidFormat(num1) || !IsValidFormat(num2))
            {
                return null;
            }
            
            bool bIsSameSignNumbers = false;
            char originSignBit = (char)0;

            num1 = ToBinaryOrNull(num1).Substring(2);
            num2 = ToBinaryOrNull(num2).Substring(2);

            if (num1.Length > BitCount || num2.Length > BitCount)
            {
                return null;
            }

            num1 = num1.PadLeft(BitCount, num1[0] == '0' ? '0' : '1');
            num2 = num2.PadLeft(BitCount, num2[0] == '0' ? '0' : '1');

            if (num1[0] == num2[0])
            {
                bIsSameSignNumbers = true;
                originSignBit = num1[0];
            }

            string sum = BinaryStringAdder(num1, num2);

            sum = sum.Substring(sum.Length - BitCount);

            if (bIsSameSignNumbers && originSignBit != sum[0])
            {
                bOverflow = true;
            }

            string result = "0b" + sum;

            if (Mode == EMode.Decimal)
            {
                return ToDecimalOrNull(result);
            }

            return result;
        }

        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            
            if (!IsValidFormat(num1) || !IsValidFormat(num2))
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
        
        private static bool IsValidFormat(string num)
        {
            if (string.IsNullOrWhiteSpace(num))
            {
                return false;
            }
            
            if (num[0] == '0')
            {
                if (num.Length == 1)
                {
                    return true;
                }

                if (num[1] == 'b')
                {
                    if (num.Length < 3)
                    {
                        return false;
                    }

                    if (num.Substring(2).All(n => !(n == '0' || n == '1')))
                    {
                        return false;
                    }

                    return true;
                } // 2진수 포맷 검사

                if (num[1] == 'x') 
                {
                    if (num.Length < 3)
                    {
                        return false;
                    }

                    for (int i = 2; i < num.Length; i++)
                    {
                        if (num[i] < 48 || num[i] > 57 && num[i] < 65 || num[i] > 70)
                        {
                            return false;
                        }
                    }

                    return true;
                } // 16진수 포맷 검사

                return false;
            }

            if (num[0] > 48 && num[0] <= 57 || num[0] == '-')
            {
                if (num.Length == 1)
                {
                    return num[0] != '-' ? true : false;
                }

                if (num[0] == '-' && (num[1] <= 48 || num[1] > 57))
                {
                    return false;
                }

                if (num[0] == '-' || num[0] > 48 || num[0] <= 57)
                {
                    for (int i = 1; i < num.Length; i++)
                    {
                        if (num[i] < 48 || num[i] > 57)
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }  // 10진수 포맷 검사

            return false;
        }
        private static string BinaryStringAdder(string numA, string numB)
        {
            if (numA.Length > numB.Length)
            {
                numB = numB.PadLeft(numA.Length, '0');
            }
            else if (numA.Length < numB.Length)
            {
                numA = numA.PadLeft(numB.Length, '0');
            }

            char[] sum = new char[numA.Length];
            bool bCarryExist = false;

            for (int i = numA.Length - 1; i >= 0; i--)
            {
                if (numA[i] == numB[i])
                {
                    sum[i] = '0';

                    if (numA[i] == '1')
                    {
                        if (bCarryExist)
                        {
                            sum[i]++;
                        }
                        bCarryExist = true;
                    }
                    else
                    {
                        if (bCarryExist)
                        {
                            sum[i]++;
                        }
                        bCarryExist = false;
                    }
                }
                else
                {
                    sum[i] = '1';

                    if (bCarryExist)
                    {
                        sum[i]--;
                    }
                }
            }

            string result = new string(sum);

            if (bCarryExist)
            {
                result = '1' + result;
            }

            return result;
        }
        private static string BinaryStrMultipleAdder(string numA, int Count)
        {
            string result = numA;

            for (int i = 0; i < Count - 1; i++)
            {
                result = BinaryStringAdder(numA, result);
            }

            return result;
        }
        private static string IntToBinary(int a)
        {
            if (a <= 1)
            {
                return $"{a}";
            }
            string rest = $"{a % 2}";

            return IntToBinary(a / 2) + rest;
        }
        private static string DecStrToBin(string num)
        {
            bool bIsMinus = false;
            
            string result;

            if (num[0] == '-')
            {
                num = num.Substring(1);
                bIsMinus = true;
            }

            if (num.Length == 1)
            {
                string oneBitString = IntToBinary(num[0] - 48);

                if(bIsMinus)
                {
                    result = GetTwosComplementOrNull("0b" + oneBitString);

                    if (result[2] == '0')
                    {
                        result = result.Insert(2, "1");

                        return result;
                    }

                    return result;
                }

                return "0b0" + oneBitString;
            }
            
            int[] numToIntArr = new int[num.Length];
            string[] numToBinaryArr = new string[num.Length];

            for (int i = 0; i < num.Length; i++)
            {
                numToIntArr[i] = num[i] - 48;
                numToBinaryArr[i] = IntToBinary(numToIntArr[i]);
            }

            string sum = BinaryStrMultipleAdder(numToBinaryArr[0], 10);

            for (int i = 1; i < numToBinaryArr.Length; i++)
            {
                string temp = BinaryStringAdder(sum, numToBinaryArr[i]);

                if (i == numToBinaryArr.Length - 1)
                {
                    sum = temp;
                    break;
                }

                sum = BinaryStrMultipleAdder(temp, 10);
            }

            if (bIsMinus)
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
        private static string HexStrToBin(string num)
        {
            num = num.Substring(2);
            builder = new StringBuilder();
            builder.Append("0b");
            int[] numToIntArr = new int[num.Length];

            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] < 58)
                {
                    numToIntArr[i] = num[i] - 48;
                }
                else
                {
                    numToIntArr[i] = num[i] - 55;
                }

                builder.Append(IntToBinary(numToIntArr[i]).PadLeft(4, '0'));
            }

            string result = builder.ToString();
            
            return result;
        }
        private static string DecimalStrAdder(string numA, string numB)
        {
            if (numA.Length > numB.Length)
            {
                numB = numB.PadLeft(numA.Length, '0');
            }
            else if (numA.Length < numB.Length)
            {
                numA = numA.PadLeft(numB.Length, '0');
            }

            char[] sum = new char[numA.Length];

            bool bCarryExist = false;
            const int CARRY = 1;

            for (int i = numA.Length - 1; i >= 0; i--)
            {
                if (!bCarryExist)
                {
                    if (numA[i] + numB[i] - 48 <= 57)
                    {
                        sum[i] = (char)(numA[i] + numB[i] - 48);
                    }
                    else
                    {
                        bCarryExist = true;
                        sum[i] = (char)(numA[i] + numB[i] - 58);
                    }
                }
                else
                {
                    if (numA[i] + numB[i] - 48 + CARRY <= 57)
                    {
                        sum[i] = (char)(numA[i] + numB[i] - 48 + CARRY);
                        bCarryExist = false;
                    }
                    else
                    {
                        sum[i] = (char)(numA[i] + numB[i] - 58 + CARRY);
                    }
                }
            }

            string result = new string(sum);

            if (bCarryExist)
            {
                return "1" + result;
            }

            return result;
        }
        private static string BinStrToDecimal(string num)
        {
            bool bIsNegative = false;

            if (num.Length == 3 && num[2] == '0')
            {
                return "0";
            }
            if (num.Substring(2).All(n => n == '0'))
            {
                return "0";
            } 

            if (num[2] == '0')
            {
                num = num.Substring(3);
            }
            else
            {
                bIsNegative = true;
                num = GetTwosComplementOrNull(num);

                if (num[2] == '0')
                {
                    num = num.Substring(3);
                }
                else
                {
                    num = num.Substring(2);
                }
            }

            char[] numToCharArr = num.ToCharArray();

            string sum = ""; 

            for (int i = 0; i < numToCharArr.Length; i++)
            {
                string temp = numToCharArr[i] == '0' ? sum : DecimalStrAdder(sum, $"{numToCharArr[i]}");

                if (i == numToCharArr.Length - 1)
                {
                    sum = temp;
                    break;
                }

                sum = DecimalStrAdder(temp, temp);
            }
            if (bIsNegative)
            {
                return "-" + sum;
            }

            return sum;
        }
        private static string BinStrToHex(string num)
        {
            num = num.Substring(2);

            int toHexLength = num.Length / 4;

            if (num.Length % 4 != 0)
            {
                num = num[0] == '0' ? num.PadLeft((toHexLength + 1) * 4, '0') : num.PadLeft((toHexLength + 1) * 4, '1');
            }

            char[] numToCharArr = new char[num.Length / 4];

            for (int i = 0; i < num.Length; i += 4)
            {
                int temp = int.Parse(BinToDecWithoutComplement("0b" + num.Substring(i, 4)));

                if (temp < 10)
                {
                    numToCharArr[i / 4] = (char)(temp + 48);
                }
                else
                {
                    numToCharArr[i / 4] = (char)(temp + 55);
                }
            }

            string result = "0x" + new string(numToCharArr);

            return result;
            
        }
        private static string BinToDecWithoutComplement(string num)
        {
            if (num.Length == 3 && num[2] == 0 || num == "0b0000")
            {
                return "0";
            }

            num = num.Substring(2);

            char[] numToCharArr = num.ToCharArray();

            string sum = "";

            for (int i = 0; i < numToCharArr.Length; i++)
            {
                string temp = numToCharArr[i] == '0' ? sum : DecimalStrAdder(sum, $"{numToCharArr[i]}");

                if (i == numToCharArr.Length - 1)
                {
                    sum = temp;
                    break;
                }

                sum = DecimalStrAdder(temp, temp);
            }

            return sum;
        }
       
    }
}
