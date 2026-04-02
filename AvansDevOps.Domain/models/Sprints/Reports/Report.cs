namespace AvansDevOps.Domain.Models.Sprints.Reports;
    public class Report
    {
        public string Header { get; private set; }
        public string Footer { get; private set; }

        public string TeamComposition { get; private set; }
        public string BurndownChart { get; private set; }
        public string EffortPerDeveloper { get; private set; }

        public string Format { get; private set; }

        internal void SetHeader(string header)
        {
            Header = header;
        }

        internal void SetFooter(string footer)
        {
            Footer = footer;
        }

        internal void SetTeamComposition(string teamComposition)
        {
            TeamComposition = teamComposition;
        }

        internal void SetBurndownChart(string burndownChart)
        {
            BurndownChart = burndownChart;
        }

        internal void SetEffortPerDeveloper(string effort)
        {
            EffortPerDeveloper = effort;
        }

        internal void SetFormat(string format)
        {
            Format = format;
        }
    }
