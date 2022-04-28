using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02Mykhailenko.Services
{
    class PersonService
    {
        private static FileRepository _repository = new FileRepository();

        public List<Person> GetAllPeople()
        {
            var res = new List<Person>();
            foreach(var user in _repository.GetAll())
            {
                res.Add(user);
            }

            return res;
        }

        public async void Delete(Person person)
        {
            await _repository.DeleteAsync(person);
        }
    }
}
