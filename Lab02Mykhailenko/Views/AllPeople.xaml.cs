using Lab02Mykhailenko.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            DataContext = _peopleViewModel = new PeopleViewModel(GoToPersonControl);
        }

        public void GoToPersonControl()
        {
            Content = new PersonControl();
        }
    }
}
