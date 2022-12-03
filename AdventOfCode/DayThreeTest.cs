using System.Reflection;

namespace AdventOfCode;

public class DayThree
{
    public Dictionary<string, int> priorities;

    public DayThree()
    {
        var items = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
            .ToCharArray()
            .Select((value, i) => (value, i));

        priorities = new Dictionary<string, int>();
        
        foreach (var (value, i) in items)
        {
            priorities.Add(value.ToString(), i + 1);
        }
    }

    public (List<string>, List<string>) splitBackpack(string backpack)
    {
        var midpoint = (int) Math.Round((double) backpack.Length / 2);
        var firstCompartment = backpack
            .Substring(0, midpoint)
            .ToCharArray()
            .ToList()
            .ConvertAll(value => value.ToString());
        var secondCompartment = backpack
            .Substring(midpoint)
            .ToCharArray()
            .ToList()
            .ConvertAll(value => value.ToString());

        return (firstCompartment, secondCompartment);
    }

    public int calculateScore(string backpack)
    {
        (var firstCompartment, var secondCompartment)= splitBackpack(backpack);
        var doublePacked = firstCompartment
            .Intersect(secondCompartment)
            .Select(item => priorities[item])
            .Sum();

        return doublePacked;
    }

    public List<string> explodeBackpack(string backpack)
    {
        return backpack
            .ToCharArray()
            .ToList()
            .ConvertAll(value => value.ToString());
    }

    public string calculateBags(string[] backpack)
    {
        var backpackOne = explodeBackpack(backpack[0]);
        var backpackTwo = explodeBackpack(backpack[1]);
        var backpackThree = explodeBackpack(backpack[2]);

        var remainder = backpackOne
            .Intersect(backpackTwo)
            .Intersect(backpackThree)
            .ToList();

        return remainder[0];
    }
}

public class DayThreeTests
{
    [Test]
    public void FirstProblemExample()
    {
        var dayThree = new DayThree();

        var score = LoadFile
            .fromPathAsList(@"samples/daythree/problem3-example.txt")
            .Select(dayThree.calculateScore)
            .Sum();

        Assert.AreEqual(score, 157);
    }

    [Test]
    public void FirstProblemSolution()
    {
        var dayThree = new DayThree();

        var score = LoadFile
            .fromPathAsList(@"samples/daythree/problem3-example.txt")
            .Select(dayThree.calculateScore)
            .Sum();

        Console.WriteLine("The total score is: {0}", score);
        
        Assert.Pass();
    }

    [Test]
    public void SecondProblemExample()
    {
        var dayThree = new DayThree();

        var score = LoadFile
            .fromPathAsList(@"samples/daythree/problem3-example.txt")
            .Select((x, i) => (x, i))
            .GroupBy(x => x.i / 3)
            .Select(tuples => tuples.Select(tuple => tuple.x).ToArray())
            .Select(dayThree.calculateBags)
            .ToList()
            .Select(item => dayThree.priorities[item])
            .Sum();

        Assert.AreEqual(score, 70);

        Assert.Pass();
    }

    [Test]
    public void SecondProblemSolution()
    {
        var dayThree = new DayThree();

        var score = LoadFile
            .fromPathAsList(@"samples/daythree/problem3-problem.txt")
            .Select((x, i) => (x, i))
            .GroupBy(x => x.i / 3)
            .Select(tuples => tuples.Select(tuple => tuple.x).ToArray())
            .Select(dayThree.calculateBags)
            .ToList()
            .Select(item => dayThree.priorities[item])
            .Sum();

        Console.WriteLine("The total score is: {0}", score);

        Assert.Pass();
    }
}