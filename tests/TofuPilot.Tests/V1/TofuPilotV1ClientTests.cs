using FluentAssertions;
using TofuPilot.V1.Utils;
using Xunit;

namespace TofuPilot.Tests.V1;

public class TofuPilotV1ClientTests
{
    [Fact]
    public void DateTimeHelper_ToIso8601_FormatsCorrectly()
    {
        // Arrange
        var dateTime = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);

        // Act
        var result = DateTimeHelper.ToIso8601(dateTime);

        // Assert
        result.Should().Be("2024-01-15T10:30:00.0000000+00:00");
    }

    [Fact]
    public void DateTimeHelper_ToIso8601Duration_FormatsCorrectly()
    {
        // Arrange
        var duration = TimeSpan.FromMinutes(5);

        // Act
        var result = DateTimeHelper.ToIso8601Duration(duration);

        // Assert
        result.Should().Be("PT5M");
    }

    [Fact]
    public void DateTimeHelper_FromMilliseconds_ConvertsCorrectly()
    {
        // Arrange
        var milliseconds = 1705315800000L; // 2024-01-15T10:30:00Z

        // Act
        var result = DateTimeHelper.FromMilliseconds(milliseconds);

        // Assert
        result.Year.Should().Be(2024);
        result.Month.Should().Be(1);
        result.Day.Should().Be(15);
    }

    [Fact]
    public void FileValidator_ValidateFiles_ThrowsForNonExistentFile()
    {
        // Arrange
        var files = new[] { "/path/to/nonexistent/file.txt" };

        // Act & Assert
        var action = () => FileValidator.ValidateFiles(files);
        action.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void FileValidator_ValidateFiles_ThrowsForTooManyFiles()
    {
        // Arrange
        var files = Enumerable.Range(0, 25).Select(i => $"/file{i}.txt").ToArray();

        // Act & Assert
        var action = () => FileValidator.ValidateFiles(files, maxAttachments: 20);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*more than 20 attachments*");
    }
}
