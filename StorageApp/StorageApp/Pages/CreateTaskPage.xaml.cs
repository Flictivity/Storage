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
    /// Interaction logic for CreateTaskPage.xaml
    /// </summary>
    public partial class CreateTaskPage : Page
    {
        public CreateTaskPage()
        {
            InitializeComponent();

            cbTaskComplexety.ItemsSource = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
        }

        private void EventCreate(object sender, RoutedEventArgs e)
        {
            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
            MySnackbar.MessageQueue = myMessageQueue;

            try
            {
                if (!string.IsNullOrEmpty(tbTaskName.Text)
                    && !string.IsNullOrEmpty(tpTaskTime.Text)
                    && cbTaskComplexety.SelectedItem != null)
                {
                    var time = new TimeSpan(tpTaskTime.SelectedTime.Value.Hour, tpTaskTime.SelectedTime.Value.Minute, 0);

                    var newTask = new Задачи()
                    {
                        Название = tbTaskName.Text,
                        Сложность_задачи = (int)cbTaskComplexety.SelectedItem,
                        Состояние_работы = false,
                        Время_выполнения = time
                    };

                    App.Context.Задачи.Add(newTask);

                    App.Context.SaveChanges();

                    NavigationService.GoBack();
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
