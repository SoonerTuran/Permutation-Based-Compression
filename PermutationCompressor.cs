using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Permutation_Based_Compression
{
    public class PermutationCompressor
    {
        public (byte[] data, int[] histogram) Compress(byte[] inputData)
        {
            var histogramSave = TransformationHelper.CalculateHistogram(inputData);
            var histogram = TransformationHelper.CalculateHistogram(inputData);
            var sorted = inputData.OrderBy(x => x).ToList();
            var combinationCount = TransformationHelper.CalculateCombinationCount(inputData);

            BigInteger firstRange = 0, lastRange = combinationCount, groupWidth = 0;

            for (int i = 0; i < inputData.Length; i++)
            {
                groupWidth = (lastRange - firstRange) / (inputData.Length - i);
                var (lowerBound, upperBound) = TransformationHelper.FindRange(histogram, inputData[i]);

                firstRange += lowerBound * groupWidth;
                lastRange -= (inputData.Length - i - 1 - upperBound) * groupWidth;
            }

            var permutationCount = firstRange + groupWidth;

            return (permutationCount.ToByteArray(), histogramSave);
        }

        public byte[] Decompress(byte[] inputData, int[] histogram)
        {
            var sortedList = TransformationHelper.ExpandHistogram(histogram);
            int lenght = sortedList.Count;

            BigInteger permutationOrder = new BigInteger(inputData);
            BigInteger combinationCount = TransformationHelper.CalculateCombinationCount(sortedList.ToArray());
            BigInteger firstRange = 0, lastRange = combinationCount;

            var decompressedArray = new byte[lenght];

            for (int i = 0; i < lenght; i++)
            {
                BigInteger groupWidth = (lastRange - firstRange) / (lenght - i);

                if (groupWidth == 0) groupWidth = 1;

                BigInteger quotient = BigInteger.DivRem(permutationOrder - firstRange - 1, groupWidth, out BigInteger remainder);

                var key = sortedList[(int)quotient];
                decompressedArray[i] = key;

                var (lowerBound, upperBound) = TransformationHelper.FindFirstAndLastRange(sortedList, key);
                firstRange += lowerBound * groupWidth;
                lastRange -= (lenght - i - 1 - upperBound) * groupWidth;


                sortedList.RemoveAt((int)quotient);
            }

            return decompressedArray;
        }
    }

}
