using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day07
{
    internal class Day07
    {
        private Node root;
        private Node currentNode;

        public void Run()
        {

            var data = File.ReadAllLines(@"Day07\input.txt");
            //data = "$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k".Split("\r\n");

            root = new Node("/", null, NodeType.Directory);
            currentNode = root;

            foreach (var line in data)
            {
                if (line == "$ cd /") currentNode = root;
                else if (line == "$ cd ..") currentNode = currentNode.Parent;
                else if (line.StartsWith("$ cd"))
                {
                    currentNode = currentNode.Children.First(x => x.Name == line.Substring(5));
                }
                else if (line == "$ ls") {/* skip ls */}
                else { currentNode.AddChild(line); }

            }

            var flatDirNodes = root.FlatList.Where(x => x.NodeType == NodeType.Directory);

            
            
            // --- Part 1 - sum of total sizes
           
            var max_size = 100_000;
            var ans1 = flatDirNodes.Where(x => x.Size < max_size).Sum(x => x.Size);


            // -- Part 2 - size of dir to delete

            var total_disk_space = 70_000_000;
            var upgrade_needs = 30_000_000;

            var size_to_delete = upgrade_needs - (total_disk_space - root.Size);

            var candidates = flatDirNodes.Where(x => x.Size >= size_to_delete).ToList();

            var ans2 = candidates.Min(x => x.Size);

        }

        internal class Node
        {
            private int size = 0;
            public string Name { get; set; }

            public int Size
            {
                get
                {
                    if (NodeType == NodeType.File) return size;
                    return Children.Sum(x => x.Size);
                }
                set => size = value;
            }

            public List<Node> Children { get; set; }
            public Node? Parent { get; set; }
            public NodeType NodeType { get; set; }

            public List<Node> FlatList
            {
                get
                {
                    if (NodeType == NodeType.File) return new List<Node>() { this };
                    var lists = Children.Select(x => x.FlatList);

                    var list = new List<Node>() { this };
                    foreach (var l in lists)
                    {
                        list.AddRange(l);
                    }
                    return list;

                }
            }

            public Node(string name, Node? parent, NodeType nodeType, int size = 0)
            {
                Name = name;
                Parent = parent;
                NodeType = nodeType;
                Size = size;
                if (nodeType == NodeType.Directory) Children = new List<Node>();
            }

            public void AddChild(string row)
            {
                var fileInfo = row.Split(' ');
                if (fileInfo[0] == "dir")
                {
                    Children.Add(new Node(fileInfo[1], this, NodeType.Directory));
                }
                else
                {
                    Children.Add(new Node(fileInfo[1], this, NodeType.File, int.Parse(fileInfo[0])));
                }
            }
        }

        internal enum NodeType
        {
            File, Directory
        }

    }
}
