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
            tbAdmin.Text = task.Работники1.ФИО_работника;
            tbEmployee.Text = task.Работники.ФИО_работника;
            tbTimer.Text = task.Время_выполнения.ToString();

            task.Работники = null;

            if (task.Работники == null)
            {
                btAccept.Visibility = Visibility.Collapsed;
            }
        }

        private void EventAccept(object sender, RoutedEventArgs e)
        {
            _currentTask.Работники = App.Context.Работники.FirstOrDefault(x => x.ID_пользователя == App.CurrentUser.ID_пользователя);
            _currentTask.Начало_выполнения = DateTime.Now;
            App.Context.SaveChanges();

            NavigationService.GoBack();
        }

        private void EventGoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
