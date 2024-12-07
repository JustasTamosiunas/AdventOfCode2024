// See https://aka.ms/new-console-template for more information

var operations = new List<(long key, List<long> operations)>();
foreach (var line in File.ReadLines("/home/justastamosiunas/repos/AdventOfCode2024Repo/AdventOfCode2024/07/input1.txt"))
{
    var parts = line.Split(":");
    var sum = long.Parse(parts[0]);
    var operationsList = new List<long>();
    var test = parts[1].Trim().Split(" ");
    foreach (var operation in parts[1].Trim().Split(" "))
    {
        operationsList.Add(long.Parse(operation.Trim()));
    }
    operations.Add((sum, operationsList));
}

Part1();

void Part1()
{
    long sum = 0;
    foreach (var operation in operations)
    {
        var permutations = new List<string>();
        GetOperationPermutations(operation.operations.Count - 1, 0, "", permutations);
        if (TryPermutations(operation.key, permutations, operation.operations))
        {
            sum += operation.key;
        }
    }
    Console.WriteLine(sum);
}


bool TryPermutations(long expectedResult, List<string> permutations, List<long> numbers)
{
    foreach (var permutation in permutations)
    {
        var currentSum = numbers[0];
        for (int i = 0; i < permutation.Length; i++)
        {
            if (permutation[i] == '+')
            {
                currentSum += numbers[i + 1];
            }
            else if (permutation[i] == '*')
            {
                currentSum *= numbers[i + 1];
            }
            else if (permutation[i] == '|')
            {
                var left = currentSum.ToString();
                var right = numbers[i + 1].ToString();
                currentSum = long.Parse(left + right);
            }
        }

        if (currentSum == expectedResult)
        {
            return true;
        }
    }

    return false;
}

void GetOperationPermutations(int count, int currentCount, string current, List<string> result)
{
    if (current.Length == count)
    {
        result.Add(current);
        return;
    }
    else
    {
        GetOperationPermutations(count, currentCount + 1, current + "+", result);
        GetOperationPermutations(count, currentCount + 1, current + "*", result);
        GetOperationPermutations(count, currentCount + 1, current + "|", result);
    }
}