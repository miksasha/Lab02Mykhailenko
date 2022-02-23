using System;

namespace Lab02Mykhailenko.Models
{
    class Person
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;
        #endregion

        #region Properties
        public string Name 
        { 
            get { return _name; } 
            set { _name = value; } 
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime Birthday
        {
            get
            { return _birthday; }

            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                }
            }
        }
        #endregion

        #region Constructors
        //Всі чотири параметри
        public Person(string name, string surname, string email, DateTime birthday)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthday = birthday;
        }

        //Ім’я, прізвище, адреса електронної пошти.
        public Person(string name, string surname, string email)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthday = DateTime.Today;
        }

        //Ім’я, прізвище, дата народження
        public Person(string name, string surname, DateTime birthday)
        {
            _name = name;
            _surname = surname;
            _email = null;
            _birthday = birthday;
        }
        #endregion

        #region Read-only properties
        public bool IsAdult
        {
            get 
            {
                DateTime today = DateTime.Today;
                int age = today.Year - _birthday.Year;
                if (today.Month - _birthday.Month < 0) --age;
                if (today.Month - _birthday.Month == 0 && today.Day - _birthday.Day < 0) --age;

                if (age > 18) return true;
                return false; 
            }
        }

        public string SunSign
        {
            get
            {
                switch (_birthday.Month)
                {
                    case 1:
                        return _birthday.Day >= 21 ? "Водолій" : "Козоріг";
                    case 2:
                        return _birthday.Day >= 20 ? "Риби" : "Водолій";
                    case 3:
                        return _birthday.Day >= 21 ? "Овен" : "Риби";
                    case 4:
                        return _birthday.Day >= 21 ? "Телець" : "Овен";
                    case 5:
                        return _birthday.Day >= 22 ? "Близнята" : "Телець";
                    case 6:
                        return _birthday.Day >= 22 ? "Рак" : "Близнята";
                    case 7:
                        return _birthday.Day >= 23 ? "Лев" : "Рак";
                    case 8:
                        return _birthday.Day >= 22 ? "Діва" : "Лев";
                    case 9:
                        return _birthday.Day >= 24 ? "Терези" : "Діва";
                    case 10:
                        return _birthday.Day >= 24 ? "Скорпіон" : "Терези";
                    case 11:
                        return _birthday.Day >= 24 ? "Стрілець" : "Скорпіон";
                    case 12:
                        return _birthday.Day >= 23 ? "Козоріг" : "Стрілець";
                    default:
                        break;
                }
                return "Wrong date";
            }
        }

        public string ChineseSign
        {
            get
            {
                if ((_birthday.Year - 4) % 12 == 0) { return "Щур"; }
                if ((_birthday.Year - 5) % 12 == 0) { return "Бик"; }
                if ((_birthday.Year - 6) % 12 == 0) { return "Тигр"; }
                if ((_birthday.Year - 7) % 12 == 0) { return "Кролик"; }
                if ((_birthday.Year - 8) % 12 == 0) { return "Дракон"; }
                if ((_birthday.Year - 9) % 12 == 0) { return "Змія"; }
                if ((_birthday.Year - 10) % 12 == 0) { return "Кінь"; }
                if ((_birthday.Year - 11) % 12 == 0) { return "Коза"; }
                if ((_birthday.Year - 12) % 12 == 0) { return "Мавпа"; }
                if ((_birthday.Year - 1) % 12 == 0) { return "Півень"; }
                if ((_birthday.Year - 2) % 12 == 0) { return "Собака"; }
                if ((_birthday.Year - 3) % 12 == 0) { return "Свиня"; }
                return "It is impossible to count";
            }
        }

        public bool IsBirthday
        {
            get
            {
                if (_birthday.Day == DateTime.Today.Day && _birthday.Month == DateTime.Today.Month) return true;
                return false;
            }
        }
        #endregion
    }
}
