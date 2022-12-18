

namespace aoc2022.Day18
{
    internal class Day18
    {
        public void Run()
        {
            var data = "2,2,2\r\n1,2,2\r\n3,2,2\r\n2,1,2\r\n2,3,2\r\n2,2,1\r\n2,2,3\r\n2,2,4\r\n2,2,6\r\n1,2,5\r\n3,2,5\r\n2,1,5\r\n2,3,5".Split("\r\n");
            data = File.ReadAllLines(@"Day18\input.txt");

            var cubes = SetupCubes(data);
            var ans1 = Part1(cubes);
            var ans2 = Part2(cubes);
        }

        private int Part1(List<Cube> cubes)
        {
            var totalSurface = 0;
            var processed = new List<Cube>();

            foreach (var cube in cubes)
            {
                totalSurface += 6;
                totalSurface -= processed.Any(x => x.x == cube.x && x.y == cube.y && x.z == cube.z - 1) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x && x.y == cube.y && x.z == cube.z + 1) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x && x.y == cube.y - 1 && x.z == cube.z) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x && x.y == cube.y + 1 && x.z == cube.z) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x - 1 && x.y == cube.y && x.z == cube.z) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x + 1 && x.y == cube.y && x.z == cube.z) ? 2 : 0;
                processed.Add(cube);
            }

            return totalSurface;
        }

        private int Part2(List<Cube> cubes)
        {
            var totalSurface = 0;
            var processed = new List<Cube>();
            var emptySpace = new List<Cube>();
            

            foreach (var cube in cubes)
            {
                totalSurface += 6;
                totalSurface -= processed.Any(x => x.x == cube.x && x.y == cube.y && x.z == cube.z - 1) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x && x.y == cube.y && x.z == cube.z + 1) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x && x.y == cube.y - 1 && x.z == cube.z) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x && x.y == cube.y + 1 && x.z == cube.z) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x - 1 && x.y == cube.y && x.z == cube.z) ? 2 : 0;
                totalSurface -= processed.Any(x => x.x == cube.x + 1 && x.y == cube.y && x.z == cube.z) ? 2 : 0;

                if (!cubes.Any(x => x.x == cube.x && x.y == cube.y && x.z == cube.z - 1)) emptySpace.Add(new Cube(cube.x, cube.y, cube.z - 1));
                if (!cubes.Any(x => x.x == cube.x && x.y == cube.y && x.z == cube.z + 1)) emptySpace.Add(new Cube(cube.x, cube.y, cube.z + 1));
                if (!cubes.Any(x => x.x == cube.x && x.y == cube.y - 1 && x.z == cube.z)) emptySpace.Add(new Cube(cube.x, cube.y - 1, cube.z));
                if (!cubes.Any(x => x.x == cube.x && x.y == cube.y + 1 && x.z == cube.z)) emptySpace.Add(new Cube(cube.x, cube.y + 1, cube.z));
                if (!cubes.Any(x => x.x == cube.x - 1 && x.y == cube.y && x.z == cube.z)) emptySpace.Add(new Cube(cube.x - 1, cube.y, cube.z));
                if (!cubes.Any(x => x.x == cube.x + 1 && x.y == cube.y && x.z == cube.z)) emptySpace.Add(new Cube(cube.x + 1, cube.y, cube.z));

                processed.Add(cube);
                
            }
            var innerSpace = new List<Cube>();
            foreach (var emptyCube in emptySpace.Distinct())
            {
                if (cubes.Any(x => x.x == emptyCube.x && x.y == emptyCube.y && x.z > emptyCube.z) &&
                    cubes.Any(x => x.x == emptyCube.x && x.y == emptyCube.y && x.z < emptyCube.z) &&
                    cubes.Any(x => x.x == emptyCube.x && x.y > emptyCube.y && x.z == emptyCube.z) &&
                    cubes.Any(x => x.x == emptyCube.x && x.y < emptyCube.y && x.z == emptyCube.z) &&
                    cubes.Any(x => x.x > emptyCube.x && x.y == emptyCube.y && x.z == emptyCube.z) &&
                    cubes.Any(x => x.x < emptyCube.x && x.y == emptyCube.y && x.z == emptyCube.z))
                    innerSpace.Add(emptyCube);

                // this doesnt take into account tentacle-like structures, where there are cubes in all directions,
                // but the empty cube is on the surface anyway. Needs more work
                // Does work for the test input but not for my input :(
            }

            return totalSurface - Part1(innerSpace);
        }

        private static List<Cube> SetupCubes(string[] data)
        {
            var cubes = new List<Cube>();
            foreach (var line in data)
            {
                var points = line.Split(",").Select(int.Parse).ToArray();
                cubes.Add(new Cube(points[0], points[1], points[2]));
            }

            return cubes;
        }
    }

    record Cube(int x, int y, int z);

}
