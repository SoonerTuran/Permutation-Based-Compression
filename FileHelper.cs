using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutation_Based_Compression
{
    public static class FileHelper
    {
        public static byte[] ReadFile(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata dosya okunurken: {ex.Message}");
                return null;
            }
        }

        public static void WriteFile(byte[] data, string path)
        {
            try
            {
                File.WriteAllBytes(path, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata dosya yazılırken: {ex.Message}");
            }
        }
    }

}
