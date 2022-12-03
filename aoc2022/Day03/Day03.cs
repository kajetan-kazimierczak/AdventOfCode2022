using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day03
{
    internal class Day03
    {
        public void Run()
        {
            var data = File.ReadAllLines(@"Day03\input.txt");
            //var data = "vJrwpWtwJgWrhcsFMMfFFhFp\r\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\r\nPmmdzqPrVvPwwTWBwg\r\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\r\nttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw".Split("\r\n");

            var ans1 = Part1(data);
            var ans2 = Part2(data);

        }


        private int charToInt(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return c - 'a' + 1;
            }

            if (c >= 'A' && c <= 'Z')
            {
                return c - 'A' + 27;
            }

            return 0;
        }

        public int Part1(string[] data)
        {
            var total = 0;
            var current = 0;

            foreach (var line in data)
            {
                current = 0;

                var arr = new int[line.Length];
                for (var i = 0; i < line.Length; i++)
                {
                    arr[i] = charToInt(line[i]);
                }

                var half = line.Length / 2;
                var used = new List<int>();

                for (var i = 0; i < half; i++)
                {
                    for (var j = half; j < line.Length; j++)
                    {
                        if (arr[i] == arr[j] && !used.Contains(arr[i]))
                        {
                            current += arr[i];
                            used.Add(arr[i]);
                            break;
                        }
                    }
                }

                total += current;
            }

            return total;
        }

        public int Part2(string[] data)
        {
            var counter = 0;
            var total = 0;
            while (counter < data.Length)
            {
                var elf1 = data[counter];
                var elf2 = data[counter + 1];
                var elf3 = data[counter + 2];
                counter += 3;

                // find item that all elfs have in common
                var found = false;
                for (var i = 0; i < elf1.Length; i++)
                {
                    for (var j = 0; j < elf2.Length; j++)
                    {
                        for (var k = 0; k < elf3.Length; k++)
                        {
                            if (elf1[i] == elf2[j] && elf2[j] == elf3[k])
                            {
                                total += charToInt(elf1[i]);
                                found = true;
                                break;
                            }
                        }

                        if (found)
                        {
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }

            }

            return total;
        }
    }

    

   
}
