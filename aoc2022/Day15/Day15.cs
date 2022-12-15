using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace aoc2022.Day15
{
    internal class Day15
    {

        public void Run()
        {
            var data = "Sensor at x=2, y=18: closest beacon is at x=-2, y=15\r\nSensor at x=9, y=16: closest beacon is at x=10, y=16\r\nSensor at x=13, y=2: closest beacon is at x=15, y=3\r\nSensor at x=12, y=14: closest beacon is at x=10, y=16\r\nSensor at x=10, y=20: closest beacon is at x=10, y=16\r\nSensor at x=14, y=17: closest beacon is at x=10, y=16\r\nSensor at x=8, y=7: closest beacon is at x=2, y=10\r\nSensor at x=2, y=0: closest beacon is at x=2, y=10\r\nSensor at x=0, y=11: closest beacon is at x=2, y=10\r\nSensor at x=20, y=14: closest beacon is at x=25, y=17\r\nSensor at x=17, y=20: closest beacon is at x=21, y=22\r\nSensor at x=16, y=7: closest beacon is at x=15, y=3\r\nSensor at x=14, y=3: closest beacon is at x=15, y=3\r\nSensor at x=20, y=1: closest beacon is at x=15, y=3".Split("\r\n");
            var row = 10;
            var boundary = 20;
            
            data = File.ReadAllLines(@"Day15\input.txt"); row = 2000000; boundary = 4000000;
            

            var readings = Parse(data);

            var ans1 = Part1(readings, row);
            var ans2 = Part2(readings, boundary);
        }

        private int ManhattanDistance((int sx, int sy, int bx, int by) reading)
        {
            return Math.Abs(reading.sx - reading.bx) + Math.Abs(reading.sy - reading.by);
        }

        private bool IsWithinManhattanDistance((int sx, int sy, int bx, int by) reading, int x, int y)
        {
            var distance = ManhattanDistance(reading);
            return Math.Abs(reading.sx - x) + Math.Abs(reading.sy - y) <= distance;
        }

        private (int xMin, int xMax, int yMin, int yMax) GetBoudary(List<(int sx, int sy, int bx, int by)> readings)
        {
            var xMin = int.MaxValue;
            var xMax = int.MinValue;
            var yMin = int.MaxValue;
            var yMax = int.MinValue;

            foreach (var reading in readings)
            {
                var distance = ManhattanDistance(reading);
                var rxmin = reading.sx - distance;
                var rxmax = reading.sx + distance;
                var rymin = reading.sy - distance;
                var rymax = reading.sy + distance;
                if (rxmin < xMin ) xMin = rxmin;
                if(rxmax > xMax ) xMax = rxmax;
                if (rymin < yMin) yMin = rymin;
                if (rymax > yMax) yMax = rymax;
            }
            return (xMin, xMax, yMin, yMax);
        } 

        private List<(int sx, int sy, int bx, int by)> Parse(string[] data)
        {
            var readings = new List<(int sx,int sy, int bx, int by)>();

            foreach (var s in data)
            {
                var parts = s.Replace(",","").Replace(":","").Split(" ");
                var reading = (sx: 0, sy: 0, bx: 0, by: 0);
                reading.sx = int.Parse(parts[2].Split("=")[1]);
                reading.sy = int.Parse(parts[3].Split("=")[1]);
                reading.bx = int.Parse(parts[8].Split("=")[1]);
                reading.by = int.Parse(parts[9].Split("=")[1]);

                readings.Add(reading);
            }

            return readings;
        }

        private int NumberOfBeaconsInRow(List<(int sx, int sy, int bx, int by)> readings, int row)
        {

            var distinct = readings.Select(r =>  (r.bx, r.by)).Distinct();

            var sum = distinct.Count(x => x.by == row);

            return sum;
        }

        public int Part1(List<(int sx, int sy, int bx, int by)> readings, int rowToExamine)
        {
            var count = 0;
        
            var boundary = GetBoudary(readings);

            for (var x = boundary.xMin; x <= boundary.xMax; x++)
            {
                foreach (var reading in readings)
                {
                    if (IsWithinManhattanDistance(reading, x, rowToExamine))
                    {
                        count++;
                        break;
                    }
                }
            }

            return count - NumberOfBeaconsInRow(readings, rowToExamine);
        }


        private BigInteger TuningFrequency(int x, int y) => 4_000_000 * (BigInteger)x + (BigInteger)y;

        public (int x, int y) FindBeacon(List<(int sx, int sy, int bx, int by)> readings, int boundary)
        {
            
            for (var x = 0; x <= boundary; x++)
            {
               for (var y = 0; y <= boundary; y++)
               {
                    var isWithinDistance = false;
                    foreach (var reading in readings)
                    {
                        isWithinDistance = isWithinDistance || IsWithinManhattanDistance(reading, x, y);
                    }
                    if (!isWithinDistance) return (x, y);
               }
            }

            return (0, 0);
        }


        public (int x, int y) FindBeaconFast(List<(int sx, int sy, int bx, int by)> readings, int boundary)
        {

            for (var y = 0; y <= boundary; y++)
            {
                for (var x = 0; x <= boundary; x++)
                {
                    var rs = readings.Where(r => IsWithinManhattanDistance(r, x, y));
                    if (!rs.Any()) return(x,y);

                    var max = rs.Select(r => FindBiggestXForRow(r, y)).Max();
                    x = max;
                }
            }

            return (0, 0);
        }

        private int FindBiggestXForRow((int sx, int sy, int bx, int by) reading, int row)
        {
            var distance = ManhattanDistance(reading);
            if (reading.sy + distance < row || reading.sy - distance > row) return 0;
            
            var x = reading.sx + distance - Math.Abs(reading.sy - Math.Abs(row)); 

            return x;
        }

        public BigInteger Part2(List<(int sx, int sy, int bx, int by)> readings, int boundary)
        {
            var beacon = FindBeaconFast(readings, boundary);
            var frequency = TuningFrequency(beacon.x, beacon.y);

            return frequency;
        }
    }
}
