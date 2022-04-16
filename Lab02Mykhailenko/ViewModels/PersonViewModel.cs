using Lab02Mykhailenko.Models;
using System;
using System.ComponentModel;
using System.Windows;

namespace Lab02Mykhailenko.ViewModels
{

    /*
     + Обчислення повинні відбуватись асинхронно 
    + Потрібно використовувати асинхронність в всіх місцях де можуть бути потенційні затримки часу виконання
     */


    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = new Person(null, null, null);
        private RelayCommand<object> _selectDateCommand;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                if (CorrectDate()) { return _person.Name; }
                return " ";
            }
            set { _person.Name = value; }
        }

        public string Surname
        {
            get
            {
                if (CorrectDate()) { return _person.Surname; }
                return " ";
            }
            set { _person.Surname = value; }
        }

        public string Email
        {
            get
            {
                if (CorrectDate()) { return _person.Email; }
                return " ";
            }
            set { _person.Email = value; }
        }

        public DateTime Birthday
        {
            get
            { return _person.Birthday; }

            set
            {
                if (_person.Birthday != value)
                {
                    _person.Birthday = value;
                }
            }
        }

        public string BirthdayToString
        {
            get
            {
                if (CorrectDate()) { return Birthday.ToString("d"); }
                return " ";
            }
        }

        public string IsAdultBool
        {
            get {
                if (CorrectDate())
                {
                    if (_person.IsAdult)
                    return "Так";
                return "Ні";
                }
                return " ";
            }

        }

        public string IsAdult
        {
            get
            {
                if (CorrectDate()) { return IsAdultBool; }
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
        #endregion

        public RelayCommand<object> SelectDateCommand
        {
            get
            {
                return _selectDateCommand ??= new RelayCommand<object>(_ => SetData() , CanExecute);
            }
        }

        private async void SetData()
        {

            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Surname");
            NotifyPropertyChanged("Email");
            NotifyPropertyChanged("BirthdayToString");
            NotifyPropertyChanged("IsAdult");
            NotifyPropertyChanged("SunSign");
            NotifyPropertyChanged("ChineseSign");
            NotifyPropertyChanged("IsBirthday");
            if (!CorrectDate())
            {
                MessageBox.Show("Ви ввели не правильну дату народження!");
            }
            else
            {
                if (BirthdayIsToday())
                {
                    MessageBox.Show("Вітаємо з Днем народження!\nБудьте щасливі!");
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_person.Name) && !String.IsNullOrWhiteSpace(_person.Surname) && !String.IsNullOrWhiteSpace(_person.Email);
        }

    }
}
