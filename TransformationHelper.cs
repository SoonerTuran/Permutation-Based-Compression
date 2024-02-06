using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Permutation_Based_Compression
{
    public static class TransformationHelper
    {
        public static int[] CalculateHistogram(byte[] byteArray)
        {
            int maxKey = byteArray.Max() + 1;
            int[] histogram = new int[maxKey];

            foreach (int value in byteArray)
            {
                histogram[value]++;
            }

            return histogram;
        }

        public static BigInteger CalculateFactorial(int number)
        {
            BigInteger factorial = 1;
            for (int i = 2; i <= number; i++)
            {
                factorial *= i;
            }
            return factorial;
        }

        public static BigInteger CalculateCombinationCount(byte[] byteArray)
        {
            int maxKey = byteArray.Max() + 1;
            var histogram = CalculateHistogram(byteArray);

            BigInteger denominator = 1;

            for (int i = 0; i < maxKey; i++)
            {
                denominator *= CalculateFactorial(histogram[i]);
            }

            return CalculateFactorial(byteArray.Length) / denominator;
        }

        public static (int lowerBound, int upperBound) FindRange(int[] histogram, byte keyToFind)
        {   
            int lowerBound = 0, upperBound = 0; 

            for (int i = 0; i < keyToFind; i++)
            {
                lowerBound += histogram[i];
            }

            upperBound += lowerBound + histogram[keyToFind] - 1;
            histogram[keyToFind] -= 1;

            return (lowerBound, upperBound);
        }

        public static (int lowerBound, int upperBound) FindFirstAndLastRange(List<byte> sortedList, byte keyToFind)
        {
            int lowerBound = FindFirstIndex(sortedList, keyToFind);
            int upperBound = FindLastIndex(sortedList, keyToFind);

            return (lowerBound, upperBound);
        }

        public static int FindFirstIndex(List<byte> sortedList, int valueToFind)
        {
            int low = 0;
            int high = sortedList.Count - 1;
            int result = -1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (valueToFind > sortedList[mid])
                {
                    low = mid + 1;
                }
                else
                {
                    if (valueToFind == sortedList[mid])
                    {
                        result = mid;
                    }
                    high = mid - 1;
                }
            }

            return result;
        }

        public static int FindLastIndex(List<byte> sortedList, int valueToFind)
        {
            int low = 0;
            int high = sortedList.Count - 1;
            int result = -1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (valueToFind < sortedList[mid])
                {
                    high = mid - 1;
                }
                else
                {
                    if (valueToFind == sortedList[mid])
                    {
                        result = mid;
                    }
                    low = mid + 1;
                }
            }

            return result;
        }

        public static List<byte> ExpandHistogram(int[] histogram)
        {
            List<byte> expandedList = new List<byte>();

            for (int i = 0; i < histogram.Length; i++)
            {
                for (int j = 0; j < histogram[i]; j++)
                {
                    expandedList.Add((byte)i);
                }
            }

            return expandedList;
        }

        public static byte[] ConvertIntArrayToByteArray(int[] intArray)
        {
            List<byte> byteList = new List<byte>();

            foreach (int value in intArray)
            {
                byteList.AddRange(BitConverter.GetBytes(value));
            }

            return byteList.ToArray();
        }

        public static int[] ConvertByteArrayToIntArray(byte[] byteArray)
        {
            if (byteArray.Length % 4 != 0)
            {
                throw new ArgumentException("Byte dizisinin uzunluğu 4'ün katı olmalıdır.");
            }

            List<int> intList = new List<int>();

            for (int i = 0; i < byteArray.Length; i += 4)
            {
                intList.Add(BitConverter.ToInt32(byteArray, i));
            }

            return intList.ToArray();
        }

        public static void CircularShift(BitArray bits, int shiftAmount)
        {
            bool temp;
            int length = bits.Length;

            // Pozitif shiftAmount için sağa kaydırma
            if (shiftAmount > 0)
            {
                for (int shift = 0; shift < shiftAmount; shift++)
                {
                    temp = bits[length - 1]; // En sağdaki biti sakla
                    for (int i = length - 1; i > 0; i--)
                    {
                        bits[i] = bits[i - 1]; // Bitleri bir pozisyon sağa kaydır
                    }
                    bits[0] = temp; // Saklanan biti en sola yerleştir
                }
            }
            // Negatif shiftAmount için sola kaydırma
            else if (shiftAmount < 0)
            {
                for (int shift = 0; shift < Math.Abs(shiftAmount); shift++)
                {
                    temp = bits[0]; // En soldaki biti sakla
                    for (int i = 0; i < length - 1; i++)
                    {
                        bits[i] = bits[i + 1]; // Bitleri bir pozisyon sola kaydır
                    }
                    bits[length - 1] = temp; // Saklanan biti en sağa yerleştir
                }
            }
            // shiftAmount 0 ise herhangi bir kaydırma yapılmaz
        }


    }

}
