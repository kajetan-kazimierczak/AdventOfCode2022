

using System.Runtime.CompilerServices;
using System.Text;

namespace aoc2022.Day17
{
    internal class Day17
    {
        private List<List<string>> _rocks = new();
        private int _currentRock = 0;
        private int _currentJet;
        private char[,] _chamber = new char[ 7, 100000];

        public void Run()
        {
            var data = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
            data = File.ReadAllLines(@"Day17\input.txt")[0];
            Setup();
            var ans1 = Part1(data);

        }

        public int Part1(string jets)
        {
            var highestRock = -1;
            var currentJet = 0;
            
            
            for (var i = 1;i <= 2022; i++)
            {
                // Each rock appears so that its left edge is two units away from the left wall
                // and its bottom edge is three units above the highest rock in the room
                var rock = GetNextRockAndInitialPosition(highestRock);
                var previousPosition =  rock.y;
                var stationary = false;

                while (!stationary)
                {
                    switch (jets[currentJet])
                    {
                        case '>':
                            rock = MoveRight(rock);
                            break;
                        case '<':
                            rock = MoveLeft(rock);
                            break;

                    }

                    //Debug(rock);
                    rock = MoveDown(rock);
                    //Debug(rock);
                    if (rock.y == previousPosition)
                    {
                        stationary = true;
                        if (rock.y > highestRock) highestRock = rock.y;


                        for (var y = 0; y < rock.shape.Count; y++)
                        {
                            for (var x = 0; x < rock.shape[y].Length; x++)
                            {
                                if (rock.shape[y][x] == '#')
                                {
                                    _chamber[rock.x + x, rock.y - y] = '#';
                                 
                                }
                            }
                        }


       //                Debug();
                    }

                    currentJet++;
                    if (currentJet >= jets.Length) currentJet = 0;
                    previousPosition = rock.y;
                }
           
            }
            return highestRock + 1; // 0-based
        }

        private (List<string> shape, int x, int y) MoveDown((List<string> shape, int x, int y) rock)
        {

            if (rock.y - rock.shape.Count < 0) return rock;

            for (var y = 0; y < rock.shape.Count; y++)
            {
                for (var x = 0; x < rock.shape[y].Length; x++)
                {
                    if (rock.shape[y][x] == '#')
                    {
                        if (_chamber[rock.x + x, rock.y - y -1] == '#')
                        {
                            return rock;
                        }
                    }
                }
            }

            rock.y--;
            return rock;
        }

        private (List<string> shape, int x, int y) MoveLeft((List<string> shape, int x, int y) rock)
        {
            if (rock.x - 1 <  0) return rock;

            for (var y = 0; y < rock.shape.Count; y++)
            {
                for (var x = 0; x < rock.shape[y].Length; x++)
                {
                    if (rock.shape[y][x] == '#')
                    {
                        if (_chamber[rock.x + x - 1, rock.y - y] == '#')
                        {
                            return rock;
                        }
                    }
                }
            }

            rock.x--;
            return rock;
        }

        private (List<string> shape, int x, int y) MoveRight((List<string> shape, int x, int y) rock)
        {
            if (rock.x + 1 + rock.shape[0].Length > 7) return rock;

            for (var y = 0; y < rock.shape.Count; y++)
            {
                for (var x = 0; x < rock.shape[y].Length; x++)
                {
                    if (rock.shape[y][x] == '#')
                    {
                        if (_chamber[rock.x + x + 1, rock.y - y] == '#')
                        {
                            return rock;
                        }
                    }
                }
            }

            rock.x++;
            return rock;
        }

        private (List<string> shape, int x, int y) GetNextRockAndInitialPosition(int highestRock)
        {
            var rock = _rocks[_currentRock];
            var x = 2;
            var y = highestRock + 4 + rock.Count - 1;
            _currentRock++;
            if (_currentRock >= _rocks.Count) _currentRock = 0;
            return (rock, x, y);
        }


        #region Setup
        /*
####

.#.
###
.#.

..#
..#
###

#
#
#
#

##
##
*/
        private void Setup()
        {
            _rocks.Add(new List<string> { "####" });
            _rocks.Add(new List<string> { ".#.", "###", ".#." });
            _rocks.Add(new List<string> { "..#", "..#", "###" });
            _rocks.Add(new List<string> { "#", "#", "#", "#" });
            _rocks.Add(new List<string> { "##", "##" });

            // reverse beause of coordinate system
            //_rocks.ForEach(r => r.Reverse());


        }


   

        private void Debug()
        {
            Console.Clear();
            for (var y = 50; y >= 0; y--)
            {
                for (var x = 0; x < 7; x++)
                {
                    Console.Write(_chamber[x, y] == '#' ? '#' : '.');
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
#endregion
}
