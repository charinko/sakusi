using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        public static double[] list =
                            {
                65.41, 69.3, 73.42, 77.78, 82.41, 87.31,
                92.5, 98, 103.83, 110, 116.54, 123.47, 130.81, 138.59, 146.83, 155.56,
                164.81, 174.61, 185, 196, 207.65, 220, 233.08, 246.94, 261.63, 277.18,
                293.66, 311.13, 329.63, 349.23, 369.99, 392, 415.3, 440, 466.16, 493,
                523.25, 554.37, 587.33, 622.25, 659.26, 698.46, 739.99, 783.99, 830.61, 880, 932.33, 987.77,
                1046.5, 1108.73, 1174.66, 1244.51, 1318.51, 1396.91, 1479.98, 1567.98, 1661.22, 1760, 1864.66, 1975.53,
                2093, 2217.46, 2349.32, 2489.02, 2637.02, 2793.83, 2959.96, 3135.96, 3322.44, 3520, 3729.31, 3951.07,
                4186.01
                };
        private static double[][] y;

        /// <summary>
        /// midiの再生に必要な情報を1,2行目に書き込みます。
        /// 
        /// </summary>
        /// <param name="fileName">出力先のファイルです。</param>
        public static void changeHead(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter writer = new StreamWriter(fs);

            writer.WriteLine(fileName);
            writer.WriteLine("0,0,120,0,0");

            writer.Close();
            fs.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="fileName"></param>
        public static void changeMero(double[] i, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Append);
            StreamWriter writer = new StreamWriter(fs);

            string s = "";
            int[] t = new int[list.Length];
            for (int n = 0; n < list.Length; n++)
            {
                for (int j = 0; j < i.Length; j++)
                {
                    if (i[j] == list[n])
                    {
                        t[n] = 5;
                    }
                    else
                    {
                        t[n] = 0;
                    }
                }
            }
            foreach (char item in t)
            {
                s =s + Convert.ToString(t[item]);
            }

            writer.WriteLine(s);

            string f = "";
            for(int n = 0; n < list.Length; n++)
            {
                f = f + "-";
            }

            for (int m = 0; m < 6; m++)
            {
                writer.WriteLine(f);
            }
            writer.Close();
            fs.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static double[][] includeFile(string fileName)
        {
            //signal
            List<double[]> y = new List<double[]>();

                using (StreamReader koeFile = new StreamReader(fileName))
            {
                List<double> tmp = new List<double>();
                //
                while (koeFile.EndOfStream == false)
               {
                    foreach (string item in koeFile.ReadLine().Split(',')) {
                        if (item != "")
                        {
                            tmp.Add(Convert.ToDouble(item));
                        }
                        else
                        {
                            tmp.Add(0);
                        }
                    }
                }
                    
                //
                y.Add(tmp.ToArray());
            }
            return y.ToArray();
        }


        static void Main(string[] args)
        {
            //Console.WriteLine(changeMero(list[0]));
            //Console.ReadKey();
            changeHead(@"C:\Users\チャリンコ\Desktop\test.txt");
            y = includeFile(@"C:\Users\チャリンコ\Desktop\targetarray.txt");
            for (int i = 0; i < y.Length; i++)
            {
                changeMero(y[i], @"C:\Users\チャリンコ\Desktop\test.txt");
            }

            
        }
    }
}
