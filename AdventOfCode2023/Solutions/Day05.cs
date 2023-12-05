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
                
                currentMappings.Add(new Mapping(fromMap, fromMap + mapping[2], toMap));
            }
        }
    }

    private static long GetMapping(long key, List<Mapping> mappings)
    {
        var mapping = mappings.SingleOrDefault(m => m.SourceStart <= key && m.SourceEnd > key);
        if (mapping == null) return key;

        var sourceDiff = key - mapping.SourceStart;
        return mapping.DestinaionStart + sourceDiff;
        
    }

    private static List<long> ParseSeeds(string input)
    {
        var rawSeeds = input.Split(":")[1];
        return rawSeeds.Split(" ").Where(x => x != "").Select(s => long.Parse(s.Trim())).ToList();
    }
}

record Mapping(long SourceStart, long SourceEnd, long DestinaionStart)
{
    
}