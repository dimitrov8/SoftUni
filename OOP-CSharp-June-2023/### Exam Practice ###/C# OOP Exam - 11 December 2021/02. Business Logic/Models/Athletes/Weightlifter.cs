namespace Gym.Models.Athletes
{
    using System;
    using Utilities.Messages;

    public class Weightlifter : Athlete
    {
        private const int INITIAL_STAMINA = 50;
        private const int INCREASE_STAMINA_VALUE = 10;

        public Weightlifter(string fullName, string motivation, int numberOfMedals)
            : base(fullName, motivation, numberOfMedals, INITIAL_STAMINA)
        {
        }

        public override void Exercise()
        {
            if (this.Stamina + INCREASE_STAMINA_VALUE > 100)
            {
                this.Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }

            this.Stamina += INCREASE_STAMINA_VALUE;
        }
    }
}