using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lab_2_crossplatform
{
    class Program
    {
        public static void Swap(List<Gangster> list, int indexA, int indexB)
        {
            Gangster tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        void SortList(List<Gangster> list,int n)
        {
            // O(n*n) - сортировка выбором
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (list[i].T > list[j].T)
                    {
                        Swap(list,i, j);
                    }
                }
        }
        void solve(List<Gangster> list,int n, int[] maxP)
        {
            for (int i = 0; i < n; i++)
            {
                int curMaxPayment = list[i].P; 
                for (int j = 0; j < i; j++)
                {
                    if (maxP[j] != 0) 
                    {
                        int dt = list[i].T - list[j].T;
                        int dk = Math.Abs(list[i].S - list[j].S);
                        if (dk <= dt) 
                            curMaxPayment = Math.Max(curMaxPayment, maxP[j] + list[i].P);
                    }
                }
                maxP[i] = curMaxPayment;
            }
        }
        static void Main(string[] args)
        {
            List<Gangster> gangsters = new List<Gangster>();
            
            //READ AND WRITE DATA FROM FILE 
            string pathREAD = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Data2.txt");
            string pathWRITE = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../OUTPUT.txt");
            string[] FileDataString;
            if (File.Exists(pathWRITE))
            {
                FileDataString = File.ReadAllLines(pathREAD);
            }
            else
                throw new Exception("No such file exists");
            
            string[] N_K_T_string = FileDataString[0].Split(" ");
            if(N_K_T_string.Length > 3)
            {
                throw new Exception("Too many variables");
            }
            else
            {
                foreach(var x in N_K_T_string)
                    try
                    {
                        int temp = Convert.ToInt32(x);
                        if (temp > 100 || temp < 1)
                            throw new Exception("One element is not in range 1<=N<=100");
                    }
                    catch { throw new Exception("One element is not int"); }
            }
            int[] N_K_T = { 
                Convert.ToInt32(N_K_T_string[0]), 
                Convert.ToInt32(N_K_T_string[1]), 
                Convert.ToInt32(N_K_T_string[2]) };

            for (int i=0;i< N_K_T[0]; i++)
            {
                int t, p, s;
                t = Convert.ToInt32(FileDataString[1].Split(" ")[i]);
                p = Convert.ToInt32(FileDataString[2].Split(" ")[i]);
                s = Convert.ToInt32(FileDataString[3].Split(" ")[i]);

                if (t<s)
                {
                    p = 0;
                }

                gangsters.Add(new Gangster(t, p, s));
            }
            int[] maxP = new int[N_K_T[0]];
            Program program = new Program();
            program.SortList(gangsters, N_K_T[0]);
            program.solve(gangsters, N_K_T[0],maxP);
            string STRINGresult = Convert.ToString(maxP.Max());
            if (File.Exists(pathWRITE))
            {
                File.WriteAllText(pathWRITE, STRINGresult);
            }
            else
                throw new Exception("No file to write to");
        }
    }
}
