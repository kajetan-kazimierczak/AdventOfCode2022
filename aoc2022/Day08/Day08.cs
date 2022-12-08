using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day08
{
    internal class Day08
    {

        int[,] _grid = new int[0,0];

        public void Run()
        {
            LoadGrid();
            var ans1 = Part1();
            var ans2 = Part2();
        }

        public int Part1()
        {
            var visible_trees = 4 * _grid.GetLength(0) - 4;

            for (int i = 1; i < _grid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < _grid.GetLength(1) - 1; j++)
                {
                    var highest = 0;
                    for (int k = 0; k < j; k++)
                    {
                        if (_grid[i, k] > highest) highest = _grid[i, k];
                    }
                    if (highest < _grid[i, j])
                    {
                        visible_trees++;
                        continue;
                    }

                    highest = 0;
                    for (int k = j + 1; k < _grid.GetLength(1); k++)
                    {
                        if (_grid[i, k] > highest) highest = _grid[i, k];

                    }
                    if (highest < _grid[i, j])
                    {
                        visible_trees++;
                        continue;
                    }

                    highest = 0;
                    for (int k = 0; k < i; k++)
                    {
                        if (_grid[k, j] > highest) highest = _grid[k, j];
                    }
                    if (highest < _grid[i, j])
                    {
                        visible_trees++;
                        continue;
                    }

                    highest = 0;
                    for (int k = i + 1; k < _grid.GetLength(0); k++)
                    {
                        if (_grid[k, j] > highest) highest = _grid[k, j];
                    }
                    if (highest < _grid[i, j])
                    {
                        visible_trees++;
                        continue;
                    }

                }
            }

            return visible_trees;
        }

        public int Part2()
        {
            var max_scenic = 0;

            for (int i = 1; i < _grid.GetLength(0) - 1; i++)
            {

                for (int j = 1; j < _grid.GetLength(1) - 1; j++)
                {
                    var me = _grid[i, j];

                    var scenic_left = 0;
                    var k = j - 1;
                    do
                    {
                        scenic_left++;
                        if (_grid[i, k] >= me)
                        {
                            k = -1;
                        }
                        k--;
                    } while (k > -1);


                    var scenic_right = 0;
                    k = j + 1;
                    do
                    {
                        scenic_right++;
                        if (_grid[i, k] >= me)
                        {
                            k = _grid.GetLength(1);
                        }
                        k++;
                    } while (k < _grid.GetLength(1));



                    var scenic_top = 0;
                    k = i - 1;
                    do
                    {
                        scenic_top++;
                        if (_grid[k, j] >= me)
                        {
                            k = -1;
                        }
                        k--;
                    } while (k > -1);


                    var scenic_bottom = 0;
                    k = i + 1;
                    do
                    {
                        scenic_bottom++;
                        if (_grid[k, j] >= me)
                        {
                            k = _grid.GetLength(1);
                        }
                        k++;
                    } while (k < _grid.GetLength(1));


                    var current_scenic = scenic_left * scenic_right * scenic_top * scenic_bottom;
                    if (max_scenic < current_scenic)
                    {
                        max_scenic = current_scenic;
                    }
                }
            }
            return max_scenic;
        }

        private void LoadGrid()
        {
            var data = File.ReadAllLines(@"Day08\input.txt");
            //data = "30373\r\n25512\r\n65332\r\n33549\r\n35390".Split("\r\n");

             _grid = new int[data[0].Length, data.Length];

            // load grid
            for (var i = 0; i < data.Length; i++)
            {
                var row = data[i];
                for (var j = 0; j < row.Length; j++)
                {
                    _grid[i, j] = int.Parse(row[j].ToString());
                }
            }
        }
    }
}
