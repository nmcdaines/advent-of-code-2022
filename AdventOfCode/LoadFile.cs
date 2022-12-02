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

    public static string[] fromPathAsList(string filepath)
    {
        var finalPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "../../../" + filepath
        );

        return File.ReadAllLines(finalPath);
    }
}
