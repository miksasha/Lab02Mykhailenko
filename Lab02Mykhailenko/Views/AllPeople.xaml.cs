using Lab02Mykhailenko.ViewModels;
using System.Windows.Controls;

namespace Lab02Mykhailenko.Views
{
    /// <summary>
    /// Логика взаимодействия для AllPeople.xaml
    /// </summary>
    public partial class AllPeople : UserControl
    {
        private PeopleViewModel _peopleViewModel;
        public AllPeople()
        {
            InitializeComponent();
            DataContext = _peopleViewModel = new PeopleViewModel(GoToPersonControlAdd);
        }

        public void GoToPersonControlAdd()
        {
            Content = new PersonControl();
        }
    }
}
