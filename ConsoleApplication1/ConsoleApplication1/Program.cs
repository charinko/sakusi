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
        public static void changeHead(string fileName,StreamWriter writer)
        {

            writer.WriteLine(fileName);
            writer.WriteLine("0,0,120,0,0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="fileName"></param>
        public static void changeMero(double[] line, StreamWriter writer)
        {
            string s = "";
            string[] oto = new string[list.Length];
            for (int n = 0; n < line.Length; n++)
            {
                for (int j = 0; j < list.Length; j++)
                {
                    if (line[n] == list[j])
                    {
                        oto[j] = "5";
                    }
                    else
                    {
                        if (oto[j] == "5")
                        {
                            oto[j] = "5";
                        }
                        else
                        {
                            oto[j] = "-";
                        }
                    }
                }

            }
            foreach (string item in oto)
            {
                s = s + Convert.ToString(item);
            }

            writer.WriteLine(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static double[][] includeFile(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                //signal
                List<double[]> y = new List<double[]>();
                while (reader.EndOfStream == false)
                {
                    List<double> tmp = new List<double>();
                    foreach (string item in reader.ReadLine().Split(','))
                    {
                        if (item != "")
                        {
                            tmp.Add(Convert.ToDouble(item));
                        }
                        else
                        {
                            tmp.Add(0);
                        }

                    }
                    y.Add(tmp.ToArray());
                }
                return y.ToArray();
            }
        }


        static void Main(string[] args)
        {
            //Console.WriteLine(changeMero(list[0]));
            //Console.ReadKey();
            string root = @"..\..\";
            string readfilename = root + @"音フォルダ\targetarray.txt";
            string outfilename = root + @"音フォルダ\test.txt";
            y = includeFile(readfilename);
            using (FileStream fw = new FileStream(outfilename, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fw))
                {
                    changeHead(@"..\..\音フォルダ\test.txt", writer);
                    for (int i = 0; i < y.Length; i++)
                    {
                        changeMero(y[i], writer);
                        if (i % 24 == 0)
                        {
                            writer.WriteLine("0000000000000000000000000000000000000000000000000000000000000000000000009");
                        }
                    }
                }
            }
        }
    }

}
