namespace EDriveRent.Models
{
    using Contracts;
    using System;
    using Utilities.ExceptionMessages;

    public class Route : IRoute
    {
        private string startPoint;
        private string endPoint;
        private double length;
        private int routeId;
        private bool isLocked;

        public Route(string startPoint, string endPoint, double length, int routeId)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.length = length;
            this.routeId = routeId;
            this.isLocked = false;
        }

        public string StartPoint
        {
            get => this.startPoint;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.STRING_IS_NOT_VALID,
                        nameof(this.StartPoint)));

                this.startPoint = value;
            }
        }

        public string EndPoint
        {
            get => this.endPoint;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.STRING_IS_NOT_VALID,
                        nameof(this.EndPoint)));

                this.endPoint = value;
            }
        }

        public double Length
        {
            get => this.length;
            private set
            {
                if (value < 1)
                    throw new ArgumentException(ExceptionMessages.LENGTH_CANNOT_BE_LESS_THAN_1_KM);

                this.Length = this.length;
            }
        }

        public int RouteId => this.routeId;

        public bool IsLocked => this.isLocked;

        public void LockRoute() => this.isLocked = true;
    }
}