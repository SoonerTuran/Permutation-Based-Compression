using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Permutation_Based_Compression
{
    public class PermutationCompressor
    {
        public byte[] Compress(byte[] inputData)
        {
            var histogram = TransformationHelper.CalculateHistogram(inputData);
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
            return permutationCount.ToByteArray();
        }

        public byte[] Decompress(byte[] inputData, Dictionary<byte, int> histogram)
        {

            // Continue
            /*
            int lenght = histogram.Sum(kvp => kvp.Value);

            BigInteger permutationOrder = new BigInteger(inputData);

            var sortedArray = imageArray.OrderBy(x => x).ToArray();

            var combinationCount = TransformationHelper.CalculateCombinationCount(histogram);

            BigInteger firstRange = 0, lastRange = combinationCount;

            for (int i = 0; i < lenght; i++)
            {
                BigInteger groupWidth = (lastRange - firstRange) / (lenght - i);

                if (groupWidth == 0) groupWidth = 1;

                BigInteger quotient = BigInteger.DivRem(permutationOrder - firstRange - 1, groupWidth, out BigInteger remainder);

                var indexRange = FindValueIndicesUsingBinarySearch(sortedArray, decompressedArray[i]);
                firstRange += indexRange[0] * groupWidth;
                lastRange -= (lenght - i - 1 - indexRange[1]) * groupWidth;


                sortedArray[(int)quotient] = Int32.MaxValue;
                sortedArray = sortedArray.AsParallel().WithDegreeOfParallelism(4).OrderBy(x => x).ToArray();
            }
            */
            return null;
        }
    }

}
