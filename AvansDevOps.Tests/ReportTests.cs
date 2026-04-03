using AvansDevOps.Domain.Models.Sprints.Reports;

namespace AvansDevOps.Tests;

public class ReportTests
{
    //TC-39:
    [Fact]
    public void GeneratesSprintReport()
    {
        var report = new ReportBuilder()
            .AddSummary("Sprint completed")
            .Build();

        Assert.Equal("Sprint completed", report.Summary);
    }

    //TC-40:
    [Fact]
    public void AddsHeaderAndFooterToReport()
    {
        var report = new ReportBuilder()
            .AddHeader("Header")
            .AddFooter("Footer")
            .Build();

        Assert.Equal("Header", report.Header);
        Assert.Equal("Footer", report.Footer);
    }

    //TC-41:
    [Fact]
    public void SupportsPdfAndPngExportFormats()
    {
        var pdfReport = new ReportBuilder().SetFormat("PDF").Build();
        var pngReport = new ReportBuilder().SetFormat("PNG").Build();

        Assert.Equal("PDF", pdfReport.Format);
        Assert.Equal("PNG", pngReport.Format);
    }

    //TC-42:
    [Fact]
    public void WorksWithoutHeaderAndFooter()
    {
        var report = new ReportBuilder()
            .AddSummary("No chrome")
            .Build();

        Assert.Null(report.Header);
        Assert.Null(report.Footer);
        Assert.Equal("No chrome", report.Summary);
    }

    //TC-43:
    [Fact]
    public void ProducesDifferentOutputForDifferentBuilderConfigurations()
    {
        var reportA = new ReportBuilder()
            .AddHeader("Sprint A")
            .SetFormat("PDF")
            .AddSummary("Summary A")
            .Build();

        var reportB = new ReportBuilder()
            .AddHeader("Sprint B")
            .SetFormat("PNG")
            .AddSummary("Summary B")
            .Build();

        Assert.NotEqual(reportA.Header, reportB.Header);
        Assert.NotEqual(reportA.Format, reportB.Format);
        Assert.NotEqual(reportA.Summary, reportB.Summary);
    }
}