using MaterialDesignThemes.Wpf;
using StorageApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StorageApp.Pages
{
    /// <summary>
    /// Interaction logic for RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
            try
            {
                cbRoles.ItemsSource = App.Context.Роли.ToList();
            }
            catch { }
        }

        private void EventRegister(object sender, RoutedEventArgs e)
        {
            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
            MySnackbar.MessageQueue = myMessageQueue;

            try
            {
                if (!string.IsNullOrEmpty(tbName.Text) &&
                    !string.IsNullOrEmpty(tbSurname.Text) &&
                    !string.IsNullOrEmpty(tbPatronymic.Text) &&
                    !string.IsNullOrEmpty(tbEmail.Text) &&
                    !string.IsNullOrEmpty(tbPassword.Text) &&
                    cbRoles.SelectedItem != null)
                {
                    if (!CheckPassword(tbPassword.Text))
                    {
                        popupPassword.IsOpen = true;
                    }

                    if (!cbAgree.IsChecked.Value || !cbNotRobot.IsChecked.Value)
                    {
                        myMessageQueue.Enqueue("Примите соглашения и подтвердите, что вы не робот!");
                        return;
                    }

                    var user = App.Context.Пользователи.FirstOrDefault(x => x.Почта == tbEmail.Text);

                    if (user != null)
                    {
                        myMessageQueue.Enqueue("Пользователь с указанной почтой уже зарегистрирован!");
                        return;
                    }

                    Пользователи newUser = new Пользователи()
                    {
                        Почта = tbEmail.Text,
                        Пароль = tbPassword.Text,
                        Роли = (Роли)cbRoles.SelectedItem,
                    };

                    Работники newEmployee = new Работники()
                    {
                        ФИО_работника = $"{tbSurname.Text} {tbName.Text} {tbPatronymic}",
                        Специальность = "Кладовщик",
                        ID_пользователя = newUser.ID_пользователя
                    };

                    App.Context.Пользователи.Add(newUser);
                    App.Context.Работники.Add(newEmployee);
                    App.Context.Рейтинг.Add(new Рейтинг { Работники = newEmployee, Значение = (decimal)1.00 });

                    App.Context.SaveChanges();

                    App.CurrentUser = newUser;

                    if (newUser.ID_роли == 1)
                    {
                        NavigationService.Navigate(new EmployeeTasksPage(true));
                    }
                    else
                    {
                        NavigationService.Navigate(new AdminTasksPage(true));
                    }
                }
                else
                {
                    myMessageQueue.Enqueue("Заполните все поля!");
                }
            }
            catch
            {
                myMessageQueue.Enqueue("Произошла ошибка!");
            }
        }

        private bool CheckPassword(string password)
        {
            if (password.Length <= 8)
            {
                return false;
            }

            bool res = password.GroupBy(x => x).Any(x => x.Count() > 1);

            if (res)
            {
                return false;
            }

            if (!Regex.IsMatch(password, @"[A-Z]") 
                || !Regex.IsMatch(password, @"[a-z]"))
            {
                return false;
            }
                
            return true;
        }
    }
}
