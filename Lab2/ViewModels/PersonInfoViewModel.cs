using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Lab2.Models;

namespace Lab2.ViewModels
{
    /// <summary>
    /// VM that corresponds to window showing all the properties of the person
    /// </summary>
    internal class PersonInfoViewModel : ObservableObject
    {
        #region binding properties
        public Visibility Visibility
        {
            get => _visibility;
            private set => SetValue(ref _visibility, value);
        } 

        public string Name => _person == null ? throw new PersonNullException() : _person.Name;

        public string Surname => _person == null ? throw new PersonNullException() : _person.Surname;

        public string Email => _person == null ? throw new PersonNullException() : _person.Email;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth => _person?.DateOfBirth ?? throw new PersonNullException();

        public bool IsAdult => _person?.IsAdult ?? throw new PersonNullException();

        public bool IsBirthday => _person?.IsBirthday ?? throw new PersonNullException();

        public AstrologicalSign AstrologicalSign => _person?.AstrologicalSign ?? throw new PersonNullException();

        public ZodiacSign ZodiacSign => _person?.ZodiacSign ?? throw new PersonNullException();


        #endregion

        private Visibility _visibility = Visibility.Collapsed;
        private Person _person;

        /// <summary>
        /// Binds this vm's properties to the specified person object,
        /// and makes the corresponding view appear on the screen
        /// </summary>
        /// <param name="person"></param>
        internal void ShowPersonInfo(Person person)
        {
            _person = person ?? throw new NullReferenceException(nameof(person));
            Visibility = Visibility.Visible;
            AttachNotifyChangeEventsToPersonProperties(person);
            NotifyPersonDependentPropertiesChanged();
        }

        private void NotifyPersonDependentPropertiesChanged()
        {
            var personDependentProperties = new string[]
            {
                nameof(Name), nameof(Surname), nameof(Email), nameof(DateOfBirth),
                nameof(IsAdult), nameof(IsBirthday), nameof(AstrologicalSign), nameof(ZodiacSign)
            };
            foreach(string propName in personDependentProperties)
            {
                OnPropertyChanged(propName);
            }
        }

        /// <summary>
        /// wires up the notify changed events of this class'es properties
        /// to corresponding properties of Person obj,
        /// to reflect the changes in the model
        /// </summary>
        private void AttachNotifyChangeEventsToPersonProperties(Person person)
        {
            // not strictly nesessary for this lab
            person.PropertyChanged += Person_PropertyChanged;
        }

        private void Person_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //TODO: fire the OnPropChanged event for every property of this Vm when the corresponding person property event fires
        }
    }
}
