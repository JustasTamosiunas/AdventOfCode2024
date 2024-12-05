// See https://aka.ms/new-console-template for more information

var result = 0;
foreach(var line in File.ReadLines("input.txt"))
{
    var numbers = line.Split(' ').Select(x => int.Parse(x)).ToList();

    for (var i = -1; i < numbers.Count; i++)
    {
        var copy = numbers.GetRange(0, numbers.Count);
        if (i != -1)
        {
            copy.RemoveAt(i);
        }

        if (IsSafe(copy))
        {
            result++;
            break;
        }
    }
}

bool IsSafe(List<int> numbers)
{
    var copy = numbers.GetRange(0, numbers.Count);
    copy.Sort();
    if (!copy.SequenceEqual(numbers))
    {
        copy.Reverse();
        if (!copy.SequenceEqual(numbers))
        {
            return false;
        }
    }

    for (var i = 1; i < numbers.Count; i++)
    {
        if (Math.Abs(numbers[i] - numbers[i - 1]) < 1)
        {
            return false;
        }
        if (Math.Abs(numbers[i] - numbers[i - 1]) > 3)
        {
            return false;
        }
    }

    return true;
}

Console.WriteLine(result);
