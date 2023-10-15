using System;

namespace DefiningClasses
{
    public class DateModifier
    {
        private string startDate;
        private string endDate;

        public string StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public string EndDate
        {
            get => endDate;
            set => endDate = value;
        }

        public DateModifier(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public double DaysDifference()
        {
            DateTime firstDate = DateTime.ParseExact(startDate ,"yyyy MM dd", null);
            DateTime secondDate = DateTime.ParseExact(endDate ,"yyyy MM dd", null);
            double difference = (firstDate - secondDate).TotalDays;
            return Math.Abs(difference);
        }
    }
}