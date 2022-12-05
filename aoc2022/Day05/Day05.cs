using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day05
{
    internal class Day05
    {
        public void Run()
        {
            var input = File.ReadAllLines(@"Day05\input.txt");
            //var input = "    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2".Split("\r\n");


            var stacks = GetStacks(input);
            var moves = GetMoves(input);

            var ans1 = Part1(GetStacks(input), moves); 
            var ans2 = Part2(GetStacks(input), moves); 
        }

        public string Part1(Stack<char>[] stacks, string[] moves)
        {
            foreach (var move in moves)
            {
                if (string.IsNullOrEmpty(move)) continue;
                var m = move.Split(" ");
                for (var i = 0; i < int.Parse(m[1]); i++)
                {
                    var crate = stacks[int.Parse(m[3])].Pop();
                    stacks[int.Parse(m[5])].Push(crate);

                }
            }

            var ans = string.Empty;
            for (var i = 1; i < stacks.Length; i++)
            {
                ans += stacks[i].Peek();
            }
            return ans;
        }

        public string Part2(Stack<char>[] stacks, string[] moves)
        {
            foreach (var move in moves)
            {
                if (string.IsNullOrEmpty(move)) continue;
                var m = move.Split(" ");
                var temp = new Stack<char>();
                for (var i = 0; i < int.Parse(m[1]); i++)
                {
                    var crate = stacks[int.Parse(m[3])].Pop();
                    temp.Push(crate);
                }
                for (var i = 0; i < int.Parse(m[1]); i++)
                {
                    var crate = temp.Pop();
                    stacks[int.Parse(m[5])].Push(crate);
                }
            }

            var ans = string.Empty;
            for (var i = 1; i < stacks.Length; i++)
            {
                ans += stacks[i].Peek();
            }

            return ans;
        }

        private Stack<char>[] GetStacks(string[] input)
        {
            var numberOfStacks = (input[0].Length + 1) / 4;
            var stacks = new Stack<char>[numberOfStacks + 1];
            for (var i= 0; i< stacks.Length; i++)
            {
                stacks[i] = new Stack<char>();
            }

            var stackInput = new List<string>();
            foreach (var line in input)
            {
                if(string.IsNullOrEmpty(line)) break;
                stackInput.Add(line);
            }

            stackInput.Reverse();

            var row = 0;
            foreach (var line in stackInput)
            {
                row++;
                
                if (row == 1)
                {
                    continue;
                }

                var stack = 1;
            
                for (var col = 1; col <= line.Length; col +=4)
                {
                    var crate = line[col];
                    if (crate != ' ')
                    {
                        stacks[stack].Push(crate);
                    }
                   
                    stack++;
                }
            }

            return stacks;
        }

        private string[] GetMoves(string[] input)
        {
            var moves = new List<string>();
            var isMove = false;
            foreach (var line in input)
            {
                if (isMove)
                {
                    moves.Add(line);
                }

                if (string.IsNullOrEmpty(line))
                {
                    isMove = true;

                }
            }

            return moves.ToArray();
        }
    }
}
