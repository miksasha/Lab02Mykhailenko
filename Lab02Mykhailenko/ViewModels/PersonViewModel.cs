using System.ComponentModel;
using System.Windows;

namespace Lab02Mykhailenko.ViewModels
{

    /*
     Кнопка повинна бути неактивною, якщо хоча б одне поле не заповнене.
    Після натискання кнопки, повинні бути виконані перевірки 3 та 5 з Лабораторної роботи 1.
    Якщо перевірки пройшли успішно, вивести значення всіх 8-ми полів класу.
    Обчислення повинні відбуватись асинхронно 

Правила виконання роботи:

Потрібно по максимум приховувати компоненти. Public використовувати лише там, де це необхідно.
Потрібно використовувати асинхронність в всіх місцях де можуть бути потенційні затримки часу виконання
     */
    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
      //  private Person _date = new Person();
        private RelayCommand<object> _selectDateCommand;
        #endregion

        #region Properties

        #endregion

        public RelayCommand<object> SelectDateCommand
        {
            get
            {
                return _selectDateCommand ??= new RelayCommand<object>(_ => SetData());
            }
        }
        private void SetData()
        {
            //if (!_date.CorrectDate())
            //{
            //    NotifyPropertyChanged("Age");
            //    NotifyPropertyChanged("WSign");
            //    NotifyPropertyChanged("CSign");
                MessageBox.Show("Ви ввели не правильну дату народження!");
            //}
            //else
            //{

            //    NotifyPropertyChanged("Age");
            //    NotifyPropertyChanged("WSign");
            //    NotifyPropertyChanged("CSign");
            //    if (_date.BirthdayIsToday())
            //    {
            //        MessageBox.Show("Вітаємо з Днем народження!\nБудьте щасливі!");
            //    }

            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

    }
}
