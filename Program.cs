using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Permutation_Based_Compression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CompressionManager compressionManager = new CompressionManager();

            string inputPath = "C:\\Users\\Soner\\Desktop\\Test\\1.txt";
            string outputPath = "C:\\Users\\Soner\\Desktop\\Test\\1.pbc";
            string histogramPath = "C:\\Users\\Soner\\Desktop\\Test\\1.histo";

            compressionManager.Compress(inputPath, outputPath, histogramPath);

            inputPath = "C:\\Users\\Soner\\Desktop\\Test\\1.pbc";
            outputPath = "C:\\Users\\Soner\\Desktop\\Test\\2.txt";
            histogramPath = "C:\\Users\\Soner\\Desktop\\Test\\1.histo";

            compressionManager.Decompress(inputPath, outputPath, histogramPath);
        }
    }
}
