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
            if(CorrectDate() && CorrectEmail())
            {
                var peopleService = new PeopleService();
                bool newPerson = await peopleService.AddNewPersonAsync(_curentPerson);
                MessageBox.Show("Зміни збережено");
            }
            else
            {
                if (!CorrectDate())
                {
                    MessageBox.Show("Зміни не буде збережено, якщо не буде вказано коректну дату");
                }
                if (!CorrectEmail())
                {
                    MessageBox.Show("Зміни не буде збережено, якщо не буде вказано коректний email");
                }
            }
        }
        private void DeletePerson()
        {
            _personService.Delete(MyProperty);
            People.Remove(MyProperty);
        }

        #region Check
        public bool CorrectDate()
        {
            if (_curentPerson.Birthday > DateTime.Today) return false;

            DateTime today = DateTime.Today;
            int age = today.Year - _curentPerson.Birthday.Year;
            if (today.Month - _curentPerson.Birthday.Month < 0) --age;
            if (today.Month - _curentPerson.Birthday.Month == 0)
                if (today.Day - _curentPerson.Birthday.Day < 0) --age;


            if (age > 135) return false;
            return true;
        }

        public bool CorrectEmail()
        {
            string email = _curentPerson.Email;
            int count = 0;
            for (int i = 0; i < email.Length; i++)
            {
                if (email[i] == '@')
                {
                    count++;
                    if (i == email.Length - 1)
                        return false;
                }
            }
            if (count == 1)
                return true;
            return false;
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
