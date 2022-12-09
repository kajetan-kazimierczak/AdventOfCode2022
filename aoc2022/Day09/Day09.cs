namespace aoc2022.Day09
{
    internal class Day09
    {
        public void Run()
        {
            var data = File.ReadAllLines(@"Day09\input.txt");
            //data = "R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2".Split("\r\n");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        public int Part1(string[] data)
        {
            (int x, int y) H = (0, 0);
            (int x, int y) T = (0, 0);
            HashSet<(int, int)> tailTrail = new HashSet<(int, int)>();


            foreach (var line in data)
            {
                var cmd = line.Split(" ");

                for (var i = 0; i < int.Parse(cmd[1]); i++)
                {
                    H = MoveHead(cmd[0], H);
                    T = MoveTail(H, T);
                    tailTrail.Add(T);
                }
            }

            return tailTrail.Count;
        }
        public int Part2(string[] data)
        {
            HashSet<(int, int)> tailTrail = new HashSet<(int, int)>();
            var rope = new (int x, int y)[10];


            foreach (var line in data)
            {
                var cmd = line.Split(" ");

                for (var i = 0; i < int.Parse(cmd[1]); i++)
                {
                    rope[0] = MoveHead(cmd[0], rope[0]);
                    for (var j = 1; j < rope.Length; j++)
                    {
                        rope[j] = MoveTail(rope[j - 1], rope[j]);
                    }


                    tailTrail.Add(rope[9]);
                }
            }

            return tailTrail.Count;
        }


        private static (int x, int y) MoveHead(string direction, (int x, int y) h)
        {
            switch (direction)
            {
                case "U":
                    h.x -= 1;
                    break;
                case "D":
                    h.x += 1;
                    break;
                case "L":
                    h.y -= 1;
                    break;
                case "R":
                    h.y += 1;
                    break;
                default:
                    break;
            }

            return h;
        }

        private static (int x, int y) MoveTail((int x, int y) h, (int x, int y) t)
        {
            if (Math.Abs(h.x - t.x) < 2 && Math.Abs(h.y - t.y) < 2) return t;

            if (h.x > t.x) t.x++;
            else if (h.x < t.x) t.x--;

            if (h.y > t.y) t.y++;
            else if (h.y < t.y) t.y--;

            return t;
        }
    }
}
