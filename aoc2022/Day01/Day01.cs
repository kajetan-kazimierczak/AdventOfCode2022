namespace aoc2022.Day01
{
    internal class Day01
    {

        public void Run()
        {
            //var data = "1000\r\n2000\r\n3000\r\n\r\n4000\r\n\r\n5000\r\n6000\r\n\r\n7000\r\n8000\r\n9000\r\n\r\n10000".Split("\r\n");

            var data = File.ReadAllLines(@"Day01\input.txt");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        public int Part1(string[] data)
        {
          
            var highest = 0;
            var current = 0;

            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (current > highest)
                    {
                        highest = current;
                    }

                    current = 0;
                }
                else
                {
                    current += int.Parse(line);
                }
            }
            if (current > highest)
            {
                highest = current;
            }

            return highest;
        }

        public int Part2(string[] data)
        {
        
            var all = new List<int>();
            var current = 0;

            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    all.Add(current);
                    current = 0;
                }
                else
                {
                    current += int.Parse(line);
                }
            }
            all.Add(current);

            return all.OrderByDescending(x => x).Take(3).Sum();
        }
    }
}
