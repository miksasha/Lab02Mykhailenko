using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Server;
using Lab02Mykhailenko.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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

        public Person MyProperty { get; set; }
        #endregion

        #region Constructor
        public PeopleViewModel(Action gotoPersonView)
        {
            People = PeopleService.People;
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
                return _edit ??= new RelayCommand<object>(_ => OpenEditionWindow());
            }
        }

        public RelayCommand<object> DeletePersonCommand
        {
            get
            {
                return _delete ??= new RelayCommand<object>(_ => DeletePerson());
            }
        }

        private void OpenAdditionWindow()
        {
            //open window for add new person
            _goToPersonView.Invoke();
        }
        private void OpenEditionWindow()
        {
            
            MessageBox.Show("Вітаємо з Днем народження!\nБудьте щасливі!" + MyProperty.Name);
            //var personViewmodel = new PersonViewModel()
            //{
            //    Name = MyProperty.Name,
            //    Surname = MyProperty.Surname,
            //    Birthday = MyProperty.Birthday
            //};
        }
        private void DeletePerson()
        {
            //delete this person
            var service = new PeopleService();
            service.DeleteNewPerson(MyProperty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
