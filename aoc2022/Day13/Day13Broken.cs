namespace aoc2022.Day13Broken
{
    internal class Day13Broken
    {
        public void Run()
        {
            var data = "[1,1,3,1,1]\r\n[1,1,5,1,1]\r\n\r\n[[1],[2,3,4]]\r\n[[1],4]\r\n\r\n[9]\r\n[[8,7,6]]\r\n\r\n[[4,4],4,4]\r\n[[4,4],4,4,4]\r\n\r\n[7,7,7,7]\r\n[7,7,7]\r\n\r\n[]\r\n[3]\r\n\r\n[[[]]]\r\n[[]]\r\n\r\n[1,[2,[3,[4,[5,6,7]]]],8,9]\r\n[1,[2,[3,[4,[5,6,0]]]],8,9]".Split("\r\n");

         data = File.ReadAllLines(@"Day13\input.txt");

          Validate(data);
          var ans1 = Part1(data);
        }

        public int Part1(string[] data)
        {
            var j = 0;
            var index = 1;
            var ans = 0;
            while (j < data.Length)
            {
                var position = 0;
                var blaj = data[j];
                var first = Parse(blaj, ref position);

                blaj = data[j + 1];
                position = 0;
                var second = Parse(blaj, ref position);


                var a = Compare(first, second);
                if (a<=0)
                {
                    Console.WriteLine(index);
                    ans += index;
                }


                j += 3;
                index++;
                // find indeces of pairs in right order
            }

            return ans;
        }


        int Compare(List<object> first, List<object> second)
        {

            var fA = first.ToArray();
            var sA = second.ToArray();
            var fatemp = Expand(first);
            var satemp = Expand(second);

            var comparing = false;
            var isEqual = false;

            for (var i = 0; i < fA.Length; i++)
            {
                if (i >= sA.Length) {

                   // DebugPrint(first,second,false);
                    return 1; // second array ran out of objects, wrong order;
                }

                else if (fA[i] is int && sA[i] is int)
                {
                    comparing = false;
                    if ((int)fA[i] > (int)sA[i]) return 1;
                    if ((int)fA[i] < (int)sA[i]) return -1;
                    comparing = true; isEqual = true;
                }
                else if (fA[i] is int && sA[i] is List<object>)
                {
                    var objList = new List<object>();
                    objList.Add(fA[i]);
                    return Compare(objList, sA[i] as List<object>);
                }
                else if (fA[i] is List<object> && sA[i] is int)
                {
                    var objList = new List<object>();
                    objList.Add(sA[i]);
                    return Compare(fA[i] as List<object>, objList);
                }

                else
                {
                    return Compare(fA[i] as List<object>, sA[i] as List<object>);
                }
                   
            }
            //
            //   DebugPrint(first,second,true);

            if (comparing && isEqual)
            {
                return 0;
            }
            return -1;

        }

        //void DebugPrint(List<object> first, List<object> second, bool res)
        //{
        //    Console.WriteLine(Expand(first));
        //    Console.WriteLine(Expand(second));
        //    Console.WriteLine(res.ToString());
        //    Console.WriteLine("");
        //}

        

        List<object> Parse(string input, ref int position)
        {
            var objList = new List<object>();

            var temp = string.Empty;
            while (position < input.Length)
            {
                var part = input[position];
                
                position++;
                if (int.TryParse(part.ToString(), out var result))
                {
                    temp += part;
                }
                else if (part == '[')
                {
                    objList.Add(Parse(input, ref position));
                }
                else if (part == ',')
                {
                    if (!string.IsNullOrEmpty(temp))
                    {
                        objList.Add(int.Parse(temp));
                        temp = string.Empty;
                    }
                }

                else  // ]
                {
                    if (!string.IsNullOrEmpty(temp))
                    {
                        objList.Add(int.Parse(temp));
                        temp = string.Empty;
                    }
                    return objList;
                }
            }

            if (!string.IsNullOrEmpty(temp))
            {
                objList.Add(int.Parse(temp));
                temp = string.Empty;
            }
            return objList;
        }
        
        #region Debug

        private void Validate(string[] data)
        {
            foreach (var s in data)
            {
                if (string.IsNullOrEmpty(s)) continue;

                var position = 0;
                var parsed = Parse(s, ref position);
                var mangled = Expand(parsed);

                if (mangled != s)
                {
                    Console.WriteLine(s);
                    Console.WriteLine(mangled);
                    throw new Exception("Broken");
                }
            }
        }

        private string Expand(List<object> objList)
        {
            var str = string.Empty;
            var strs = new List<string>();

            foreach (object obj in objList)
            {
                if (obj is List<object>)
                {
                    strs.Add($"[{Expand((List<object>)obj)}]");
                }

                if (obj is int)
                {
                    strs.Add($"{obj}");
                }

            }

            str = string.Join(",", strs.ToArray());
            return str;
        }


        #endregion

    }
}


