using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day02Tests
{
    [Fact]
    public void Example_01_Should_Be_8()
    {
        var expected = 8;
        var result = Day02.Solve(Day02Inputs.Example01, 12, 13, 14);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_2156()
    {
        var expected = 2156;
        var result = Day02.Solve(Day02Inputs.Real, 12, 13, 14);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Example_02_Should_Be_2286()
    {
        var expected = 2286;
        var result = Day02.SolveMinimum(Day02Inputs.Example02);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_02_Should_Be_66909()
    {
        var expected = 66909;
        var result = Day02.SolveMinimum(Day02Inputs.Real);
        result.Should().Be(expected);
    }
}