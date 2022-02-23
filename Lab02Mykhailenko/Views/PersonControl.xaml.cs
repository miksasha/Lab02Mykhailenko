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
    public partial class PersonControl : UserControl
    {
        private PersonViewModel _personViewModel;

        public PersonControl()
        {
            InitializeComponent();
            DataContext = _personViewModel = new PersonViewModel();
        }
    }
}
