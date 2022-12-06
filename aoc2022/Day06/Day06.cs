using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day06
{
    internal class Day06
    {
        public void Run()
        {
            var data = File.ReadAllLines(@"Day06\input.txt")[0];

            var ans1 = FindStart(data, 4);
            var ans2 = FindStart(data, 14);
        }

        public int FindStart(string input, int length)
        {
            for (int i = 0; i < input.Length - length; i++)
            {
                var a = input.Substring(i, length);

                if (a.Distinct().Count() == length)
                {
                    return i + length;
                }
            }
            return 0;
        }
    }
}
