using System.Diagnostics;

namespace AdventOfCode2023.Solutions;

public static class Day03
{
    public static int Solve(string input)
    {
        var lines = input.Split(Environment.NewLine);

        var possibleParts = new List<PossiblePart>();
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            var aboveLine = y > 0 ? lines[y - 1] : null;
            var belowLine = y + 1 < lines.Length ? lines[y + 1] : null;
            var currentNumber = "";
            var isPart = false;
            for (var x = 0; x < line.Length; x++)
            {
                if (char.IsDigit(line[x]))
                {
                    currentNumber += line[x];
                    //Left
                    if (x - 1 >= 0)
                    {
                        
                        if (IsSymbol(line, x - 1))
                        {
                            isPart = true;
                        }

                        //TopLeft
                        if (aboveLine != null)
                        {
                            if (IsSymbol(aboveLine, x - 1))
                            {
                                isPart = true;
                            }
                        }
                        //BottomLeft
                        if (belowLine != null)
                        {
                            if (IsSymbol(belowLine, x - 1))
                            {
                                isPart = true;
                            }
                        }
                    }
                    
                    //Above
                    if (aboveLine != null)
                    {
                        if (IsSymbol(aboveLine, x))
                        {
                            isPart = true;
                        }
                    }
                    
                    //Below
                    if (belowLine != null)
                    {
                        if (IsSymbol(belowLine, x))
                        {
                            isPart = true;
                        }
                    }
                    
                    //Right
                    if (x + 1 < line.Length)
                    {
                        if (IsSymbol(line, x + 1))
                        {
                            isPart = true;
                        }

                        //TopRight
                        if (aboveLine != null)
                        {
                            if (IsSymbol(aboveLine, x + 1))
                            {
                                isPart = true;
                            }
                        }
                        //BottomRight
                        if (belowLine != null)
                        {
                            if (IsSymbol(belowLine, x + 1))
                            {
                                isPart = true;
                            }
                        }
                    }
                    
                    
                    
                    
                    
                    
                }
                else if(currentNumber != "")
                {
                    possibleParts.Add(new PossiblePart(int.Parse(currentNumber), isPart));
                    currentNumber = "";
                    isPart = false;
                }
            }

            if (currentNumber != "")
            {
                possibleParts.Add(new PossiblePart(int.Parse(currentNumber), isPart));
            }
        }

        return possibleParts.Aggregate(0, (i, i2) => i + (i2.IsPart ? i2.Number : 0));
    }

    private static bool IsSymbol(string line, int position)
    {
        var c = line[position];

        if (c == '.') return false;
        if (char.IsNumber(c)) return false;

        char[] symbols = ['*', '#', '+', '$', '%', '@', '/', '=', '-', '&'];

        if (symbols.Contains(c)) return true;
            
        return false;
    }
}

public record PossiblePart(int Number, bool IsPart);