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
        public static Dictionary<byte, int> CalculateHistogram(byte[] byteArray)
        {
            var histogram = new Dictionary<byte, int>();

            foreach (byte value in byteArray)
            {
                if (histogram.ContainsKey(value))
                {
                    histogram[value]++;
                }
                else
                {
                    histogram.Add(value, 1);
                }
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
            Dictionary<byte, int> histogram = CalculateHistogram(byteArray);

            BigInteger denominator = 1;
            foreach (var kvp in histogram)
            {
                denominator *= CalculateFactorial(kvp.Value);
            }

            BigInteger totalCombinations = CalculateFactorial(byteArray.Length) / denominator;
       
            return totalCombinations;
        }

        public static BigInteger CalculateCombinationCount(Dictionary<byte, int> histogram)
        {
            var lenght = histogram.Sum(kvp => kvp.Value);   
            BigInteger denominator = 1;
            foreach (var kvp in histogram)
            {
                denominator *= CalculateFactorial(kvp.Value);
            }

            BigInteger totalCombinations = CalculateFactorial(lenght) / denominator;

            return totalCombinations;
        }

        public static (int lowerBound, int upperBound) FindRange(Dictionary<byte, int> histogram, byte keyToFind)
        {
            var lowerBound = histogram.Where(kvp => kvp.Key < keyToFind).Sum(kvp => kvp.Value);
            var upperBound = lowerBound + histogram[keyToFind] - 1;
            histogram.Remove(keyToFind);

            return (lowerBound, upperBound);
        }

    }

}
