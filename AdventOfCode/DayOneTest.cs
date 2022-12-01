using System.Reflection;

namespace AdventOfCode;

public class LoadFile
{
    public static string fromPath(string filepath)
    {
        var finalPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "../../../" + filepath
        );

        return File.ReadAllText(finalPath);
    }
}

public static class DayOne
{
    static IDictionary<int, int> calculateCalories(string[] caloriesList)
    {
        IDictionary<int, int> elfList = new Dictionary<int, int>();
        elfList[0] = 0;

        foreach (var amount in caloriesList)
        {
            if (amount.Equals(""))
            {
                elfList[elfList.Count()] = 0;
            }
            else
            {
                elfList[elfList.Count() - 1] += int.Parse(amount);
            }
        }

        return elfList;
    }

    private static string[] splitText(string text)
    {
        return text.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );
    }

    public static (int, int) getElfWithMostCalories(string caloriesString)
    {
        var caloriesList = splitText(caloriesString);
        var elvesWithCalories = calculateCalories(caloriesList);
        var highestElfIndex = 0;

        foreach (var (elfId, calories) in elvesWithCalories)
        {
            highestElfIndex = calories > elvesWithCalories[highestElfIndex]
                ? elfId
                : highestElfIndex;
        }

        return (highestElfIndex, elvesWithCalories[highestElfIndex]);
    }


    public static List<(int, int)> getElvesWithMostCalories(string caloriesString, int takeCount)
    {
        var caloriesList = splitText(caloriesString);
        var elvesWithCalories = calculateCalories(caloriesList);
        var elvesWithCaloriesList = new List<(int, int)>();

        foreach (var (elfId, calories) in elvesWithCalories)
        {
            elvesWithCaloriesList.Add((elfId, calories));
        }

        return elvesWithCaloriesList
            .OrderBy(i => i.Item2)
            .Reverse()
            .Take(takeCount)
            .ToList();
    }
}

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FirstProblemExample()
    {
        var input = LoadFile.fromPath(@"samples/dayone/problem1-example.txt");
        var (elfIndex, totalCalories) = DayOne.getElfWithMostCalories(input);

        Assert.AreEqual(elfIndex, 3);
        Assert.AreEqual(totalCalories, 24000);
    }

    [Test]
    public void FirstProblemSolution()
    {
        var input = LoadFile.fromPath(@"samples/dayone/problem1-problem.txt");
        var (elfIndex, totalCalories) = DayOne.getElfWithMostCalories(input);

        Console.WriteLine("Elf Index {0} with calories {1}", elfIndex, totalCalories);

        Assert.Pass();
    }

    [Test]
    public void SecondProblemExample()
    {
        var input = LoadFile.fromPath(@"samples/dayone/problem1-example.txt");
        var result = DayOne.getElvesWithMostCalories(input, 3);

        Assert.AreEqual(result[0], (3, 24000));
        Assert.AreEqual(result[1], (2, 11000));
        Assert.AreEqual(result[2], (4, 10000));
        
        var totalCalories = result.Sum(i => i.Item2);
        Assert.AreEqual(totalCalories, 45000);
    }

    [Test]
    public void SecondProblemSolution()
    {
        var input = LoadFile.fromPath(@"samples/dayone/problem1-problem.txt");
        var result = DayOne.getElvesWithMostCalories(input, 3);
        
        foreach (var (elfId, calories) in result)
        {
            Console.WriteLine("1. [{0}]: [{1}]", elfId, calories);
        }

        var totalCalories = result.Sum(i => i.Item2);
        Console.WriteLine("Total number of calories: {0}", totalCalories);
        Assert.Pass();
    }
}