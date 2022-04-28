using Lab02Mykhailenko.Models;
using Lab02Mykhailenko.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab02Mykhailenko.Repositories
{
    class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mykhailenko", "People");
        //private static ObservableCollection<PersonViewModel> _people = new ObservableCollection<PersonViewModel> {
        //            new Person("Марія", "Воловська", "mar@gmail.com", new DateTime(1999, 1, 4)),
        //            new Person("Bob", "Gilbert", "bob@gmaom", new DateTime(1999, 1, 4)),
        //            new Person("Lili", "Miklson", "lil@gmail.com", new DateTime(1999, 1, 4)) };

        private static ObservableCollection<PersonViewModel> _people = new ObservableCollection<PersonViewModel>()
        {
           // new PersonViewModel(){Name = "Vitalik", Surname = "Mamontov"}
        };

        public FileRepository()
        {
            if (!Directory.Exists(BaseFolder))
            {
                Directory.CreateDirectory(BaseFolder);
            }

            foreach (PersonViewModel p in _people)
            {
                _ = AddOrUpdateAsync(p);
            }
        }

        public async Task AddOrUpdateAsync(PersonViewModel person)
        {
            var stringObj = JsonSerializer.Serialize(person);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, person.Guid.ToString()), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }


        public async Task DeleteAsync(PersonViewModel person)
        {
            string filePath = Path.Combine(BaseFolder, person.Guid.ToString());

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public async Task<Person> GetAsync(Guid guid)
        {
            string stringObj = null;

            string filePath = Path.Combine(BaseFolder, guid.ToString());

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sr = new StreamReader(filePath))
            {
                stringObj = await sr.ReadToEndAsync();
            }

            //повертаємо, якщо людина вже існує
            return JsonSerializer.Deserialize<Person>(stringObj);
        }

        public List<PersonViewModel> GetAll()
        {
            var res = new List<PersonViewModel>();

            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sr = new StreamReader(file))
                {
                    stringObj = sr.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<PersonViewModel>(stringObj));
            }
            return res;
        }
    }
}
