using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day05Tests
{
    [Fact]
    public void Example_01_Should_Be_35()
    {
        var expected = 35;
        var result = Day05.Solve(Day05Inputs.Example01);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_331445006()
    {
        var expected = 331445006;
        var result = Day05.Solve(Day05Inputs.Real);
        result.Should().Be(expected);
    }
}