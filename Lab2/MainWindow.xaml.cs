using System.Windows;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var personInfoVm = new PersonInfoViewModel();
            PersonInfoGrid.DataContext = personInfoVm;
            DataContext = new MainWindowViewModel(personInfoVm);
        }
    }
}
