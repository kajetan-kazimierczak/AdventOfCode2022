using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day04
{
    internal class Day04
    {
        public void Run()
        {
            var data = File.ReadAllLines(@"Day04\input.txt");
            //var data = "2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8".Split("\r\n");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        public int Part1(string[] data)
        {
            var result = 0;

            foreach (var line in data)
            {
                var elfs = line.Split(",");
                var elf1 = elfs[0].Split("-");
                var elf2 = elfs[1].Split("-");

                if (int.Parse(elf1[0]) <= int.Parse(elf2[0]) && int.Parse(elf1[1]) >= int.Parse(elf2[1])
                    || int.Parse(elf2[0]) <= int.Parse(elf1[0]) && int.Parse(elf2[1]) >= int.Parse(elf1[1]))
                {
                    result++;
                }

            }
            return result;
        }

        public int Part2(string[] data)
        {
            var result = 0;

            foreach (var line in data)
            {
                var elfs = line.Split(",");
                var elf1 = elfs[0].Split("-");
                var elf2 = elfs[1].Split("-");


                if (int.Parse(elf1[1]) >= int.Parse(elf2[0]) && int.Parse(elf1[0]) <= int.Parse(elf2[1]))
                {
                    result++;
                }

            }
            return result;
        }
    }
}
