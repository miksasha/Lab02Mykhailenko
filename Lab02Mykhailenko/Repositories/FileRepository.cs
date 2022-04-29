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

        private static ObservableCollection<PersonViewModel> _people = new ObservableCollection<PersonViewModel>()
        {
            new PersonViewModel(){Name = "Vitalik", Surname = "Mamontov", Email = "vit@dff", Birthday = new DateTime(2000,09,1), Guid = new Guid("1aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sasha", Surname = "Mykhailenko", Email = "miksasha2003@gmail.com", Birthday = new DateTime(2003,03,10), Guid = new Guid("2aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Vania", Surname = "Vata", Email = "vata@gmail.com", Birthday = new DateTime(2001,09,1), Guid = new Guid("3aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Dasha", Surname = "Sunshin", Email = "sun@gmail.com", Birthday = new DateTime(2010,10,10), Guid = new Guid("4aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Masha", Surname = "Thank", Email = "thank@dff", Birthday = new DateTime(2005,11,9), Guid = new Guid("5aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Tania", Surname = "King", Email = "taniaking@gmail.com", Birthday = new DateTime(2006,02,2), Guid = new Guid("6aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Viktor", Surname = "Mikhas", Email = "viktor@dff", Birthday = new DateTime(2002,07,5), Guid = new Guid("7aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Maria", Surname = "Parants", Email = "mar@dff", Birthday = new DateTime(2001,04,4), Guid = new Guid("8aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sania", Surname = "Sweet", Email = "sania@gmail.com", Birthday = new DateTime(2008,03,20), Guid = new Guid("9aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Boy", Surname = "Swan", Email = "boy@edu.com", Birthday = new DateTime(1999,02,7), Guid = new Guid("10aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},

            new PersonViewModel(){Name = "Vlad", Surname = "Voronin", Email = "vlad@email.com", Birthday = new DateTime(2015,01,1), Guid = new Guid("11aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sasha", Surname = "Winner", Email = "miksasha@email.com", Birthday = new DateTime(2003,03,10), Guid = new Guid("12aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Niklause", Surname = "Miklson", Email = "nik@email.com", Birthday = new DateTime(2011,01,10), Guid = new Guid("13aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sarah", Surname = "Miklson", Email = "miklsonsarah@gmail.com", Birthday = new DateTime(2010,11,11), Guid = new Guid("14aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Elena", Surname = "Gilbert", Email = "elena_gilbert@gmail.com", Birthday = new DateTime(2014,11,15), Guid = new Guid("15aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Bob", Surname = "Mamontov", Email = "bob_mamontov@gmail.com", Birthday = new DateTime(2011,03,3), Guid = new Guid("16aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Demon", Surname = "Salvator", Email = "demon_salvator@gmail.com", Birthday = new DateTime(2012,07,2), Guid = new Guid("17aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Stefan", Surname = "Salvator", Email = "stefan_salvator@gmail.com", Birthday = new DateTime(2011,01,4), Guid = new Guid("18aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sarah", Surname = "Mamontov", Email = "vit@gmail.com", Birthday = new DateTime(2008,03,20), Guid = new Guid("19aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Galia", Surname = "Voronina", Email = "galia@gmail.com", Birthday = new DateTime(1993,02,7), Guid = new Guid("20aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},

            new PersonViewModel(){Name = "Jack", Surname = "Movy", Email = "mov@dff", Birthday = new DateTime(2000,09,1), Guid = new Guid("21aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Seuze", Surname = "Firy", Email = "fir@dff", Birthday = new DateTime(2000,09,1), Guid = new Guid("22aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Demon", Surname = "Fishing", Email = "fish@dff", Birthday = new DateTime(2001,09,1), Guid = new Guid("23aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Fortune", Surname = "Swany", Email = "forty@dff", Birthday = new DateTime(2010,10,10), Guid = new Guid("24aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Dorman", Surname = "Slowly", Email = "vit@dff", Birthday = new DateTime(2005,11,9), Guid = new Guid("25aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Rarity", Surname = "Beauty", Email = "rarity@dff", Birthday = new DateTime(2006,02,2), Guid = new Guid("26aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Pinki", Surname = "Pay", Email = "pinki_pay@dff", Birthday = new DateTime(2002,07,5), Guid = new Guid("27aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Rainbow", Surname = "Dash", Email = "rain@dff", Birthday = new DateTime(2001,04,4), Guid = new Guid("28aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Jack", Surname = "Apple", Email = "jack_apple@dff", Birthday = new DateTime(2008,03,20), Guid = new Guid("29aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Flatter", Surname = "Shy", Email = "flatter_shy@dff", Birthday = new DateTime(2008,03,20), Guid = new Guid("30aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},


            new PersonViewModel(){Name = "Vova", Surname = "Samoylov", Email = "samoylov@dff", Birthday = new DateTime(1982,09,1), Guid = new Guid("31aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Molly", Surname = "Porcha", Email = "mol_por@dff", Birthday = new DateTime(2010,09,1), Guid = new Guid("32aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Polly", Surname = "Rich", Email = "rich@dff", Birthday = new DateTime(2011,03,1), Guid = new Guid("33aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Luna", Surname = "Night", Email = "night@dff", Birthday = new DateTime(2020,10,10), Guid = new Guid("34aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Selestia", Surname = "Day", Email = "day@dff", Birthday = new DateTime(2005,10,9), Guid = new Guid("35aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Ponny", Surname = "Voyky", Email = "voyky@dff", Birthday = new DateTime(2006,12,2), Guid = new Guid("36aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Vally", Surname = "Mikky", Email = "mik@dff", Birthday = new DateTime(2002,11,5), Guid = new Guid("37aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Conty", Surname = "Mamo", Email = "conty_mamo@dff", Birthday = new DateTime(1980,06,4), Guid = new Guid("38aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Barry", Surname = "Strawberry", Email = "barry_strawberry@dff", Birthday = new DateTime(1980,02,20), Guid = new Guid("39aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Hot", Surname = "Warm", Email = "hot_warm@dff", Birthday = new DateTime(1999,08,26), Guid = new Guid("40aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},

            new PersonViewModel(){Name = "Contick", Surname = "Taddy", Email = "tad@dff", Birthday = new DateTime(1992,09,20), Guid = new Guid("41aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Bola", Surname = "Vorov", Email = "vorov@dff", Birthday = new DateTime(2017,09,19), Guid = new Guid("42aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sora", Surname = "Candy", Email = "candy@dff", Birthday = new DateTime(2001,03,17), Guid = new Guid("43aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sister", Surname = "Konty", Email = "kontik@dff", Birthday = new DateTime(2020,10,10), Guid = new Guid("44aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Brothr", Surname = "Korup", Email = "grow@dff", Birthday = new DateTime(2005,10,11), Guid = new Guid("45aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Bob", Surname = "King", Email = "kingy_pingvy@dff", Birthday = new DateTime(2006,12,14), Guid = new Guid("46aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Galy", Surname = "Holly", Email = "hol@dff", Birthday = new DateTime(2002,11,12), Guid = new Guid("47aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Masha", Surname = "Beautiful", Email = "beau@dff", Birthday = new DateTime(1980,06,4), Guid = new Guid("48aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sasha", Surname = "Smolly", Email = "small@dff", Birthday = new DateTime(1970,02,20), Guid = new Guid("49aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Dasha", Surname = "Wonderful", Email = "wonder@dff", Birthday = new DateTime(1979,08,26), Guid = new Guid("50aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")}
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
