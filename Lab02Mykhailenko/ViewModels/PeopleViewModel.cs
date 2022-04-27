using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;

namespace Lab02Mykhailenko.ViewModels
{
    class PeopleViewModel 
    {
        #region Fields
        private ObservableCollection<Person> _people;
        private RelayCommand<object> _add;
        private RelayCommand<object> _edit;
        private RelayCommand<object> _delete;
        private Action _goToPersonView;
        #endregion

        #region Property
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set
            {
                if (_people == value) return;
                _people = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public PeopleViewModel(Action gotoPersonView)
        {
            People = ServicePeople.People;
            _goToPersonView = gotoPersonView;
        }
        #endregion
        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return _add ??= new RelayCommand<object>(_ => OpenAdditionWindow());
            }
        }
        public RelayCommand<object> EditPersonCommand
        {
            get
            {
                return _edit ??= new RelayCommand<object>(_ => OpenAdditionWindow());
            }
        }
        public RelayCommand<object> DeletePersonCommand
        {
            get
            {
                return _delete ??= new RelayCommand<object>(_ => OpenAdditionWindow());
            }
        }

        private void OpenAdditionWindow()
        {
            //open window for add new person
            _goToPersonView.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
