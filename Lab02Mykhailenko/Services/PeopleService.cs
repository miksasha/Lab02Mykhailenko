﻿using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Lab02Mykhailenko.Server
{
    class PeopleService
    {
        private static ObservableCollection<Person> _people = new ObservableCollection<Person> {  
                new Person("Марія", "Воловська", "mar@gmail.com", new DateTime(1999, 1, 4)),
                new Person("Bob", "Gilbert", "bob@gmail.com"),
                new Person("Lili", "Miklson", "lil@gmail.com") };

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
        public PeopleService()
        {

        }

        public void AddNewPerson(Person person)
        {
            bool alreadyExist = false;
            foreach (var per in _people)
            {
                if (per.Name.Equals(person.Name) && per.Surname.Equals(person.Surname)
                    && per.Email.Equals(person.Email) && per.Birthday.Equals(person.Birthday))
                { alreadyExist = true; break; }
            }
            if (!alreadyExist)
            {
                _people.Add(person);
            }
        }

        public void DeleteNewPerson(Person person)
        {
            foreach (var per in _people)
            {
                if (per.Equals(person))
                {
                    People.Remove(per);
                    break;
                }
            }
        }
    }
}