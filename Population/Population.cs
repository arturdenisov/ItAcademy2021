using System;

namespace Population
{
    public static class Population
    {
        /// <summary>
        /// Calculates the count of years which the town need to see its population greater or equal to currentPopulation inhabitants.
        /// </summary>
        /// <param name="initialPopulation">The population at the beginning of a year.</param>
        /// <param name="percent">The percentage of growth per year.</param>
        /// <param name="visitors">The visitors (new inhabitants per year) who come to live in the town.</param>
        /// <param name="currentPopulation">The population at present.</param>
        /// <returns>The count of years which the town need to see its population greater or equal to currentPopulation inhabitants.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when initial population is less or equals 0
        /// - or -
        /// the count of visitors cannot be less 0
        /// - or -
        /// the current population is less or equals 0
        /// - or -
        /// the current population is less than initial population.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">Throw if the value of percents is less then 0% or more then 100%.</exception>
        public static int GetYears(int initialPopulation, double percent, int visitors, int currentPopulation)
        {
            CheckArgs_GetYears(initialPopulation, percent, visitors, currentPopulation);

            double initPop = initialPopulation;
            int years = 0;

            while (initPop < currentPopulation)
            {
                initPop = initPop + (initPop * percent / 100) + visitors;
                years++;
            }

            return years;
        }

        private static void CheckArgs_GetYears(int initialPopulation, double percent, int visitors, int currentPopulation)
        {
            if (initialPopulation <= 0)
            {
                throw new ArgumentException($"{nameof(initialPopulation)} is less or equals 0");
            }
            else if (visitors < 0)
            {
                throw new ArgumentException($"{nameof(visitors)} cannot be less 0");
            }
            else if ((currentPopulation <= 0) || (currentPopulation < initialPopulation))
            {
                throw new ArgumentException($"{nameof(currentPopulation)} is less or equals 0 or is less than initial population");
            }
            else if (percent < 0 || percent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percent));
            }
        }
    }
}
