// See https://aka.ms/new-console-template for more information

var map = new List<List<char>>();
foreach (var line in File.ReadLines("/home/justastamosiunas/repos/AdventOfCode2024Repo/AdventOfCode2024/08/input1.txt"))
{
    map.Add([..line]);
}

var antennas = new Dictionary<char, List<(int x, int y)>>();
for (int y = 0; y < map.Count; y++)
{
    for (int x = 0; x < map[y].Count; x++)
    {
        if (map[y][x] != '.')
        {
            if (!antennas.ContainsKey(map[y][x]))
            {
                antennas[map[y][x]] = new List<(int x, int y)>();
            }
            antennas[map[y][x]].Add((x, y));
        }
    }
}

var antinodes = new List<(int x, int y)>();

foreach (var antennaList in antennas)
{
    var combinations =
        antennaList.Value.SelectMany((x, i) => antennaList.Value.Skip(i + 1), (x, y) => Tuple.Create(x, y));
    foreach (var (antenna1, antenna2) in combinations)
    {
        var an1DirectionDone = false;
        var an2DirectionDone = false;
        if (!antinodes.Contains(antenna1))
        {
            antinodes.Add(antenna1);
        }
        if (!antinodes.Contains(antenna2))
        {
            antinodes.Add(antenna2);
        }
        var currentAntenna1 = antenna1;
        var currentAntenna2 = antenna2;
        
        var an1xStep = 0;
        var an2xStep = 0;
        var an1yStep = 0;
        var an2yStep = 0;
        an1xStep = antenna1.x - antenna2.x;
        an1yStep = antenna1.y - antenna2.y;
        an2xStep = antenna2.x - antenna1.x;
        an2yStep = antenna2.y - antenna1.y;
        while (!an1DirectionDone || !an2DirectionDone)
        {
            currentAntenna1 = (currentAntenna1.x + an1xStep, currentAntenna1.y + an1yStep);
            currentAntenna2 = (currentAntenna2.x + an2xStep, currentAntenna2.y + an2yStep);
            if (!an1DirectionDone)
            {
                if (currentAntenna1.x < 0 || currentAntenna1.x >= map[0].Count || currentAntenna1.y < 0 ||
                    currentAntenna1.y >= map.Count)
                {
                    an1DirectionDone = true;
                }
                else
                {
                    if (!antinodes.Contains(currentAntenna1))
                    {
                        antinodes.Add(currentAntenna1);
                    }
                }
            }

            if (!an2DirectionDone)
            {
                if (currentAntenna2.x < 0 || currentAntenna2.x >= map[0].Count || currentAntenna2.y < 0 ||
                    currentAntenna2.y >= map.Count)
                {
                    an2DirectionDone = true;
                }
                else
                {
                    if (!antinodes.Contains(currentAntenna2))
                    {
                        antinodes.Add(currentAntenna2);
                    }
                }
            }
        }
    }
}

foreach (var coordinate in antinodes)
{
    if (map[coordinate.y][coordinate.x] != '.')
    {
        continue;
    }
    else
    {
        map[coordinate.y][coordinate.x] = '#';
    }
}

foreach (var line in map)
{
    Console.WriteLine(new string(line.ToArray()));
}

Console.WriteLine(antinodes.Count);