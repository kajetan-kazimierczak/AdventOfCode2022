namespace aoc2022.Day11;

internal class Day11
{
    

    public void Run()
    {
        var monkeys = SetupMonkeys();
        
        //monkeys = SetupExampleMonkeys();

        var ans1 = Part1(monkeys);

         monkeys = SetupMonkeys();
        var ans2 = Part2(monkeys);

    }

    public long Part1(Monkey[] monkeys)
    {

        for (var round = 1; round <= 20; round++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.HasItems())
                {
                    var item = monkey.GetItem();
                    item = monkey.Operation(item);
                    monkey.Inspections++;
                    item = item / 3;
                    var recipient = monkey.Test(item);
                    monkeys[(int)recipient].AddItem(item);
                }
               
            }
        }

        var topMonkeys = monkeys.OrderByDescending(x => x.Inspections).Take(2).ToArray();
        var monkeyBusiness = topMonkeys[0].Inspections * topMonkeys[1].Inspections;
        return monkeyBusiness;
    }

    public long Part2(Monkey[] monkeys)
    {
        for (var round = 1; round <= 10000; round++)
        {
            if(round % 100 == 0) Console.WriteLine(round);
            foreach (var monkey in monkeys)
            {
                while (monkey.HasItems())
                {
                    var item = monkey.GetItem();
                    item = monkey.Operation(item);
                    monkey.Inspections++;

                    item = ReduceItem(item, monkeys);

                    var recipient = monkey.Test(item);
                    monkeys[(int)recipient].AddItem( item);
                }

            }
        }

        var topMonkeys = monkeys.OrderByDescending(x => x.Inspections).Take(2).ToArray();
        var monkeyBusiness = topMonkeys[0].Inspections * topMonkeys[1].Inspections;
        return monkeyBusiness;
    }

    private long ReduceItem(long item, Monkey[] monkeys)
    {
        long divider = 1;
        foreach (var monkey in monkeys)
        {
            divider *= monkey.Divider;
        }

        return item % divider ;
    }

    public Monkey[] SetupExampleMonkeys()
    {
        /*
        Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1
        */

        var monkeys = new Monkey[4];
        monkeys[0] = new Monkey((x) => x * 19, (x) => x % 23 == 0 ? 2 : 3, 23, new long[] { 79, 98 });
        monkeys[1] = new Monkey((x) => x + 6, (x) => x % 19 == 0 ? 2 : 0, 19, new long[] { 54, 65, 75, 74 });
        monkeys[2] = new Monkey((x) => x * x, (x) => x % 13 == 0 ? 1 : 3, 13, new long[] { 79, 60, 97 });
        monkeys[3] = new Monkey((x) => x + 3, (x) => x % 17 == 0 ? 0 : 1, 17,new long[] { 74 });
        return monkeys;
    }

    public Monkey[] SetupMonkeys()
    {
        var monkeys = new Monkey[8];
        monkeys[0] = new Monkey((x) => x * 11, (x) => x % 13 == 0 ? 1 : 7,13, new long[] { 71, 56, 50, 73 });
        monkeys[1] = new Monkey((x) => x + 1, (x) => x % 7 == 0 ? 3 : 6, 7,new long[] { 70, 89, 82 });
        monkeys[2] = new Monkey((x) => x * x, (x) => x % 3 == 0 ? 5 : 4, 3,new long[] { 52, 95 });
        monkeys[3] = new Monkey((x) => x + 2, (x) => x % 19 == 0 ? 2 : 6, 19,new long[] { 94, 64, 69, 87, 70 });
        monkeys[4] = new Monkey((x) => x + 6, (x) => x % 5 == 0 ? 0 : 5, 5,new long[] { 98, 72, 98, 53, 97, 51 });
        monkeys[5] = new Monkey((x) => x + 7, (x) => x % 2 == 0 ? 7 : 0, 2,new long[] { 79 });
        monkeys[6] = new Monkey((x) => x * 7, (x) => x % 11 == 0 ? 2 : 4, 11,new long[] { 77, 55, 63, 93, 66, 90, 88, 71 });
        monkeys[7] = new Monkey((x) => x + 8, (x) => x % 17 == 0 ? 1 : 3, 17,new long[] { 54, 97, 87, 70, 59, 82, 59 });

        return monkeys;
    }
   
}

internal class Monkey
{
    private Queue<long> _items = new();
    private long _divider = 1;

    public Monkey(Func<long, long> operation, Func<long, long> test, long divider, long[] items)
    {
        Operation = operation;
        Test = test;
        _divider = divider;
        foreach (var item in items)
        {
            _items.Enqueue(item);
        }
    }

    public long Inspections { get; set; }

    public void AddItem(long item)
    {
        _items.Enqueue(item);
    }

    public long GetItem()
    {
        return _items.Dequeue();
    }


    public long Divider => _divider;

    public bool HasItems()
    {
        return _items.Count > 0;
    }

    public Func<long, long> Operation { get; set; }

    public Func<long, long> Test { get; set; }

}

