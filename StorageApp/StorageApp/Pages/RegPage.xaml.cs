﻿using MaterialDesignThemes.Wpf;
using StorageApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            cbRoles.ItemsSource = App.Context.Роли.ToList();
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
                        ФИО_работника = tbSurname.Text + tbName.Text + tbPatronymic,
                        Специальность = "Кладовщик",
                        ID_пользователя = newUser.ID_пользователя
                    };

                    App.Context.Пользователи.Add(newUser);
                    App.Context.Работники.Add(newEmployee);

                    App.Context.SaveChanges();
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
    }
}
