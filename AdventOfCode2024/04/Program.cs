// See https://aka.ms/new-console-template for more information

var letters = new List<char> {'X', 'M', 'A', 'S'};
var map = new List<List<char>>();
var rows = 0;
var cols = 0;
foreach (var line in File.ReadLines("/home/justastamosiunas/repos/AdventOfCode2024/04/input1.txt"))
{
    map.Add(new List<char>(line));
    rows++;
    cols = line.Length;
}

var sum = 0;
var sum2 = 0;
for (int i = 1; i < rows-1; i++)
{
    for (int j = 1; j < cols-1; j++)
    {
        if (map[i][j] == 'X')
        {
            var count = GetXmasCount(1, 0, 1, i, j) +
                        GetXmasCount(-1, 0, 1, i, j) +
                        GetXmasCount(0, 1, 1, i, j) +
                        GetXmasCount(0, -1, 1, i, j) +
                        GetXmasCount(-1, -1, 1, i, j) +
                        GetXmasCount(-1, 1, 1, i, j) +
                        GetXmasCount(1, 1, 1, i, j) +
                        GetXmasCount(1, -1, 1, i, j);
            sum += count;
        }

        if (map[i][j] == 'A')
        {
            if (GetIsCrossMas(i, j))
            {
                sum2++;
            }
        }
    }
}
Console.WriteLine(sum);
Console.WriteLine(sum2);
int GetXmasCount(int rowChange, int colChange, int nextLetterIndex, int currentRow, int currentCol)
{
    currentRow += rowChange;
    currentCol += colChange;
    if (currentRow >= rows || currentCol >= cols || currentRow < 0 || currentCol < 0)
    {
        return 0;
    }
    if (CheckIfCorrectLetter(currentRow, currentCol, nextLetterIndex))
    {
        if (nextLetterIndex == letters.Count - 1)
        {
            return 1;
        }
        return GetXmasCount(rowChange, colChange, nextLetterIndex + 1, currentRow, currentCol);
    }
    return 0;
}

bool GetIsCrossMas(int row, int column)
{
    var checkRow = row - 1;
    var checkColumn = column - 1;
    var mCoords = new List<(int x, int y)>();
    var foundChars = new List<char>();
    if (checkRow >= 0 && checkColumn >= 0)
    {
        foundChars.Add(map[checkRow][checkColumn]);
        if (map[checkRow][checkColumn] == 'M')
        {
            mCoords.Add((checkRow, checkColumn));
        }
    }
    checkRow = row - 1;
    checkColumn = column + 1;
    if (checkRow >= 0 && checkColumn < cols)
    {
        foundChars.Add(map[checkRow][checkColumn]);
        if (map[checkRow][checkColumn] == 'M')
        {
            mCoords.Add((checkRow, checkColumn));
        }
    }
    checkRow = row + 1;
    checkColumn = column - 1;
    if (checkRow < rows && checkColumn >= 0)
    {
        foundChars.Add(map[checkRow][checkColumn]);
        if (map[checkRow][checkColumn] == 'M')
        {
            mCoords.Add((checkRow, checkColumn));
        }
    }
    checkRow = row + 1;
    checkColumn = column + 1;
    if (checkRow < rows && checkColumn < cols)
    {
        foundChars.Add(map[checkRow][checkColumn]);
        if (map[checkRow][checkColumn] == 'M')
        {
            mCoords.Add((checkRow, checkColumn));
        }
    }

    if (foundChars.Count < 4)
    {
        return false;
    }

    if (foundChars.Count(x => x == 'M') == 2 && foundChars.Count(x => x == 'S') == 2)
    {
        var m1 = mCoords[0];
        var m2 = mCoords[1];
        if (m1.x != m2.x && m1.y != m2.y)
        {
            return false;
        }
        return true;
    }

    return false;
}

bool CheckIfCorrectLetter(int row, int col, int index)
{
    return map[row][col] == letters[index];
}