namespace AvansDevOps.Domain.Models.Sprints.Reports;
    public class ReportBuilder
    {
        private readonly Report report;

        public ReportBuilder()
        {
            report = new Report();
        }

        public ReportBuilder AddHeader(string header)
        {
            report.SetHeader(header);
            return this;
        }

        public ReportBuilder AddFooter(string footer)
        {
            report.SetFooter(footer);
            return this;
        }

        public ReportBuilder AddTeamComposition(string team)
        {
            report.SetTeamComposition(team);
            return this;
        }

        public ReportBuilder AddBurndownChart(string chart)
        {
            report.SetBurndownChart(chart);
            return this;
        }

        public ReportBuilder AddEffortPerDeveloper(string effort)
        {
            report.SetEffortPerDeveloper(effort);
            return this;
        }

        public ReportBuilder SetFormat(string format)
        {
            report.SetFormat(format);
            return this;
        }

        public ReportBuilder AddSummary(string summary)
        {
            report.SetSummary(summary);
            return this;
        }

        public Report Build()
        {
            return report;
        }
    }