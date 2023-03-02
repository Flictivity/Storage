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
    /// Interaction logic for EmployeeTasks.xaml
    /// </summary>
    public partial class EmployeeTasks : Page
    {
        public EmployeeTasks()
        {
            InitializeComponent();

            var tasks = App.Context.Задачи.ToList();
            var employeeId = App.Context.Работники
                .FirstOrDefault(x => x.ID_пользователя == App.CurrentUser.ID_пользователя)
                .ID_работника;
            lvInProgress.ItemsSource = tasks.Where(x => !x.Состояние_работы && x.ID_работника == employeeId).ToList();
            lvFinished.ItemsSource = tasks.Where(x => x.Состояние_работы && x.ID_работника == employeeId).ToList();
        }

        private void lvFinished_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowTaskInfo((Задачи) lvFinished.SelectedItem);
        }

        private void lvInProgress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowTaskInfo((Задачи)lvInProgress.SelectedItem);
        }

        private void ShowTaskInfo(Задачи task)
        {
            NavigationService.Navigate(new EmployeeTaskPage(task));
        }
    }
}
