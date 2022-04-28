using Lab02Mykhailenko.Server;
using Lab02Mykhailenko.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Lab02Mykhailenko.ViewModels
{
    class PeopleViewModel 
    {
        #region Fields
        private ObservableCollection<PersonViewModel> _people;
        private PersonService _personService;
        private RelayCommand<object> _add;
        private RelayCommand<object> _edit;
        private RelayCommand<object> _delete;
        private Action _goToPersonView;
        private PersonViewModel _curentPerson;
        private int countClick = 0;
        #endregion

        #region Property
        public ObservableCollection<PersonViewModel> People
        {
            get { return _people; }
            set
            {
                if (_people == value) return;
                _people = value;
                NotifyPropertyChanged();
            }
        }

        public PersonViewModel MyProperty { 
            get
            { return _curentPerson; }
            set
            {
                if (_curentPerson == value) return;
                _curentPerson = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public PeopleViewModel(Action gotoPersonView)
        {
            _personService = new PersonService();
            People = new ObservableCollection<PersonViewModel>(_personService.GetAllPeople());
            _goToPersonView = gotoPersonView;
        }
        #endregion

        #region RelayCommand
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
                return _edit ??= new RelayCommand<object>(_ => EditPersonAsync());
            }
        }

        public RelayCommand<object> DeletePersonCommand
        {
            get
            {
                return _delete ??= new RelayCommand<object>(_ => DeletePerson());
            }
        }
        #endregion
        private void OpenAdditionWindow()
        {
            //open window for add new person
            _goToPersonView.Invoke();
        }

        private async Task EditPersonAsync()
        {
            var peopleService = new PeopleService();
            bool newPerson = await peopleService.AddNewPersonAsync(_curentPerson);
            if (!newPerson)
            {
                _personService.Delete(MyProperty);
                await peopleService.AddNewPersonAsync(MyProperty);
                MessageBox.Show("Зміни збережено");
                NotifyPropertyChanged();
            }
            else
            {

            }
            //if (!newPerson)
            //{
            //    MessageBox.Show("Людина з таким email вже існує");
            //}
            //_personService.Delete(MyProperty);
            //People.Remove(MyProperty);
        }
        private void DeletePerson()
        {
            _personService.Delete(MyProperty);
            People.Remove(MyProperty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
