using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.ViewModels;
using System.Windows.Controls;

namespace Lab02Mykhailenko.Views
{
    public partial class PersonControl : UserControl
    {
        private PersonViewModel _personViewModel;

        public PersonControl()
        {
            InitializeComponent();
            DataContext = _personViewModel = new PersonViewModel(GoToAllPeople);
        }

        public void GoToAllPeople()
        {
            Content = new AllPeople();
        }
    }
}
