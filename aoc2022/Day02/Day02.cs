using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day02
{
    internal class Day02
    {
        
        public void Run()
        {
            var data = File.ReadAllLines(@"Day02\input.txt");
            //var data = "A Y\r\nB X\r\nC Z".Split("\r\n");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        public int Part1(string[] data)
        {
            var score = 0;
            foreach (var line in data)
            {
                var shapes = line.Split(" ");
                score += winscore(shapes[0], shapes[1]) + shapescore(shapes[1]);
            }
            return score;
        }

        public int Part2(string[] data)
        {
            var score = 0;
            foreach (var line in data)
            {
                var shapes = line.Split(" ");
                shapes[1] = shapeToUse(shapes[0], shapes[1]);
                score += winscore(shapes[0], shapes[1]) + shapescore(shapes[1]);
            };
            return score;
        }



        private int shapescore(string shape) => shape switch
        {
            "X" => 1,
            "Y" => 2,
            "Z" => 3,
            _ => 0
        };
        
        private int winscore(string shape1, string shape2) => shape1 switch
        {
            "A" => shape2 switch // rock
            {
                "Y" => 6, // paper
                "Z" => 0, // scisors
                _ => 3
            },
            "B" => shape2 switch // paper
            {
                "X" => 0, // rock
                "Z" => 6, // scissors
                _ => 3
            },
            "C" => shape2 switch // scisors
            {
                "X" => 6, // rock
                "Y" => 0, // paper
                _ => 3
            },
            _ => 0
        };

        private string shapeToUse(string shape1, string result) => shape1 switch
        {
            "A" => result switch // rock
            {
                "X" => "Z", // lose
                "Y" => "X", // draw
                "Z" => "Y", // win
                _ => ""
            },
            "B" => result switch // paper
            {
                "X" => "X", // lose
                "Y" => "Y",
                "Z" => "Z",
                _ => ""
            },
            "C" => result switch // scisors
            {
                "X" => "Y", // lose
                "Y" => "Z",
                "Z" => "X",
                _ => ""
            },
            _ => ""
        };

    }
}
