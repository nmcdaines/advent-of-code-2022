using System.Reflection;

namespace AdventOfCode;

/*
 * Decoded:        X Rock, Y Paper, Z Scissors
 * Score:          1 Rock, 2 Paper, 3 Scissors
 * --------------------------------------------
 * Outcome Score:  0 lost, 3 draw,  6 won
 */

/*
 * Ideal way of tracking who wins, would be to have an array that loops
 * that way you can use item + 1... maybe a mod would work?
 */

public enum ResponseType
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public enum OutcomeType
{
    Win = 6,
    Draw = 3,
    Loose = 0
}

public class DayTwo
{
    private Dictionary<string, ResponseType> myCipher;
    private Dictionary<string, ResponseType> theirCipher;
    private Dictionary<string, OutcomeType> myOutcomes;
    
    public DayTwo()
    {
        myCipher = new Dictionary<string, ResponseType>
        {
            { "X", ResponseType.Rock },
            { "Y", ResponseType.Paper },
            { "Z", ResponseType.Scissors }
        };
        
        theirCipher = new Dictionary<string, ResponseType>
        {
            { "A", ResponseType.Rock },
            { "B", ResponseType.Paper },
            { "C", ResponseType.Scissors },
        };
        
        myOutcomes = new Dictionary<string, OutcomeType>
        {
            { "X", OutcomeType.Loose },
            { "Y", OutcomeType.Draw },
            { "Z", OutcomeType.Win }
        };
    }

    public static int play(ResponseType them, ResponseType me)
    {
        // Wins
        if (lhsWins(me, them))
        {
            return 6;
        }

        // Loss
        if (lhsWins(them, me))
        {
            return 0;
        }

        // Draw
        return 3;
    }

    public static ResponseType generateMyResponse(ResponseType them, OutcomeType me)
    {
        if (me == OutcomeType.Draw)
        {
            return them;
        }

        return them switch
        {
            ResponseType.Rock => me == OutcomeType.Win ? ResponseType.Paper : ResponseType.Scissors,
            ResponseType.Paper => me == OutcomeType.Win ? ResponseType.Scissors : ResponseType.Rock,
            ResponseType.Scissors => me == OutcomeType.Win ? ResponseType.Rock : ResponseType.Paper,
            _ => them
        };
    }

    private static bool lhsWins(ResponseType a, ResponseType b)
    {
        return a == ResponseType.Rock && b == ResponseType.Scissors ||
               a == ResponseType.Scissors && b == ResponseType.Paper ||
               a == ResponseType.Paper && b == ResponseType.Rock;
    }

    public (ResponseType, int, int, int) entryToResult(string entry)
    {
        var elements = entry.Split(" ");
        
        var theirAnswer = theirCipher[elements[0]];
        var myAnswer = myCipher[elements[1]];

        var roundPoints = play(theirAnswer, myAnswer);
        var actionPoints = (int)myAnswer;
        var roundTotal = roundPoints + actionPoints;

        return (myAnswer, roundPoints, actionPoints, roundTotal);
    }

    public (ResponseType, int, int, int) entryToResultByOutcome(string entry)
    {
        var elements = entry.Split(" ");
        
        var theirAnswer = theirCipher[elements[0]];
        var myOutcome = myOutcomes[elements[1]];

        var myAnswer = generateMyResponse(theirAnswer, myOutcome);
        var roundPoints = play(theirAnswer, myAnswer);
        var actionPoints = (int)myAnswer;
        var roundTotal = roundPoints + actionPoints;

        return (myAnswer, roundPoints, actionPoints, roundTotal);
    }
}

public class DayTwoTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FirstProblemExample()
    {
        var dayTwo = new DayTwo();

        var gameResults = LoadFile
            .fromPathAsList(@"samples/daytwo/problem2-example.txt")
            .ToList()
            .ConvertAll<(ResponseType, int, int, int)>(dayTwo.entryToResult);
        
        Assert.AreEqual(gameResults[0].Item4, 8);
        Assert.AreEqual(gameResults[1].Item4, 1);
        Assert.AreEqual(gameResults[2].Item4, 6);

        var total = gameResults.Aggregate(0, (acc, round) => acc + round.Item4);
        Assert.AreEqual(total, 15);
    }

    [Test]
    public void FirstProblemSolution()
    {
        var dayTwo = new DayTwo();

        var gameResults = LoadFile
            .fromPathAsList(@"samples/daytwo/problem2-problem.txt")
            .ToList()
            .ConvertAll<(ResponseType, int, int, int)>(dayTwo.entryToResult);
        
        var total = gameResults.Aggregate(0, (acc, round) => acc + round.Item4);
        
        Console.WriteLine("You will have won a total of {0} points", total);
        Assert.Pass();
    }

    [Test]
    public void SecondProblemExample()
    {
        var dayTwo = new DayTwo();

        var gameResults = LoadFile
            .fromPathAsList(@"samples/daytwo/problem2-example.txt")
            .ToList()
            .ConvertAll<(ResponseType, int, int, int)>(dayTwo.entryToResultByOutcome);
        
        Assert.AreEqual(gameResults[0].Item4, 4);
        Assert.AreEqual(gameResults[1].Item4, 1);
        Assert.AreEqual(gameResults[2].Item4, 7);

        var total = gameResults.Aggregate(0, (acc, round) => acc + round.Item4);
        Assert.AreEqual(total, 12);
    }

    [Test]
    public void SecondProblemSolution()
    {
        var dayTwo = new DayTwo();

        var gameResults = LoadFile
            .fromPathAsList(@"samples/daytwo/problem2-problem.txt")
            .ToList()
            .ConvertAll<(ResponseType, int, int, int)>(dayTwo.entryToResultByOutcome);
        
        var total = gameResults.Aggregate(0, (acc, round) => acc + round.Item4);
        
        Console.WriteLine("You will have won a total of {0} points", total);
        Assert.Pass();
    }
}