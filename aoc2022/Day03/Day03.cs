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
            var ans1_ = Part1UsingHashSets(data);
            var ans2 = Part2(data);
            var ans2_ = Part2UsingHashSets(data);

        }

        private int charToInt(char c) => c switch
        {
            >= 'a' and <= 'z' => c - 'a' + 1,
            >= 'A' and <= 'Z' => c - 'A' + 27,
            _ => 0
        };

        private int charToInt_old(char c)
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
                    arr[i] = charToInt_old(line[i]);
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
                                total += charToInt_old(elf1[i]);
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

        public int Part1UsingHashSets(string[] data)
        {
            var total = 0;
            foreach (var line in data)
            {
                var compartment1 = line.Substring(0,line.Length/2);
                var compartment2 = line.Substring(line.Length / 2);

                var compartment1set = new HashSet<char>(compartment1);
                var compartment2set = new HashSet<char>(compartment2);
                var commonitems = compartment1set.Intersect(compartment2set);

                foreach (var item in commonitems)
                {
                    total += charToInt(item);
                }

            }

            return total;
        }


        public int Part2UsingHashSets(string[] data)
        {
            var counter = 0;
            var total = 0;
            while (counter < data.Length)
            {
                var elf1 = new HashSet<char>(data[counter]);
                var elf2 = new HashSet<char>(data[counter + 1]);
                var elf3 = new HashSet<char>(data[counter + 2]);
                counter += 3;

                var commonItem = elf1.Intersect(elf2).Intersect(elf3).First();
                total += charToInt(commonItem);
            }

            return total;
        }

    }

}
