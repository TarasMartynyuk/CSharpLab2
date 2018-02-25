using System;

namespace Lab2.Models
{
    internal class Person : ObservableObject
    {
        #region read & write binding properties
        internal string Name 
        {
            get => _name;
            private set => SetValue(ref _name, value);
        }

        internal string Surname 
        {
            get => _surname;
            private set => SetValue(ref _surname, value);
        }

        internal string Email
        {
            get => _email;
            private set => SetValue(ref _email, value);
        }

        internal DateTime DateOfBirth
        {
            get => _dateOfBirth;
            private set
            {
                SetValue(ref _dateOfBirth, value);
                NotifyBirthDateDependentPropertiesChanged();
            }
        }

        #endregion
        #region read only binding properties
        internal bool IsAdult => BirthDataUtils.CalculateAge(DateOfBirth) >= 18;

        internal bool IsBirthday => BirthDataUtils.IsBirthday(DateOfBirth);

        internal AstrologicalSign AstrologicalSign => GetAstrologicalSign(DateOfBirth);

        internal ZodiacSign ZodiacSign => GetZodiacSign(DateOfBirth);
        #endregion

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _dateOfBirth;

        #region ctors
        internal Person(string name, string surname, string email, DateTime dateOfBirth) : this(name, surname, email)
        {
            DateOfBirth = dateOfBirth;
            
        }

        internal Person(string name, string surname, string email) : this(name, surname)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        internal Person(string name, string surname, DateTime dateOfBirth) : this(name, surname)
        {
            DateOfBirth = dateOfBirth;
        }

        internal Person(string name, string surname)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
        }
        #endregion

        private static AstrologicalSign GetAstrologicalSign(DateTime birthDate)
        {
            const int astrologicalYearStartMonth = 3;

            int monthOrdinalFromMarch =  birthDate.DayOfYear / 31 - astrologicalYearStartMonth;

            if(monthOrdinalFromMarch < 0)
            {
                monthOrdinalFromMarch += 12;
            }
            return (AstrologicalSign) monthOrdinalFromMarch;
        }

        private static ZodiacSign GetZodiacSign(DateTime birthDate)
        {
            // the first year when the cycle would start after 0th yearAD
            const int firstCycleStartAd = 4;
            int inCycleYear = (birthDate.Year - firstCycleStartAd) % 12;
            return (ZodiacSign) inCycleYear;
        }

        private void NotifyBirthDateDependentPropertiesChanged()
        {
            OnPropertyChanged(nameof(IsBirthday));
            OnPropertyChanged(nameof(IsAdult));
            OnPropertyChanged(nameof(AstrologicalSign));
            OnPropertyChanged(nameof(ZodiacSign));
        }
    }
}
