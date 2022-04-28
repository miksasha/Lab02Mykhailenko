using Lab02Mykhailenko.Models;
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
        private static readonly string BaseFolder = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mykhailenko", "People");
        private static ObservableCollection<Person> _people = new ObservableCollection<Person> {
                    new Person("Марія", "Воловська", "mar@gmail.com", new DateTime(1999, 1, 4)),
                    new Person("Bob", "Gilbert", "bob@gmaom", new DateTime(1999, 1, 4)),
                    new Person("Lili", "Miklson", "lil@gmail.com", new DateTime(1999, 1, 4)) };

        public FileRepository()
        {
            if(!Directory.Exists(BaseFolder))
            {
                Directory.CreateDirectory(BaseFolder);
            }

            foreach(Person p in _people)
            {
                _ = AddOrUpdateAsync(p);
            }
        }

        public async Task AddOrUpdateAsync(Person person)
        {
            var stringObj = JsonSerializer.Serialize(person);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, person.Email), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public async Task DeleteAsync(Person person)
        {
            string filePath = Path.Combine(BaseFolder, person.Email);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public async Task<Person> GetAsync(string email)
        {
            string stringObj = null;

            string filePath = Path.Combine(BaseFolder, email);

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sr= new StreamReader(filePath))
            {
                stringObj = await sr.ReadToEndAsync();
            }

            //повертаємо, якщо людина вже існує
            return JsonSerializer.Deserialize<Person>(stringObj);
        }

        public List<Person> GetAll()
        {
            var res = new List<Person>();

            foreach(var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sr = new StreamReader(file))
                {
                    stringObj = sr.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }
            return res;
        }
    }
}
