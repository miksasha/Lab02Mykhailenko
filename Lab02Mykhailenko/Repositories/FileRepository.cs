using Lab02Mykhailenko.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab02Mykhailenko.Repositories
{
    class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mykhailenko", "People");

        public FileRepository()
        {
            if(!Directory.Exists(BaseFolder))
            {
                Directory.CreateDirectory(BaseFolder);
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

            return JsonSerializer.Deserialize<Person>(stringObj);
        }
    }
}
