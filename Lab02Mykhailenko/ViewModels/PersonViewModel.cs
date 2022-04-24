using Lab02Mykhailenko.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab02Mykhailenko.ViewModels
{

    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = new Person(null, null, null);
        private RelayCommand<object> _selectDateCommand;
        private bool _isEnabled = true;
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
            set 
            { 
                _person.Email = value;
                if (!CorrectEmail() && !_person.Email.Equals(""))
                {
                    throw new EmailException("Incorrect email", value);
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
                    _person.Birthday = value;
                    IsEnabled = false;
                    Task.Run(async () => await setAsynchronouData());
                    IsEnabled = true;
                    if (!CorrectDate())
                    {
                        throw new DateException("Incorrect date", value);
                    }
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
            string email = Email;
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

        private async Task setAsynchronouData()
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

        private void SetData()
        {
            //CultureInfo cultureInfo = new CultureInfo(1);
            //Validate(Birthday, cultureInfo);

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

            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Surname");
            NotifyPropertyChanged("Email");
            NotifyPropertyChanged("BirthdayToString");
            NotifyPropertyChanged("IsAdult");
            NotifyPropertyChanged("SunSign");
            NotifyPropertyChanged("ChineseSign");
            NotifyPropertyChanged("IsBirthday");

     
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

        //public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        //{
        //    //try
        //    //{
        //    //   // DateTime date = Birthday;
        //    //    if (((string)value).Length > 0)
        //    //        Birthday = (DateTime)value;
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    return new ValidationResult(false, $"Illegal characters or {e.Message}");
        //    //}
        //    if (!CorrectEmail())
        //    {
        //        return new ValidationResult(true, "Email must contain @\n@ shouldn't be the last symbol");
        //    }
        //    if (!CorrectDate())
        //    {
        //        return new ValidationResult(false, "Date of birth cannot be in the future\nA person must be under 135 years of age");
        //    }
        //    return ValidationResult.ValidResult;
        //}

    }
}
