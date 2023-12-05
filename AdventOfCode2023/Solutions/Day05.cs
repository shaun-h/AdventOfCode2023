using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day05
{
    public static long Solve(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var seeds = ParseSeeds(lines.First());

        SetupMappings(lines, out var soilMap, out var fertilizerMap, out var waterMap, out var lightMap, out var temperatureMap, out var humidityMap, out var locationMap);

        var lowest = -1l;
        foreach (var seed in seeds)
        {
            var currentKey = seed;

            currentKey = GetMapping(currentKey, soilMap);
            currentKey = GetMapping(currentKey, fertilizerMap);
            currentKey = GetMapping(currentKey, waterMap);
            currentKey = GetMapping(currentKey, lightMap);
            currentKey = GetMapping(currentKey, temperatureMap);
            currentKey = GetMapping(currentKey, humidityMap);
            currentKey = GetMapping(currentKey, locationMap);
            
            if (lowest == -1 || lowest > currentKey)
            {
                lowest = currentKey;
            }
        }

        return lowest;
    }

    public static long SolveRange(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var seeds = ParseSeeds(lines.First());

        SetupMappings(lines, out var soilMap, out var fertilizerMap, out var waterMap, out var lightMap, out var temperatureMap, out var humidityMap, out var locationMap);
        
        var lowest = -1l;
        for (var i = 0; i < seeds.Count; i += 2)
        {
            var startOfSeed = seeds[i];
            var endOfSeed = seeds[i] + seeds[i + 1];

            var currentRanges = SolveForRangeAndMapping(startOfSeed, endOfSeed, soilMap);
            currentRanges = SolveForRangesAndMapping(currentRanges, fertilizerMap);
            currentRanges = SolveForRangesAndMapping(currentRanges, waterMap);
            currentRanges = SolveForRangesAndMapping(currentRanges, lightMap);
            currentRanges = SolveForRangesAndMapping(currentRanges, temperatureMap);
            currentRanges = SolveForRangesAndMapping(currentRanges, humidityMap);
            currentRanges = SolveForRangesAndMapping(currentRanges, locationMap);

            var rangeLowest = currentRanges.MinBy(r => r.start).start;
            if (lowest == -1 || rangeLowest < lowest)
            {
                lowest = rangeLowest;
            }
        }

        return lowest;
    }

    private static List<(long start, long end)> SolveForRangesAndMapping(List<(long start, long end)> ranges,
        List<Mapping> map)
    {
        var rangesToReturn = new List<(long start, long end)>();

        foreach (var range in ranges)
        {
            rangesToReturn.AddRange(SolveForRangeAndMapping(range.start, range.end, map));
        }

        return rangesToReturn;
    }

    private static List<(long start, long end)> SolveForRangeAndMapping(long start, long end, List<Mapping> map)
    {
        var mappings = GetMappingsForRange(start, end, map);
        if (!mappings.Any()) return new List<(long start, long end)> { (start, end) };
        var currentStart = start;
        var splitRangesToSolve = new List<(long start, long end)>();
        if (start < mappings.First().SourceStart)
        {
            splitRangesToSolve.Add((start, mappings.First().SourceStart-1));
            currentStart = mappings.First().SourceStart;
        }
        
        foreach (var mapping in mappings)
        {
            var currentEnd = Math.Min(mapping.SourceEnd, end);
            splitRangesToSolve.Add((currentStart, currentEnd));
            currentStart = currentEnd + 1;
        }
        
        if (end > mappings.Last().SourceEnd)
        {
            splitRangesToSolve.Add((currentStart, end));
            
        }

        var solvedRanges = new List<(long start, long end)>();

        foreach (var range in splitRangesToSolve)
        {
            var s = GetMapping(range.start, map);
            var e = GetMapping(range.end, map);
            solvedRanges.Add((s, e));
        }
        return solvedRanges;
    }

    private static void SetupMappings(string[] lines, out List<Mapping> soilMap, out List<Mapping> fertilizerMap, out List<Mapping> waterMap, out List<Mapping> lightMap,
        out List<Mapping> temperatureMap, out List<Mapping> humidityMap, out List<Mapping> locationMap)
    {
        soilMap = new List<Mapping>();
        fertilizerMap = new List<Mapping>();
        waterMap = new List<Mapping>();
        lightMap = new List<Mapping>();
        temperatureMap = new List<Mapping>();
        humidityMap = new List<Mapping>();
        locationMap = new List<Mapping>();

        List<Mapping>? currentMappings = null;
        
        foreach (var line in lines)
        {
            if (line == "seed-to-soil map:")
            {
                currentMappings = soilMap;
            }
            else if (line == "soil-to-fertilizer map:")
            {
                currentMappings = fertilizerMap;
            }
            else if (line == "fertilizer-to-water map:")
            {
                currentMappings = waterMap;
            }
            else if (line == "water-to-light map:")
            {
                currentMappings = lightMap;
            }
            else if (line == "light-to-temperature map:")
            {
                currentMappings = temperatureMap;
            }
            else if (line == "temperature-to-humidity map:")
            {
                currentMappings = humidityMap;
            }
            else if (line == "humidity-to-location map:")
            {
                currentMappings = locationMap;
            }
            else if (currentMappings != null)
            {
                if (line == string.Empty)
                {
                    currentMappings = null;
                    continue;
                }
                var mapping = line.Split(" ").Where(x => x != "").Select(long.Parse).ToList();
                if (mapping.Count != 3)
                {
                    throw new Exception("Bad Mapping");
                }
                
                var fromMap = mapping[1];
                var toMap = mapping[0];

                currentMappings.Add(new Mapping(fromMap, fromMap + mapping[2] - 1, toMap));
            }
        }

        soilMap = soilMap.OrderBy(x => x.SourceStart).ToList();
        fertilizerMap = fertilizerMap.OrderBy(x => x.SourceStart).ToList();
        waterMap = waterMap.OrderBy(x => x.SourceStart).ToList();
        lightMap = lightMap.OrderBy(x => x.SourceStart).ToList();
        temperatureMap = temperatureMap.OrderBy(x => x.SourceStart).ToList();
        humidityMap = humidityMap.OrderBy(x => x.SourceStart).ToList();
        locationMap = locationMap.OrderBy(x => x.SourceStart).ToList();
        
    }

    private static long GetMapping(long key, List<Mapping> mappings)
    {
        var mapping = mappings.SingleOrDefault(m => m.SourceStart <= key && m.SourceEnd >= key);
        if (mapping == null) return key;

        var sourceDiff = key - mapping.SourceStart;
        return mapping.DestinationStart + sourceDiff;
        
    }
    
    private static List<Mapping> GetMappingsForRange(long startKey, long endkey, List<Mapping> mappings)
    {
        return mappings.Where(m => m.SourceStart <= endkey && m.SourceEnd >= startKey).OrderBy(m => m.SourceStart).ToList();
    }

    private static List<long> ParseSeeds(string input)
    {
        var rawSeeds = input.Split(":")[1];
        return rawSeeds.Split(" ").Where(x => x != "").Select(s => long.Parse(s.Trim())).ToList();
    }
}



record Mapping(long SourceStart, long SourceEnd, long DestinationStart);