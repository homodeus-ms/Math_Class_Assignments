using System.Text;
using System.Linq;

namespace Assignment1
{
    public class BigNumberCalculator
    {
        private readonly int mBitCount;
        private readonly EMode mMode;
        private static StringBuilder mbuilder;
        private const int First_BIT_INDEX = 2;

        public BigNumberCalculator(int bitCount, EMode mode)
        {
            mBitCount = bitCount;
            mMode = mode;
        }

        public static string GetOnesComplementOrNull(string num)
        {
            if (!isValidFormat(num))
            {
                return null;
            }

            if (num.Length == 1 || num[1] != 'b')
            {
                return null;
            }

            mbuilder = new StringBuilder();

            mbuilder.Append("0b");

            for (int i = First_BIT_INDEX; i < num.Length; i++)
            {
                mbuilder.Append(num[i] == '0' ? '1' : '0');
            }

            string result = mbuilder.ToString();

            return result;
        }

        public static string GetTwosComplementOrNull(string num)
        {
            if (!isValidFormat(num))
            {
                return null;
            }

            if (num.Length == 1 || num[1] != 'b')
            {
                return null;
            }

            if (num.Substring(First_BIT_INDEX).All(n => n == '0'))
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
            if (!isValidFormat(num))
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
                    return makeHexStrToBin(num);
                }
            }

            string result = makeDecStrToBin(num);

            return result;
        }

        public static string ToHexOrNull(string num)
        {
            if (!isValidFormat(num))
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
                    return makeBinStrToHex(num);
                }
            }

            num = makeDecStrToBin(num);

            string result = makeBinStrToHex(num);

            return result;
        }

        public static string ToDecimalOrNull(string num)
        {
            if (!isValidFormat(num))
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
                return makeBinStrToDec(num);
            }
            
            string result = makeHexStrToBin(num);

            return makeBinStrToDec(result);
        }


        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;

            if (!isValidFormat(num1) || !isValidFormat(num2))
            {
                return null;
            }
            
            bool bIsSameSignNumbers = false;
            char originSignBit = (char)0;

            num1 = ToBinaryOrNull(num1).Substring(First_BIT_INDEX);
            num2 = ToBinaryOrNull(num2).Substring(First_BIT_INDEX);

            if (num1.Length > mBitCount || num2.Length > mBitCount)
            {
                return null;
            }

            num1 = num1.PadLeft(mBitCount, num1[0] == '0' ? '0' : '1');
            num2 = num2.PadLeft(mBitCount, num2[0] == '0' ? '0' : '1');

            if (num1[0] == num2[0])
            {
                bIsSameSignNumbers = true;
                originSignBit = num1[0];
            }

            string sum = addTwoBinaryStrings(num1, num2);

            sum = sum.Substring(sum.Length - mBitCount);

            if (bIsSameSignNumbers && originSignBit != sum[0])
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

                    if (num.Substring(First_BIT_INDEX).All(n => !(n == '0' || n == '1')))
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

                    for (int i = First_BIT_INDEX; i < num.Length; i++)
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
                    return num[0] != '-';
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
        private static string addTwoBinaryStrings(string numA, string numB)
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
        private static string addBinaryStrMultiple(string numA, int Count)
        {
            string result = numA;

            for (int i = 0; i < Count - 1; i++)
            {
                result = addTwoBinaryStrings(numA, result);
            }

            return result;
        }
        private static string getBinaryFromIntRecursive(int a)
        {
            if (a <= 1)
            {
                return $"{a}";
            }
            string rest = $"{a % 2}";

            return getBinaryFromIntRecursive(a / 2) + rest;
        }
        private static string makeDecStrToBin(string num)
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
                string oneBitString = getBinaryFromIntRecursive(num[0] - 48);

                if (bIsMinus)
                {
                    result = GetTwosComplementOrNull("0b" + oneBitString);

                    if (result[First_BIT_INDEX] == '0')
                    {
                        result = result.Insert(First_BIT_INDEX, "1");

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
                numToBinaryArr[i] = getBinaryFromIntRecursive(numToIntArr[i]);
            }

            string sum = addBinaryStrMultiple(numToBinaryArr[0], 10);

            for (int i = 1; i < numToBinaryArr.Length; i++)
            {
                string temp = addTwoBinaryStrings(sum, numToBinaryArr[i]);

                if (i == numToBinaryArr.Length - 1)
                {
                    sum = temp;
                    break;
                }

                sum = addBinaryStrMultiple(temp, 10);
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
        private static string makeHexStrToBin(string num)
        {
            num = num.Substring(First_BIT_INDEX);
            mbuilder = new StringBuilder();
            mbuilder.Append("0b");
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

                mbuilder.Append(getBinaryFromIntRecursive(numToIntArr[i]).PadLeft(4, '0'));
            }

            string result = mbuilder.ToString();
            
            return result;
        }
        private static string addTwoDecimalStrings(string numA, string numB)
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
        private static string makeBinStrToDec(string num)
        {
            bool bIsNegative = false;

            if (num.Length == 3 && num[First_BIT_INDEX] == '0')
            {
                return "0";
            }
            if (num.Substring(First_BIT_INDEX).All(n => n == '0'))
            {
                return "0";
            } 

            if (num[First_BIT_INDEX] == '0')
            {
                num = num.Substring(First_BIT_INDEX + 1);
            }
            else
            {
                bIsNegative = true;
                num = GetTwosComplementOrNull(num);

                if (num[First_BIT_INDEX] == '0')
                {
                    num = num.Substring(First_BIT_INDEX + 1);
                }
                else
                {
                    num = num.Substring(First_BIT_INDEX);
                }
            }

            char[] numToCharArr = num.ToCharArray();

            string sum = ""; 

            for (int i = 0; i < numToCharArr.Length; i++)
            {
                string temp = numToCharArr[i] == '0' ? sum : addTwoDecimalStrings(sum, $"{numToCharArr[i]}");

                if (i == numToCharArr.Length - 1)
                {
                    sum = temp;
                    break;
                }

                sum = addTwoDecimalStrings(temp, temp);
            }
            if (bIsNegative)
            {
                return "-" + sum;
            }

            return sum;
        }
        private static string makeBinStrToHex(string num)
        {
            num = num.Substring(First_BIT_INDEX);

            int toHexLength = num.Length / 4;

            if (num.Length % 4 != 0)
            {
                num = num[0] == '0' ? num.PadLeft((toHexLength + 1) * 4, '0') : num.PadLeft((toHexLength + 1) * 4, '1');
            }

            char[] numToCharArr = new char[num.Length / 4];

            for (int i = 0; i < num.Length; i += 4)
            {
                int temp = int.Parse(makeBinToDecWithoutComplement("0b" + num.Substring(i, 4)));

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
        private static string makeBinToDecWithoutComplement(string num)
        {
            if (num.Length == 3 && num[First_BIT_INDEX] == 0 || num == "0b0000")
            {
                return "0";
            }

            num = num.Substring(First_BIT_INDEX);

            char[] numToCharArr = num.ToCharArray();

            string sum = "";

            for (int i = 0; i < numToCharArr.Length; i++)
            {
                string temp = numToCharArr[i] == '0' ? sum : addTwoDecimalStrings(sum, $"{numToCharArr[i]}");

                if (i == numToCharArr.Length - 1)
                {
                    sum = temp;
                    break;
                }

                sum = addTwoDecimalStrings(temp, temp);
            }

            return sum;
        }
       
    }
}
