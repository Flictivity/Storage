﻿using MaterialDesignThemes.Wpf;
using StorageApp.DataBase;
using System;
using System.Collections;
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
using System.Windows.Threading;

namespace StorageApp.Pages
{
    /// <summary>
    /// Interaction logic for EmployeeTaskPage.xaml
    /// </summary>
    public partial class EmployeeTaskPage : Page
    {
        private Задачи _currentTask;

        public EmployeeTaskPage(Задачи task)
        {
            InitializeComponent();

            _currentTask = task;

            tbName.Text = task.Название;
            tbDifficult.Text = task.Сложность_задачи.ToString();
            tbIsFinished.Text = task.Состояние_работы ? "Выполнена" : "Не выполнена";
            tbStartDate.Text = task.Начало_выполнения.ToString();
            tbEndDate.Text = task.Окончание_выполнения.ToString();
            tbEstimation.Text = task.Оценка_выполнения.ToString();
            tbAdmin.Text = task.Работники?.ФИО_работника;
            tbEmployee.Text = task.Работники1?.ФИО_работника;
            tblTimer.Text = task.Время_выполнения.ToString();

            if (task.Работники1 != null)
            {
                btAccept.Visibility = Visibility.Collapsed;
            }

            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
            MySnackbar.MessageQueue = myMessageQueue;

            try
            {
                var startTime = new TimeSpan(0, _currentTask.Начало_выполнения.Value.Hour, _currentTask.Начало_выполнения.Value.Minute,
                _currentTask.Начало_выполнения.Value.Second);
                var endTime = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                var time = (TimeSpan)task.Время_выполнения;
                App.ExTime = time - endTime - startTime;
                StartTimer();
            }
            catch
            {
                myMessageQueue.Enqueue("Произошла ошибка при загрузке");
            }
        }

        private async void StartTimer()
        {
            App.Timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tblTimer.Text = App.ExTime.ToString("c");
                if (App.ExTime == TimeSpan.Zero) App.Timer.Stop();
                App.ExTime = App.ExTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            App.Timer.Start();
        }

        private void EventAccept(object sender, RoutedEventArgs e)
        {
            _currentTask.Работники1 = App.Context.Работники.FirstOrDefault(x => x.ID_пользователя == App.CurrentUser.ID_пользователя);
            _currentTask.Начало_выполнения = DateTime.Now;
            App.Context.SaveChanges();

            NavigationService.Navigate(new EmployeeTasksPage(false));
        }

        private void EventGoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
