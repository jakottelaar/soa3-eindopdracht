namespace AvansDevOps.Domain.Models.Sprints.Reports;
    public class ReportBuilder
    {
        private readonly Report _report;

        public ReportBuilder()
        {
            _report = new Report();
        }

        public ReportBuilder AddHeader(string header)
        {
            _report.SetHeader(header);
            return this;
        }

        public ReportBuilder AddFooter(string footer)
        {
            _report.SetFooter(footer);
            return this;
        }

        public ReportBuilder AddTeamComposition(string team)
        {
            _report.SetTeamComposition(team);
            return this;
        }

        public ReportBuilder AddBurndownChart(string chart)
        {
            _report.SetBurndownChart(chart);
            return this;
        }

        public ReportBuilder AddEffortPerDeveloper(string effort)
        {
            _report.SetEffortPerDeveloper(effort);
            return this;
        }

        public ReportBuilder SetFormat(string format)
        {
            _report.SetFormat(format);
            return this;
        }

        public Report Build()
        {
            return _report;
        }
    }