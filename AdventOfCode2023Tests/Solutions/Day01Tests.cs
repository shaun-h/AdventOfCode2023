using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day01Tests
{
    [Fact]
    public void Example_01_Should_Be_142()
    {
        var expected = 142;
        var result = Day01.Solve(Day01Inputs.Example01, false);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_55002()
    {
        var expected = 55002;
        var result = Day01.Solve(Day01Inputs.Real, false);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Example_02_Should_Be_281()
    {
        var expected = 281;
        var result = Day01.Solve(Day01Inputs.Example02, true);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_02_Should_Be_55093()
    {
        var expected = 55093;
        var result = Day01.Solve(Day01Inputs.Real, true);
        result.Should().Be(expected);
    }
}