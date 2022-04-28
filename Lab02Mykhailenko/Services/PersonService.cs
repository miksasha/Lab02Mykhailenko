using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Repositories;
using Lab02Mykhailenko.ViewModels;
using System.Collections.Generic;

namespace Lab02Mykhailenko.Services
{
    class PersonService
    {
        private static FileRepository _repository = new FileRepository();

        public IEnumerable<PersonViewModel> GetAllPeople()
        {
            var res = new List<PersonViewModel>();
            foreach(var user in _repository.GetAll())
            {
                res.Add(user);
            }

            return res;
        }

        public async void Edit(PersonViewModel person)
        {
            await _repository.AddOrUpdateAsync(person);
        }
        public async void Delete(PersonViewModel person)
        {
            await _repository.DeleteAsync(person);
        }
    }
}
