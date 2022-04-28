using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lab02Mykhailenko.Server
{
    class PeopleService
    {
        private static FileRepository _repository = new FileRepository();

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
            if (per != null)
            {
                return false;
            }

            await _repository.AddOrUpdateAsync(person);
            return true;
        }
    }
}
