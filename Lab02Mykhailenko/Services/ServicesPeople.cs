using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Lab02Mykhailenko.Server
{
    class ServicePeople
    {
        private static ObservableCollection<Person> _people = new ObservableCollection<Person>()
        {
            new Person("A","B", new DateTime(2000,8,25))
        };

        #region Property
        public static ObservableCollection<Person> People
        {
            get { return _people; }
            set
            {
                if (_people == value) return;
                _people = value;
            }
        }
        #endregion
        public ServicePeople()
        {

        }

        public void addNewPerson(Person person)
        {
            bool alreadyExist = false;
            foreach (var per in _people)
            {
                if (per.Name == person.Name && per.Surname.Equals(person.Surname)
                    && per.Email.Equals(person.Email) && per.Birthday.Equals(person.Birthday))
                { alreadyExist = true; break; }
            }
            if (!alreadyExist)
            {
                _people.Add(person);
            }
        }
    }
}
