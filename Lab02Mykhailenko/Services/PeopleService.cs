using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.Repositories;
using Lab02Mykhailenko.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lab02Mykhailenko.Server
{
    class PeopleService
    {
        private static FileRepository _repository = new FileRepository();

        public async Task<bool> AddNewPersonAsync(PersonViewModel person)
        {
            await _repository.AddOrUpdateAsync(person);
            return true;
        }

    }
}
