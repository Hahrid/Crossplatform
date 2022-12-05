using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace All_Labs
{
    public class PR3
    {
        public static string Main(string[] args)
        {
            const int INF = 1000000000;
            int n, START, FINISH, r;

/*            string pathREAD = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Data.txt");
            string pathWRITE = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../OUTPUT.txt");*/
            string[] FileDataString = args;

/*            if (File.Exists(pathWRITE))
            {
                FileDataString = File.ReadAllLines(pathREAD);
            }
            else
                throw new Exception("No such file exists");*/

            try
            {
                n = Convert.ToInt32(FileDataString[0]);
            }
            catch
            {
                throw new Exception("First element N is Not a intager or more than one number");
            }
            try
            {
                START = Convert.ToInt32(FileDataString[1].Split(" ")[0]);
            }
            catch
            {
                throw new Exception("start village is Not a intager or more than two numbers");
            }
            try
            {
                FINISH = Convert.ToInt32(FileDataString[1].Split(" ")[1]);
            }
            catch
            {
                throw new Exception("finish village is Not a intager or more than two numbers");
            }
            try
            {
                r = Convert.ToInt32(FileDataString[2]);
                if (r != FileDataString.Length-3)
                    throw new Exception("number of villages is not equalls as stated");
            }
            catch
            {
                throw new Exception("number of bus trips is Not a intager or more than one number");
            }

            


            int[,] dataExtraction = new int[r,n+1];
            for (int i = 0; i < r; i++)
            {
                var temp = FileDataString[i + 3].Split(" ");
                for (int j = 0; j < 4; j++)
                {
                    dataExtraction[i,j] = Convert.ToInt32(temp[j]);
                }
                
            }
            List<List<Tuple<int, int, int>>> data = new List<List<Tuple<int, int, int>>>( new List<Tuple<int, int, int>>[n+1] );
            for (int i = 0; i < 4; i++)
            {
                data[i] = new List<Tuple<int, int, int>>();
            }
            for (int i = 0; i < r; i++)
            {
                int start = dataExtraction[i, 0];
                int start_time = dataExtraction[i, 1];
                int finish = dataExtraction[i, 2];
                int finish_time = dataExtraction[i, 3];
                data[start].Add(new Tuple<int, int, int>(start_time, finish, finish_time));

                
            }


            List<int> time = Enumerable.Repeat(INF, n+1).ToList();
            for (int i = 0; i < n + 1; i++)
                time.Add(INF);
            time[START] = 0;
            List<bool> used = Enumerable.Repeat(false, n + 1).ToList();
            int min_time = 0;
            int min_village = START;


            while (min_time < INF)
            {
                used[min_village] = true;
                int start = min_village;
                foreach (var v in data[start])
                {
                    int start_time = v.Item1;
                    int finish = v.Item2;
                    int finish_time = v.Item3;
                    if (time[start] <= start_time && finish_time<time[finish])
                        time[finish] = finish_time;
                }
                min_time = INF;
                for (int v = 0; v < n; ++v)
                    if (!used[v] && time[v] < min_time)
                    {
                        min_time = time[v];
                        min_village = v;
                    }
            }
            /*            if (File.Exists(pathWRITE))
                        {
                            File.WriteAllText(pathWRITE, Convert.ToString(time[FINISH]));
                        }
                        else
                            throw new Exception("No file to write to");*/
            return Convert.ToString(time[FINISH]);
        }
    }
}
