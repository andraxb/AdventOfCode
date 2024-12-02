// read the data from the file
using System;

var data = File.ReadAllLines("data");
int safeReports = 0;
// Part 1
foreach (var line in data)
{
    var levels = line.Split(' ').Select(int.Parse).ToArray();
    bool increasing = isIncreasing(levels[0], levels[^1]);
    bool steady = true;
    for (int i = 0; i < levels.Length - 1; i++)
    {
        if (!SteadyFlow(levels[i], levels[i + 1], increasing))
        {
            steady = false;
            // Console.Write($"{levels[i]} false");
            break;
        }

        if (!NoSpikes(levels[i], levels[i + 1]))
        {
            steady = false;
            // Console.Write($"{levels[i]} false");
            break;
        }
        // Console.Write($"{levels[i]} ");
    }
    Console.WriteLine();
    if (steady)
    {
        safeReports++;
    }
    // Console.WriteLine($"{line} {steady}");
}
// endresult
Console.WriteLine(safeReports);

static bool isIncreasing(int level1, int level2)
{
    return level1 < level2;
}

// Rule 1: The levels are either all increasing or all decreasing.
static bool SteadyFlow(int level1, int level2, bool isIncreasing)
{
    if (isIncreasing)
    {
        return level1 < level2;
    }
    else
    {
        return level1 > level2;
    }
}

// Any two adjacent levels differ by at least one and at most three.
static bool NoSpikes(int level1, int level2)
{
    var diff = Math.Abs(level1 - level2);
    return diff >= 1 && diff <= 3;
}

// Part 2
// reset reports for part 2
safeReports = 0;

foreach (var line in data)
{
    var levels = line.Split(' ').Select(int.Parse).ToList();
    bool increasing = isIncreasing(levels[0], levels[^1]);

    bool steady = false;
    steady = RunLevelTests(levels, increasing);
    if (steady)
    {
        // Console.WriteLine("Safe");
        safeReports++;
        // break;
    }
    else
    {
        // retry 
        for (int i = 0; i < levels.Count; i++)
        {
            var modifiedReport = levels.Where((_, index) => index != i).ToArray();
            increasing = isIncreasing(modifiedReport[0], modifiedReport[1]);
            steady = RunLevelTests(modifiedReport.ToList(), increasing);
            if (steady)
            {
                safeReports++;
                break;
            }
        }
    }
}


// endresult
Console.WriteLine(safeReports);

static bool RunLevelTests(List<int> levels, bool increasing)
{
    // Console.WriteLine($"Running level tests for: {string.Join(" ", levels)}");

    // bool steady = true;
    for (int i = 0; i < levels.Count - 1; i++)
    {
        if (!SteadyFlow(levels[i], levels[i + 1], increasing))
        {
            return false;
        }

        if (!NoSpikes(levels[i], levels[i + 1]))
        {
            return false;
        }
    }
    return true;
}