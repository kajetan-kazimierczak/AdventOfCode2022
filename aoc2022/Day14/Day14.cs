
namespace aoc2022.Day14
{
    internal class Day14
    {
        char[,] _cave;

        public void Run()
        {
            var data = "498,4 -> 498,6 -> 496,6\r\n503,4 -> 502,4 -> 502,9 -> 494,9".Split("\r\n");

            data = File.ReadAllLines(@"Day14\input.txt");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }


        int Part1(string[] data)
        {
            SetupCave(data);
            var grains = 0;
            while (DropSandToAbyss() != true)
            {

                grains++;
                // DebugCave();
                // Console.WriteLine("Grains: " + grains);
            }

            return grains;
        }

        int Part2(string[] data)
        {
            SetupCave(data);
            SetupFloor(data);
            var grains = 0;
            while (DropSandTillStop() != true)
            {

                grains++;

            }
            // DebugCave();
            return grains + 1;


        }

        void SetupFloor(string[] data)
        {

            var highest = 0;
            foreach (var s in data)
            {
                var points = s.Split(" -> ");
                for (var i = 0; i < points.Length; i++)
                {

                    var current = points[i].Split(",").Select(x => int.Parse(x)).ToArray();
                    if (current[1] > highest) highest = current[1];
                }
            }

            for (var i = 0; i < 1000; i++)
            {
                _cave[i, highest + 2] = '#';
            }
        }


        

        void DebugCave()
        {



            for (int i = 0; i < 200; i++)
            {
                for (int j = 470; j < 585; j++)
                {
                    Console.Write(_cave[j, i] == 0 ? '.' : _cave[j, i]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        bool DropSandTillStop()
        {
            var sand = (x: 500, y: 0);

            while (true)
            {
                var newPos = (x: sand.x, y: sand.y + 1);
                if (newPos.y >= 1000) return true; //abyss

                if (_cave[newPos.x, newPos.y] != 0)
                {
                    newPos.x -= 1;
                }
                else
                {
                    sand = newPos;
                    continue;
                }

                if (_cave[newPos.x, newPos.y] != 0)
                {
                    newPos.x += 2;
                    if (_cave[newPos.x, newPos.y] == 0)
                    {

                        sand = newPos;
                        continue;
                    }
                }
                else
                {
                    sand = newPos;
                    continue;
                }

                if (sand.y == 0)
                {
                    return true;
                }

                _cave[sand.x, sand.y] = 'o';
                return false;

            }

            return false;
        }


        bool DropSandToAbyss()
        {
            var sand = (x: 500, y: 0);

            while (true)
            {
                var newPos = (x: sand.x, y: sand.y + 1);
                if (newPos.y >= 1000) return true; //abyss

                if (_cave[newPos.x, newPos.y] != 0)
                {
                    newPos.x -= 1;
                }
                else
                {
                    sand = newPos;
                    continue;
                }

                if (_cave[newPos.x, newPos.y] != 0)
                {
                    newPos.x += 2;
                    if (_cave[newPos.x, newPos.y] == 0)
                    {

                        sand = newPos;
                        continue;
                    }
                }
                else
                {
                    sand = newPos;
                    continue;
                }

                _cave[sand.x, sand.y] = 'o';
                return false;



            }

            return false;
        }

        void SetupCave(string[] data)
        {
            _cave = new char[1000, 1000];

            foreach (var s in data)
            {
                var points = s.Split(" -> ");
                for (var i = 1; i < points.Length; i++)
                {
                    var previous = points[i - 1].Split(",").Select(x => int.Parse(x)).ToArray();
                    var current = points[i].Split(",").Select(x => int.Parse(x)).ToArray();

                    if (previous[0] == current[0])
                    {
                        for (var j = Math.Min(previous[1], current[1]);
                             j <= Math.Max(previous[1], current[1]);
                             j++)
                        {
                            _cave[previous[0], j] = '#';
                        }
                    }
                    else
                    {
                        for (var j = Math.Min(previous[0], current[0]);
                             j <= Math.Max(previous[0], current[0]);
                             j++)
                        {
                            _cave[j, previous[1]] = '#';
                        }
                    }
                }
            }

        }
    }
}
