using System;
using System.IO;

namespace Utility
{
    public static class ConverterExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="csvPath"></param>
        public static void CsvToJson(this Converter<string, string> target, string csvPath)
        {
            string csv = File.ReadAllText(csvPath);
        }
    }
}