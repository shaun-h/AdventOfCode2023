using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day03Tests
{
    [Fact]
    public void Example_01_Should_Be_4361()
    {
        var expected = 4361;
        var result = Day03.Solve(Day03Inputs.Example01);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_539637()
    {
        var expected = 539637;
        var result = Day03.Solve(Day03Inputs.Real);
        result.Should().Be(expected);
    }
    
}