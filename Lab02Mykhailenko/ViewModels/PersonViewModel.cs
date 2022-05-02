using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Server;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Lab02Mykhailenko.ViewModels
{

    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = new Person(null, null, null);
        private RelayCommand<object> _selectDateCommand;
        private Action _goToAllPeople;
        private bool _isEnabled = true;
        #endregion

        #region Constructors
        public PersonViewModel(Action gotoAllPeople)
        {
            _goToAllPeople = gotoAllPeople;
            Guid = Guid.NewGuid();
        }

        public PersonViewModel()
        {
            Guid = Guid.NewGuid();
        }
        #endregion

        #region Properties
        public string Name
        {
            get
            {
               return _person.Name; 
            }
            set { _person.Name = value; }
        }

        public string Surname
        {
            get
            {
                return _person.Surname; 
            }
            set { _person.Surname = value; }
        }

        public string Email
        {
            get
            {
                return _person.Email;
            }
            set 
            { 
                try
                {
                    _person.Email = value;
                }
                catch (EmailException ex)
                {
                    MessageBox.Show(ex.Message + $"\nНекоректне значення: {ex.Value}");
                }
            }
        }

        public DateTime Birthday
        {
            get
            { return _person.Birthday; }

            set
            {
                if (_person.Birthday != value)
                {
                    try
                    {
                        _person.Birthday = value;
                        Task.Run(async () => await setAsynchronousData());
                    }
                    catch (DateException ex)
                    {
                        MessageBox.Show(ex.Message + $"\nНекоректне значення: {ex.Value.ToString("d")}");
                    }

                  //  SetIsAdult();
                    NotifyPropertyChanged("IsAdult");
                    NotifyPropertyChanged("SunSign");
                    NotifyPropertyChanged("ChineseSign");
                    NotifyPropertyChanged("IsBirthday");
                }
            }
        }
        public string BirthdayString
        {
            get
            { return _person.Birthday.ToString("d"); }

            set
            {
              
            }
        }

        //private void SetIsAdult()
        //{
        //    DateTime today = DateTime.Today;
        //    int age = today.Year - Birthday.Year;
        //    if (today.Month - Birthday.Month < 0) --age;
        //    if (today.Month - Birthday.Month == 0 && today.Day - Birthday.Day < 0) --age;

        //    IsAdult = age > 18;
        //}

        public string BirthdayToString
        {
            get
            {
                if (CorrectDate()) { return Birthday.ToString("d"); }
                return " ";
            }
        }

  //      public bool IsAdult { get; set; }

        public string IsAdult
        {
            get
            {
                if (CorrectDate())
                {
                    if (_person.IsAdult)
                        return "Так";
                    return "Ні";
                }
                return " ";
            }
        }

        public string SunSign
        {
            get
            {
                if (CorrectDate()) { return _person.SunSign; }
                return " ";
            }
        }

        public string ChineseSign
        {
            get
            {
                if (CorrectDate()) { return _person.ChineseSign; }
                return " ";
            }
        }

        public string IsBirthday
        {
            get {
                if (CorrectDate())
                {
                    if (_person.IsBirthday)
                        return "Так";
                    return "Ні";
                }
                return " ";
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Check
        public bool CorrectDate()
        {
            if (Birthday > DateTime.Today) return false;

            DateTime today = DateTime.Today;
            int age = today.Year - Birthday.Year;
            if (today.Month - Birthday.Month < 0) --age;
            if (today.Month - Birthday.Month == 0)
                if (today.Day - Birthday.Day < 0) --age;
        

            if (age > 135) return false;
            return true;
        }

        public bool BirthdayIsToday()
        {
            if (Birthday.Day == DateTime.Today.Day && Birthday.Month == DateTime.Today.Month) return true;
            return false;
        }

        public bool CorrectEmail()
        {
            string email = _person.Email;
            int count = 0;
            for (int i = 0; i < email.Length; i++)
            {
                if(email[i]=='@')
                {
                    count++;
                    if(i== email.Length - 1)
                        return false;
                }
            }
            if (count == 1)
                return true;
            return false;
        }
        #endregion

        private async Task setAsynchronousData()
        {
            await Task.Run(() => CorrectDate());
            await Task.Run(() => BirthdayIsToday());
            await Task.Run(() => IsAdult);
            await Task.Run(() => SunSign);
            await Task.Run(() => ChineseSign);
            await Task.Run(() => IsBirthday);
        }

        public RelayCommand<object> SelectDateCommand
        {
            get
            {
                return _selectDateCommand ??= new RelayCommand<object>(_ => SetData() , CanExecute);
            }
        }

        public Guid Guid { get; set; }

        private async void SetData()
        {
            if (!CorrectDate())
            {
                try
                {
                    throw new DateException("Ви ввели не правильну дату народження!\nЛюдина може бути від 0 до 135 років!", Birthday);
                }
                catch (DateException ex)
                {
                    MessageBox.Show(ex.Message + $"\nНекоректне значення: {ex.Value.ToString("d")}");
                }
            }
            else
            {
                if (BirthdayIsToday())
                {
                    MessageBox.Show("Вітаємо з Днем народження!\nБудьте щасливі!");
                }

                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Surname");
                NotifyPropertyChanged("Email");
                NotifyPropertyChanged("BirthdayToString");
                NotifyPropertyChanged("IsAdult");
                NotifyPropertyChanged("SunSign");
                NotifyPropertyChanged("ChineseSign");
                NotifyPropertyChanged("IsBirthday");

                var peopleService = new PeopleService();
                bool newPerson = await peopleService.AddNewPersonAsync(this);
                if(!newPerson)
                {
                    MessageBox.Show("Людина з таким email вже існує");
                }
                //var service = new PeopleService();
                //service.AddNewPerson(_person);
                _goToAllPeople.Invoke();

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_person.Name) && !String.IsNullOrWhiteSpace(_person.Surname) && !String.IsNullOrWhiteSpace(_person.Email);
        }
    }
}
