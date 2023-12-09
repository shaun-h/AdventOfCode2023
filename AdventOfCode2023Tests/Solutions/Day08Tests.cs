using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day08Tests
{
    [Fact]
    public void Example_01_Should_Be_6()
    {
        var expected = 6;
        var result = Day08.Solve(Day08Inputs.Example01);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_20093()
    {
        var expected = 20093;
        var result = Day08.Solve(Day08Inputs.Real);
        result.Should().Be(expected);
    }
    
    
}