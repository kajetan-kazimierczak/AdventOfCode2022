namespace aoc2022.Day10
{
    internal class Day10
    {

        public void Run()
        {
            var data = File.ReadAllLines(@"Day10\input.txt");
            //data = "addx 15\r\naddx -11\r\naddx 6\r\naddx -3\r\naddx 5\r\naddx -1\r\naddx -8\r\naddx 13\r\naddx 4\r\nnoop\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx 5\r\naddx -1\r\naddx -35\r\naddx 1\r\naddx 24\r\naddx -19\r\naddx 1\r\naddx 16\r\naddx -11\r\nnoop\r\nnoop\r\naddx 21\r\naddx -15\r\nnoop\r\nnoop\r\naddx -3\r\naddx 9\r\naddx 1\r\naddx -3\r\naddx 8\r\naddx 1\r\naddx 5\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\naddx -36\r\nnoop\r\naddx 1\r\naddx 7\r\nnoop\r\nnoop\r\nnoop\r\naddx 2\r\naddx 6\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\nnoop\r\naddx 1\r\nnoop\r\nnoop\r\naddx 7\r\naddx 1\r\nnoop\r\naddx -13\r\naddx 13\r\naddx 7\r\nnoop\r\naddx 1\r\naddx -33\r\nnoop\r\nnoop\r\nnoop\r\naddx 2\r\nnoop\r\nnoop\r\nnoop\r\naddx 8\r\nnoop\r\naddx -1\r\naddx 2\r\naddx 1\r\nnoop\r\naddx 17\r\naddx -9\r\naddx 1\r\naddx 1\r\naddx -3\r\naddx 11\r\nnoop\r\nnoop\r\naddx 1\r\nnoop\r\naddx 1\r\nnoop\r\nnoop\r\naddx -13\r\naddx -19\r\naddx 1\r\naddx 3\r\naddx 26\r\naddx -30\r\naddx 12\r\naddx -1\r\naddx 3\r\naddx 1\r\nnoop\r\nnoop\r\nnoop\r\naddx -9\r\naddx 18\r\naddx 1\r\naddx 2\r\nnoop\r\nnoop\r\naddx 9\r\nnoop\r\nnoop\r\nnoop\r\naddx -1\r\naddx 2\r\naddx -37\r\naddx 1\r\naddx 3\r\nnoop\r\naddx 15\r\naddx -21\r\naddx 22\r\naddx -6\r\naddx 1\r\nnoop\r\naddx 2\r\naddx 1\r\nnoop\r\naddx -10\r\nnoop\r\nnoop\r\naddx 20\r\naddx 1\r\naddx 2\r\naddx 2\r\naddx -6\r\naddx -11\r\nnoop\r\nnoop\r\nnoop".Split("\r\n");

            var ans1 = Part1(data);
            var ans2 = Part2(data); // see console output
        }



        public int Part1(string[] data)
        {
            var clock = 1;
            var X = 1;
            var sum = 0;

            foreach (var line in data)
            {
                if (line == "noop")
                {
                    clock++;
                }
                else
                {
                    var instr = line.Split(" ");
                    clock++;
                    sum = UpdateReading(clock, X, sum);
                    X += int.Parse(instr[1]);
                    clock++;
                }
                sum = UpdateReading(clock, X, sum);
            }

            return sum;
        }

        public int Part2(string[] data)
        {
            var clock = 1;
            var X = 1;

            foreach (var line in data)
            {
                DrawPixel(clock, X);
                if (line == "noop")
                {
                    clock++;
                }
                else
                {
                    var instr = line.Split(" ");
                    clock++;
                    DrawPixel(clock, X);
                    X += int.Parse(instr[1]);
                    clock++;
                }

            }

            return 0;
        }


        private void DrawPixel(int clockCycle, int x)
        {
            var position = (clockCycle - 1) % 40;
            bool pixel = (position >= x - 1 && position <= x + 1);

            Console.Write(pixel ? "#" : ".");
            if (position == 39) Console.WriteLine("");

        }

        private int UpdateReading(int clockCycle, int x, int sum)
        {
            if ((clockCycle - 20) % 40 == 0)
            {
                sum += clockCycle * x;
                // Console.WriteLine($"{clockCycle}, X = {x}");
            }

            return sum;
        }
    }
}
