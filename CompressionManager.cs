using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Permutation_Based_Compression
{
    public class CompressionManager
    {
        private PermutationCompressor compressor = new PermutationCompressor();

        public void Compress(string inputPath, string outputPath)
        {
            byte[] inputData = FileHelper.ReadFile(inputPath);
            byte[] compressedData = compressor.Compress(inputData);
            FileHelper.WriteFile(compressedData, outputPath);
        }

        public void Decompress(string inputPath, string outputPath)
        {
            byte[] inputData = FileHelper.ReadFile(inputPath);
            byte[] decompressedData = compressor.Decompress(inputData);
            FileHelper.WriteFile(decompressedData, outputPath);
        }
    }
}
