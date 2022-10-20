using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Crossplatform_lab1
{
    class Program
    {
        public HashSet<string> GetWords(int N, int M,string[] FileDataString)
        {
            HashSet<string> words = new HashSet<string>();
            for (int i = FileDataString.Length - M; i < FileDataString.Length; i++)
                if ((FileDataString[i]).Length > 100)
                    throw new Exception("Word to search is too big");
                else
                    words.Add(FileDataString[i]);
            return words;
        }
        public Node[,] BoardCreation(int N, List<char> FileDataChar)
        {
            Node[,] board = new Node[N, N];
            int start = 0;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    board[i, j] = new Node(FileDataChar[start], false, null);
                    start++;
                }
            return board;
        }
        public bool IsSafe(int[] coordinates,int FirstOperation,int SecondOperation, int bounds,bool processed) 
        {
            bool check = true;
            if((coordinates[0] + FirstOperation >= bounds || coordinates[0] + FirstOperation < 0)
                || (coordinates[1] + SecondOperation >= bounds || coordinates[1] + SecondOperation < 0) || processed)
                check = false;
            return check;
        }
        public bool DeepSearch(int[] coordinates,int LetterNumber,char[] word, int[] row, int[] col,Node[,] board, int bounds)
        {
            bool find = false;
            int currCord1, currCord2;
            LetterNumber++;
            if (LetterNumber == word.Length && board[coordinates[0],coordinates[1]].Letter==word[word.Length - 1])
                find = true;
            else
                for (int i = 0; i < 4; i++)
                {
                    if(IsSafe(coordinates, row[i],col[i],bounds,board[coordinates[0], coordinates[1]].Taken))
                        if (board[coordinates[0] + row[i], coordinates[1] + col[i]].Letter == word[LetterNumber])
                        {
                            coordinates[0] = currCord1 = coordinates[0] + row[i]; 
                            coordinates[1] = currCord2 = coordinates[1] + col[i];

                            find = DeepSearch(coordinates, LetterNumber, word, row, col, board, bounds);
                            if (find)
                            {
                                board[currCord1, currCord2].Taken = true;
                                board[currCord1, currCord2].Word = new string(word);
                                break;
                            }


                        } 
                }
            return find;
        }
        public void SearchWord(HashSet<string> words, Node[,]board,int N) 
        {
            //Movement Up,left,right,down
            int[] row = { -1, 0, 0, 1 };
            int[] col = { 0, -1, 1, 0 };
            foreach (string word in words)
            {
                char[] CharArray = word.ToCharArray();
                //First two loops to find postion of first letter
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        int LetterNumber = 0;
                        if (CharArray[LetterNumber] == board[i, j].Letter)
                        {
                            int[] coordinates = { i, j };
                            bool check = DeepSearch(coordinates,LetterNumber,CharArray, row, col, board, N);
                            if (check)
                            {
                                board[i, j].Taken = true;
                                Console.WriteLine("Word was found");
                            }
                            else
                                Console.WriteLine("Word wasn't found");
                            break;
                        }
                    }
                    break;
                }
            }
        }
		static void Main(string[] args)
        {
            List<char> FileDataChar = new List<char>();
            Program program = new Program();
            string[] FileDataString;
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
            foreach (string data in FileDataString)
                foreach (char c in data)
                    if (c != ' '&& c != '-')
                        FileDataChar.Add(c);

            //inialise N( SIZE OF MARIX) and M( AMOUNT OF WORDS TO FIND IN MATRIX) from FileDataChar
            int N = int.Parse(FileDataChar[0].ToString());
            FileDataChar.RemoveAt(0);
            int M = int.Parse(FileDataChar[0].ToString());
            FileDataChar.RemoveAt(0);

            if (N + M != FileDataString.Length-1)
                throw new Exception("Not enogh data");

            var board = program.BoardCreation(N, FileDataChar);

            var words = program.GetWords(N,M, FileDataString);
            
            program.SearchWord(words, board,N);
            

            List<char> result = new List<char>();
            foreach(Node x in board)
            {
                if(x.Taken == false)
                {
                    result.Add(x.Letter);
                }
            }
            result.Sort();
            string STRINGresult = new string(result.ToArray());
            if (File.Exists(pathWRITE))
            {
                File.WriteAllText(pathWRITE, STRINGresult);
            }
            else
                throw new Exception("No file to write to");
        }
    }
}
