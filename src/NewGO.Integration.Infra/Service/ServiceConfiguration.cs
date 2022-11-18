using Keeggo.Integration.Infra;

namespace NewGO.Integration.Infra
{
    public class ServiceConfiguration
    {
        public int Interval { get; set; }
        public string? Start { get; set; }
        public string? End { get; set; }

        public bool IsRun()
        {
            DateTime start = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {Start}".ToDateTimeBR();
            DateTime end = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {End}".ToDateTimeBR();

            if (end < start)
                end = end.AddDays(1);

            return DateTime.Now >= start && DateTime.Now <= end;
        }
    }
}
