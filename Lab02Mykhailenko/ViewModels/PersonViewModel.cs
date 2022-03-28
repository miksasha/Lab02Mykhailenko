using Lab02Mykhailenko.Models;
using System;
using System.ComponentModel;
using System.Windows;

namespace Lab02Mykhailenko.ViewModels
{

    /*
     + Кнопка повинна бути неактивною, якщо хоча б одне поле не заповнене. (тімс)
     + очищати поля, якщо не правильна дата
     + Якщо перевірки пройшли успішно, вивести значення всіх 8-ми полів класу.
     + Обчислення повинні відбуватись асинхронно 

    Правила виконання роботи:

    + Потрібно по максимум приховувати компоненти. Public використовувати лише там, де це необхідно.
    + Потрібно використовувати асинхронність в всіх місцях де можуть бути потенційні затримки часу виконання
     */

    //Питання:
    //чи потрібно  CorrectDate, BirthdayIsToday додавати в Person.cs

    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = new Person(null, null, null);
        private RelayCommand<object> _selectDateCommand;
        #endregion

        #region Properties
        public string Name
        {
            get { return _person.Name; }
            set { _person.Name = value; }
        }

        public string Surname
        {
            get { return _person.Surname; }
            set { _person.Surname = value; }
        }

        public string Email
        {
            get { return _person.Email; }
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
            get { return Birthday.ToString("d"); }
        }

        public bool IsAdult
        {
            get { return _person.IsAdult; }
        }

        public string SunSign
        {
            get { return _person.SunSign; }
        }

        public string ChineseSign
        {
            get { return _person.ChineseSign; }
        }

        public bool IsBirthday
        {
            get { return _person.IsBirthday; }
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
                return _selectDateCommand ??= new RelayCommand<object>(_ => SetData());
            }
        }
        private void SetData()
        {
            if (!CorrectDate())
            {
                //NotifyPropertyChanged("Name");
                //NotifyPropertyChanged("Surname");
                //NotifyPropertyChanged("Email");
                //NotifyPropertyChanged("Birthday");
                MessageBox.Show("Ви ввели не правильну дату народження!");
            }
            else
            {
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Surname");
                NotifyPropertyChanged("Email");
                NotifyPropertyChanged("BirthdayToString");
                NotifyPropertyChanged("IsAdult");
                NotifyPropertyChanged("SunSign");
                NotifyPropertyChanged("ChineseSign");
                NotifyPropertyChanged("IsBirthday");
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

    }
}
