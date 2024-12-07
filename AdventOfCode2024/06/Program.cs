// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;

var map = new List<List<char>>();
var startX = 0;
var startY = 0;

var foundStart = false;

foreach (var line in File.ReadLines("/home/justastamosiunas/repos/AdventOfCode2024Repo/AdventOfCode2024/06/input1.txt"))
{
    if (!foundStart)
    {
        if (line.Contains('^'))
        {
            startY = line.IndexOf('^');
            foundStart = true;
        }
    }
    
    map.Add([..line]);

    if (!foundStart)
    {
        startX++;
    }
}
Part2();
void Part1()
{
    var sum = 1;
    var xChange = -1;
    var yChange = 0;
    var x = startX;
    var y = startY;
    var currentDirection = "up";
    map[x][y] = '@';
    while (true)
    {
        if (x + xChange < 0 || x + xChange >= map.Count || y + yChange < 0 || y + yChange >= map[0].Count)
        {
            break;
        }
        if (map[x + xChange][y + yChange] != '#')
        {
            x += xChange;
            y += yChange;
            if (map[x][y] != '@')
            {
                sum++;
                map[x][y] = '@';
            }
        }
        else
        {
            var newDirection = NextDirection(currentDirection);
            xChange = newDirection.dir.xChange;
            yChange = newDirection.dir.yChange;
            currentDirection = newDirection.direction;
        }
    }
    Console.WriteLine(sum);
}

void Part2()
{
    var mapCopy = new List<List<char>>();
    foreach (var line in map)
    {
        mapCopy.Add(new List<char>(line));
    }
    
    var sum = 0;
    var xChange = -1;
    var yChange = 0;
    var x = startX;
    var y = startY;
    var currentDirection = "up";
    map[x][y] = '@';
    while (true)
    {
        if (x + xChange < 0 || x + xChange >= map.Count || y + yChange < 0 || y + yChange >= map[0].Count)
        {
            break;
        }
        if (map[x + xChange][y + yChange] != '#')
        {
            if (map[x + xChange][y + yChange] != '!')
            {
                var changeDirection = NextDirection(currentDirection);
                if (CanBeInfinite(x, y, changeDirection.dir.xChange, changeDirection.dir.yChange,
                        changeDirection.direction,x + xChange, y + yChange))
                {
                    sum++;
                    map[x+xChange][y+yChange] = '!';
                    Console.WriteLine(sum);
                }
            }
            x += xChange;
            y += yChange;
        }
        else
        {
            var newDirection = NextDirection(currentDirection);
            xChange = newDirection.dir.xChange;
            yChange = newDirection.dir.yChange;
            currentDirection = newDirection.direction;
        }
    }
    Console.WriteLine(sum);
}

bool CanBeInfinite(int startX, int startY, int xChange, int yChange, string changeDirection, int obstacleX, int obstacleY)
{
    var mapCopy = new List<List<char>>();
    foreach (var line in map)
    {
        mapCopy.Add(new List<char>(line));
    }
    mapCopy[obstacleX][obstacleY] = 'A';
    var dict = new Dictionary<(int x, int y), List<string>>();
    var x = startX;
    var y = startY;
    dict[(x, y)] = new List<string>{changeDirection};
    while (true)
    {
        if (x + xChange < 0 || x + xChange >= map.Count || y + yChange < 0 || y + yChange >= map[0].Count)
        {
            return false;
        }
        
        if (mapCopy[x + xChange][y+yChange] != '#' && mapCopy[x + xChange][y+yChange] != 'A')
        {
            mapCopy[x][y] = '@';
            x += xChange;
            y += yChange;
            var valueExists = dict.TryGetValue((x, y), out var dirList);
            if (valueExists)
            {
                if (dirList.Contains(changeDirection))
                {
                    
                    foreach (var line in mapCopy)
                    {
                        Console.WriteLine(string.Join("", line));
                    }
                    return true;
                }

                dirList.Add(changeDirection);
            }
            else
            {
                dict[(x, y)] = new List<string>{changeDirection};
            }
        }
        else
        {
            var newDirection = NextDirection(changeDirection);
            xChange = newDirection.dir.xChange;
            yChange = newDirection.dir.yChange;
            changeDirection = newDirection.direction;
        }
    }
}

((int xChange, int yChange) dir, string direction) NextDirection(string currentDirection)
{
    return currentDirection switch
    {
        "up" => ((0, 1), "right"),
        "right" => ((1, 0), "down"),
        "down" => ((0, -1), "left"),
        _ => ((-1, 0), "up")
    };
}