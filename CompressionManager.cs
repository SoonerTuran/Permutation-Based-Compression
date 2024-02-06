using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Permutation_Based_Compression
{
    public class CompressionManager
    {
        private PermutationCompressor compressor = new PermutationCompressor();

        public void Compress(string inputPath, string outputPath, string histogramPath)
        {
            byte[] inputData = FileHelper.ReadFile(inputPath);

            var (compressedData, histogramData) = compressor.Compress(inputData);

            FileHelper.WriteFile(compressedData, outputPath);
            var byteHistogramData = TransformationHelper.ConvertIntArrayToByteArray(histogramData);
            FileHelper.WriteFile(byteHistogramData, histogramPath);
        }

        public void Decompress(string inputPath, string outputPath, string histogramPath)
        {
            byte[] inputData = FileHelper.ReadFile(inputPath);
            byte[] histogramData = FileHelper.ReadFile(histogramPath);
            var intHistogramData = TransformationHelper.ConvertByteArrayToIntArray(histogramData);

            byte[] decompressedData = compressor.Decompress(inputData, intHistogramData);
            FileHelper.WriteFile(decompressedData, outputPath);
        }
    }
}
