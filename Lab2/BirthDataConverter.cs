using System;

namespace Lab2
{
    static class BirthDataUtils
    {
        public static bool IsBirthday(DateTime birthDate)
        {
            if(IsValidBirthDate(birthDate) == false)
            {
                throw new ArgumentException("not valid birthDate");
            }
            return DateTime.Today.DayOfYear == birthDate.DayOfYear;
        }

        public static int CalculateAge(DateTime birthDate)
        {
            return DateTime.Today.Year - birthDate.Year;
        }

        public static bool IsValidBirthDate(DateTime birthDate)
        {
            return IsValidAge(CalculateAge(birthDate));
        }

        private static bool IsValidAge(int age)
        {
            return age >= 0 && age <= 130;
        }
    }
}
