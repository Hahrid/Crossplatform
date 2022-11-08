using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Lab1CrossplatformV2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> FileDataChar = new List<char>();
            string[] FileDataString;
            SortedDictionary<char, int> rebus_symbols = new SortedDictionary<char, int> { };
            SortedDictionary<char, int> words_symbols = new SortedDictionary<char, int> { };
            string Result =null;
            int N, M;
            //READ AND WRITE DATA FROM FILE
            string pathREAD = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Data1.txt");
            string pathWRITE = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../OUTPUT.txt");

            if (File.Exists(pathWRITE))
            {
                FileDataString = File.ReadAllLines(pathREAD);
            }
            else
                throw new Exception("No such file exists");

            //Convert String array to char array
            foreach (char c in FileDataString[0])
                if (c != ' ' && c != '-')
                    FileDataChar.Add(c);

            //inialise N( SIZE OF MARIX) and M( AMOUNT OF WORDS TO FIND IN MATRIX) from FileDataChar
            try
            {
                N = int.Parse(FileDataChar[0].ToString());
                M = int.Parse(FileDataChar[1].ToString());
            }
            catch
            {
                throw new Exception("Size of matrix or amount of words in not Intager");
            }
            

            if (N + M != FileDataString.Length - 1)
                throw new Exception("Not enogh data");


            for (int i = 1; i < N+1; ++i)
            {
                foreach (var symbol in FileDataString[i])
                {
                    if (rebus_symbols.ContainsKey(symbol))
                        rebus_symbols[symbol]++;
                    else
                        rebus_symbols.Add(symbol, 1);
                }
            }
            for (int i = N+1; i <= M+N; ++i)
            {
                foreach (var symbol in FileDataString[i])
                {
                    if (words_symbols.ContainsKey(symbol))
                        words_symbols[symbol]++;
                    else
                        words_symbols.Add(symbol, 1);
                }
            }

            foreach (var symbol_counter in words_symbols)
            {
                rebus_symbols[symbol_counter.Key] -= symbol_counter.Value;
            }

            foreach (var symbol_counter in rebus_symbols)
            {
                int dataValue = symbol_counter.Value;
                char dataKey = symbol_counter.Key;
                while (dataValue > 0)
                {
                    Result += dataKey;
                    dataValue--;
                }
                
            }
            if (File.Exists(pathWRITE))
            {
                File.WriteAllText(pathWRITE, Result);
            }
            else
                throw new Exception("No file to write to");
        }
    }
}
