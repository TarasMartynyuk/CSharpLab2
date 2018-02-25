using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab2.Models;

namespace Lab2.ViewModels
{
    class MainWindowViewModel : ObservableObject
    {
        #region binding properties
        public string Name { get; set; }

        public string Surname  { get; set; }

        public string Email  { get; set; }

        public DateTime DateOfBirth  { get; set; } = DateTime.Now;

        public ICommand PersonDataSubmitCommand { get; }
        #endregion

        private readonly PersonInfoViewModel _personInfoVm;

        internal MainWindowViewModel(PersonInfoViewModel personInfoVm)
        {
            _personInfoVm = personInfoVm ?? throw new ArgumentNullException(nameof(personInfoVm));
            PersonDataSubmitCommand = new DelegateCommandAsync(CreateAndShowPersonFromInputedData, AllFieldsHaveBeenSet);
        }

        #region command delegates
        private bool AllFieldsHaveBeenSet(object o)
        {
            return ! (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) ||
                string.IsNullOrEmpty(Email)); // date time always has default value 
        }

        private async Task CreateAndShowPersonFromInputedData(object o)
        {

            Console.WriteLine(DateOfBirth);
            if(BirthDataUtils.IsValidBirthDate(DateOfBirth) == false)
            {
                MessageBox.Show("Enter a valid date!", "error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            if(BirthDataUtils.IsBirthday(DateOfBirth))
            {
                MessageBox.Show("Wow, it's your birthday! Go enjoy yourself.", "your only invite today",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            await Task.Factory.StartNew(() => {
                var person = new Person(Name, Surname, Email, DateOfBirth);
                _personInfoVm.ShowPersonInfo(person);
            });
        }
        #endregion
    }
}
