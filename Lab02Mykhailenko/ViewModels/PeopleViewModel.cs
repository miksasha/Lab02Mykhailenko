using Lab02Mykhailenko.Server;
using Lab02Mykhailenko.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Lab02Mykhailenko.ViewModels
{
    class PeopleViewModel 
    {
        #region Fields
        private ObservableCollection<PersonViewModel> _people;
        private ObservableCollection<PersonViewModel> _selectedPeople;
        private PersonService _personService;
        private RelayCommand<object> _add;
        private RelayCommand<object> _edit;
        private RelayCommand<object> _delete;
        private RelayCommand<object> _fil;
        private Action _goToPersonView;
        private PersonViewModel _curentPerson;
        private string _wordToFind;
        private List<string> _allColumns;
        private string _wordForSearch;
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

        public ObservableCollection<PersonViewModel> SelectedPeople
        {
            get { return _selectedPeople; }
            set
            {
             //   if (_selectedPeople == value) return;
                _selectedPeople = value;
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
        public List<string> AllColumns
        {
            get
            {
                return _allColumns;
            }

        }
        public string WordToFind
        {
            get
            {
                return _wordToFind;
            }
            set {
                _wordToFind = value;
                NotifyPropertyChanged();
            }
        }

        public string WordForSearch
        {
            get
            {
                return _wordForSearch;
            }
            set
            {
                _wordForSearch = value;
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

            _selectedPeople = new ObservableCollection<PersonViewModel>();

            _allColumns = new List<string>();
            _allColumns.Add("Ім'я");
            _allColumns.Add("Прізвище");
            _allColumns.Add("Email");
            _allColumns.Add("Дата народження");
            _allColumns.Add("Чи дорослий?");
            _allColumns.Add("Західна астрологія");
            _allColumns.Add("Китайська астрологія");
            _allColumns.Add("Чи сьогодні народився?");
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

        public RelayCommand<object> FilterCommand
        {
            get
            {
                return _fil ??= new RelayCommand<object>(_ => OpenFilterWindow());
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

        private void OpenFilterWindow()
        {
            List<PersonViewModel> selectedPerson = new List<PersonViewModel>();
            bool goToDefault = false;
            switch (WordToFind)
            {
                case "Ім'я":
                    var s = from person in _people
                            where person.Name.Equals(WordForSearch)
                            select person;
                    selectedPerson = s.ToList();
                    break;
                case "Прізвище":
                    s = from person in _people
                        where person.Surname.Equals(WordForSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Email":
                    s = from person in _people
                        where person.Email.Equals(WordForSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Дата народження":
                    s = from person in _people
                        where person.BirthdayString.Equals(WordForSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Чи дорослий?":
                    s = from person in _people
                        where person.IsAdult.Equals(WordForSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Чи сьогодні народився?":
                    s = from person in _people
                        where person.IsBirthday.Equals(WordForSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Західна астрологія":
                    s = from person in _people
                        where person.SunSign.Equals(WordForSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Китайська астрологія":
                    s = from person in _people
                        where person.ChineseSign.Equals(WordForSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                default:
                    goToDefault = true;
                    MessageBox.Show("Оберіть колонку, в якій відбуватиметься фільтрація");
                    break;
            }
            SelectedPeople.Clear();
            if(selectedPerson.Count==0 && !goToDefault)
            {
                MessageBox.Show("Не знайдено співпадіння");
            }
            foreach (PersonViewModel p in selectedPerson)
            {
                SelectedPeople.Add(p);
            }
            NotifyPropertyChanged("SelectedPeople");
            
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
