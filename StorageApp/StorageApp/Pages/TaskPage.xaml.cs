using MaterialDesignThemes.Wpf;
using StorageApp.DataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StorageApp.Pages
{
    /// <summary>
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        private Задачи _task;
        private TimeSpan _exTime;
        private DispatcherTimer _timer;
        private bool _isRunning;

        public TaskPage(Задачи task)
        {
            InitializeComponent();

            _task = task;

            cbAdmin.ItemsSource = App.Context.Работники.Where(x => x.Пользователи.ID_роли == 2).ToList();
            cbEmployee.ItemsSource = App.Context.Работники.Where(x => x.Пользователи.ID_роли == 1).ToList();

            tbName.Text = _task.Название;
            tbDifficult.Text = _task.Сложность_задачи.ToString();
            chbIsFinished.IsChecked = _task.Состояние_работы;
            dpStartTime.SelectedDate = _task.Начало_выполнения;
            dpEndDate.SelectedDate = _task.Окончание_выполнения;
            tbEstimation.Text = _task.Оценка_выполнения.ToString();
            cbAdmin.SelectedItem = _task.Работники1;
            cbEmployee.SelectedItem = _task.Работники;
            tblTimer.Text = _task.Время_выполнения.ToString();

            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
            MySnackbar.MessageQueue = myMessageQueue;

            try
            {
                var startTime = new TimeSpan(_task.Начало_выполнения.Value.Day, _task.Начало_выполнения.Value.Hour, _task.Начало_выполнения.Value.Minute,
                _task.Начало_выполнения.Value.Second);
                var endTime = new TimeSpan(DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                var time = (TimeSpan)task.Время_выполнения;
                _exTime = time - (endTime - startTime);
                StartTimer();
            }
            catch
            {
                myMessageQueue.Enqueue("Произошла ошибка при загрузке");
            }
        }

        private async void StartTimer()
        {
            if (_isRunning) return;
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tblTimer.Text = _exTime.ToString("c");
                if (_exTime == TimeSpan.Zero) _timer.Stop();
                _exTime = _exTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            _isRunning = true;
        }

        private void Event_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPage());
        }

        private void Event_SaveChanges(object sender, RoutedEventArgs e)
        {
            _task.Название = tbName.Text;
            _task.Сложность_задачи = int.Parse(tbDifficult.Text);
            _task.Состояние_работы = (bool)chbIsFinished.IsChecked;
            if(_task.Состояние_работы)
            {
                _task.Окончание_выполнения = DateTime.Now;
            }
            _task.Оценка_выполнения = int.Parse(tbEstimation.Text);
            _task.Работники = (Работники)cbEmployee.SelectedItem;
        }

        private void Event_RemoveTask(object sender, RoutedEventArgs e)
        {
            App.Context.Задачи.Remove(_task);
            App.Context.SaveChanges();
        }
    }
}
