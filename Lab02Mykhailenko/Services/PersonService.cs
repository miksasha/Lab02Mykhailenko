using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Repositories;
using System.Collections.Generic;

namespace Lab02Mykhailenko.Services
{
    class PersonService
    {
        private static FileRepository _repository = new FileRepository();

        public IEnumerable<Person> GetAllPeople()
        {
            var res = new List<Person>();
            foreach(var user in _repository.GetAll())
            {
                res.Add(user);
            }

            return res;
        }

        public async void Edit(Person person)
        {
            await _repository.AddOrUpdateAsync(person);
        }
        public async void Delete(Person person)
        {
            await _repository.DeleteAsync(person);
        }
    }
}
