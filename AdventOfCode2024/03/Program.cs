// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var input = File.ReadAllText("input1.txt");

var regex = new Regex(@"mul\(\d+,\d+\)|do\(\)|don't\(\)");

var matches = regex.Matches(input);
var sum = 0;
var enabled = true;
foreach (var match in matches)
{
    var text = match.ToString();
    if (text == "do()")
    {
        enabled = true;
        continue;
    }

    if (text == "don't()")
    {
        enabled = false;
        continue;
    }

    if (!enabled)
    {
        continue;
    }
    
    text = text.Substring(4);
    text = text.Replace(')', ' ');
    var postSplit = text.Split(',');
    sum += int.Parse(postSplit[0]) * int.Parse(postSplit[1]);
}

Console.WriteLine(sum);
Console.WriteLine("Hello, World!");
var result = 0;