namespace aoc2022.Day12
{
    internal class Day12
    {
        List<Node> _graph = new List<Node>();

        int[,] _intgrid;
        Point[,] _grid;
        public void Run()
        {
            var data = "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi".Split("\r\n");

            data = File.ReadAllLines(@"Day12\input.txt");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        public int Part1(string[] data)
        {
            // 1
            Point start = new Point();
            Point goal = new Point();
            Initialize(data);

            foreach (var point in _grid)
            {
                if (point.IsStart) start = point;
                if (point.IsEnd) goal = point;
            }

            return ShortestPath(start, goal);

        }

        public int Part2(string[] data)
        {
            var starts = new List<Point>();
            Point goal = new Point();
            Initialize(data);

            foreach (var point in _grid)
            {
                if (point.IsStart || point.Value == 'a') starts.Add(point);
                if (point.IsEnd) goal = point;
            }

            var shortest = int.MaxValue;
            foreach (var s in starts)
            {
                ResetShortest();
                s.Shortest = 0;
                var sp = ShortestPath(s, goal);
                if (sp < shortest) shortest = sp;

            }

            return shortest;
        }

        private void Initialize(string[] data)
        {
            var height = data.Length;
            var width = data[0].Length;

            (int x, int y) start = (0, 0);
            (int x, int y) goal = (0, 0);

            _graph = new List<Node>();

            _intgrid = new int[height, width];
            _grid = new Point[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var c = data[i][j];

                    _intgrid[i, j] = c switch
                    {
                        >= 'a' and <= 'z' => c - 'a',
                        'S' => 0,
                        'E' => 'z' - 'a',
                        _ => -1
                    };


                    var point = new Point() { Name = $"({i},{j})", Value = c };
                    if (c == 'S')
                    {
                        start = (i, j);
                        point.IsStart = true;
                        point.Shortest = 0;
                    }

                    if (c == 'E')
                    {
                        goal = (i, j);
                        point.IsEnd = true;
                    }

                    _grid[i, j] = point;

                }

            }

            // skapa graf
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var c = _intgrid[i, j];
                    var top = i - 1;
                    var bottom = i + 1;
                    var left = j - 1;
                    var right = j + 1;

                    if (top >= 0 && _intgrid[top, j] - _intgrid[i, j] < 2)
                    {
                        _graph.Add(new Node
                        {
                            Source = _grid[i, j],
                            Destination = _grid[top, j],
                        });

                    }
                    if (bottom < height && _intgrid[bottom, j] - _intgrid[i, j] < 2)
                    {
                        _graph.Add(new Node
                        {
                            Source = _grid[i, j],
                            Destination = _grid[bottom, j],
                        });
                    }
                    if (left >= 0 && _intgrid[i, left] - _intgrid[i, j] < 2)
                    {
                        _graph.Add(new Node
                        {
                            Source = _grid[i, j],
                            Destination = _grid[i, left],
                        });
                    }
                    if (right < width && _intgrid[i, right] - _intgrid[i, j] < 2)
                    {
                        _graph.Add(new Node
                        {
                            Source = _grid[i, j],
                            Destination = _grid[i, right],
                        });
                    }


                }
            }

        }




        private int ShortestPath(Point start, Point goal)
        {
            // find path
            var processing = new Queue<Point>();
            // var p = grid[start.x, start.y];
            processing.Enqueue(start);

            while (processing.Count > 0)
            {
                var current = processing.Dequeue();
                var paths = _graph.Where(x => x.Source == current).ToList();
                foreach (var path in paths)
                {
                    if (path.Destination.Shortest > current.Shortest + path.Weight)
                    {
                        path.Destination.Shortest = current.Shortest + path.Weight;
                        processing.Enqueue(path.Destination);
                    }
                }
            }


            var length = goal.Shortest;
            return length;
        }

        private void ResetShortest()
        {
            foreach (var point in _grid)
            {
                point.Shortest = int.MaxValue;
            }
        }


    }

    class Point
    {
        public int Shortest { get; set; } = int.MaxValue;
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public string Name { get; set; }
        public char Value { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    class Node
    {
        public Point Source { get; set; }
        public Point Destination { get; set; }
        public int Weight => 1; // always 1

        public override string ToString()
        {
            return $"{Source} => {Destination}";
        }
    }


}
