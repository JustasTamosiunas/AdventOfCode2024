// See https://aka.ms/new-console-template for more information

Dictionary<int, List<int>> rules = new();
List<string> updates = new();

bool isRulesMode = true;
foreach (var line in File.ReadLines("/home/justastamosiunas/repos/AdventOfCode2024/05/input1.txt"))
{
    if (line == string.Empty)
    {
        isRulesMode = false;
        continue;
    }
    
    if (isRulesMode)
    {
        var split = line.Split("|");
        var page1 = int.Parse(split[0]);
        var page2 = int.Parse(split[1]);
        if (rules.TryGetValue(page1, out var list))
        {
            list.Add(page2);
        }
        else
        {
            rules.Add(page1, new List<int> { page2 });
        }
        continue;
    }
    updates.Add(line);
}


Part1();
Part2();


void Part1()
{
    var sum = 0;
    foreach (var update in updates)
    {
        var printed = new HashSet<int>();
        var pages = update.Split(",").Select(int.Parse).ToList();
        var isValid = true;
        foreach (var page in pages)
        {
            var doesRuleExist = rules.TryGetValue(page, out var rule);
            if (doesRuleExist && printed.Any(x => rule.Contains(x)))
            {
                isValid = false;
                break;
            }
            printed.Add(page);
        }

        if (isValid)
        {
            sum += pages[pages.Count/2];
        }
    }
    Console.WriteLine(sum);
}

void Part2()
{
    var sum = 0;
    var invalid = new List<string>();
    foreach (var update in updates)
    {
        var printed = new HashSet<int>();
        var pages = update.Split(",").Select(int.Parse).ToList();
        var isValid = true;
        foreach (var page in pages)
        {
            var doesRuleExist = rules.TryGetValue(page, out var rule);
            if (doesRuleExist && printed.Any(x => rule.Contains(x)))
            {
                isValid = false;
                break;
            }
            printed.Add(page);
        }

        if (!isValid)
        {
            invalid.Add(update);
        }
    }

    foreach (var update in invalid)
    {
        var pages = update.Split(",").Select(int.Parse).ToList();
        SortByRules(pages);
        sum += pages[pages.Count/2];
    }
    Console.WriteLine(sum);
}

void SortByRules(List<int> list)
{
    list.Sort((a, b) =>
    {
        var doesARuleExist = rules.TryGetValue(a, out var ruleA);
        if (doesARuleExist && ruleA.Contains(b))
        {
            return -1;
        }
        
        var doesBRuleExist = rules.TryGetValue(b, out var ruleB);
        if (doesBRuleExist && ruleB.Contains(a))
        {
            return 1;
        }
        
        return 0;
    });
}