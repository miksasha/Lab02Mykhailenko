using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lab02Mykhailenko.Server
{
    class PeopleService
    {
        private static FileRepository _repository;
        //private static ObservableCollection<Person> _people = new ObservableCollection<Person> {
        //            new Person("Марія", "Воловська", "mar@gmail.com", new DateTime(1999, 1, 4)),
        //            new Person("Bob", "Gilbert", "bob@gmail.com"),
        //            new Person("Lili", "Miklson", "lil@gmail.com") };
        #region Property
        //public static ObservableCollection<Person> People
        //{
        //    get { return _people; }
        //    set
        //    {
        //        if (_people == value) return;
        //        _people = value;
        //    }
        //}
        #endregion

        public PeopleService()
        {
            _repository = new FileRepository();
        }

        public async Task<Person> AuthenticateAsync(Person person)
        {
            var per = await _repository.GetAsync(person.Email);
            if (per != null)
            {
                return new Person(per.Name, per.Surname, per.Email, per.Birthday);
            }
            return null;
        }

        public async Task<bool> AddNewPersonAsync(Person person)
        {
            var per = await _repository.GetAsync(person.Email);
            if (per == null)
            {
                return false;
            }

            await _repository.AddOrUpdateAsync(person);
            return true;
        }
        //    private static ObservableCollection<Person> _people = new ObservableCollection<Person> {  
        //            new Person("Марія", "Воловська", "mar@gmail.com", new DateTime(1999, 1, 4)),
        //            new Person("Bob", "Gilbert", "bob@gmail.com"),
        //            new Person("Lili", "Miklson", "lil@gmail.com") };

        //    #region Property
        //    public static ObservableCollection<Person> People
        //    {
        //        get { return _people; }
        //        set
        //        {
        //            if (_people == value) return;
        //            _people = value;
        //        }
        //    }
        //    #endregion

        //    public PeopleService()
        //    {

        //    }

        //public void AddNewPerson(Person person)
        //{
        //    bool alreadyExist = false;
        //    foreach (var per in _people)
        //    {
        //        if (per.Name.Equals(person.Name) && per.Surname.Equals(person.Surname)
        //            && per.Email.Equals(person.Email) && per.Birthday.Equals(person.Birthday))
        //        { alreadyExist = true; break; }
        //    }
        //    if (!alreadyExist)
        //    {
        //        _people.Add(person);
        //        _ = AddNewPersonAsync(person);
        //    }
        //}

        //public void DeleteNewPerson(Person person)
        //{
        //    foreach (var per in _people)
        //    {
        //        if (per.Equals(person))
        //        {
        //            People.Remove(per);
        //            break;
        //        }
        //    }
        //}
    }
}
