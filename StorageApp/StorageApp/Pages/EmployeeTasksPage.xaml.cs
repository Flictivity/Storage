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
    /// Interaction logic for EmployeeTasksPage.xaml
    /// </summary>
    public partial class EmployeeTasksPage : Page
    {
        private List<HintWondow> hints = new List<HintWondow>()
        {
            new HintWondow("Здесь вы получите всю нужную информации о вашем аккаунте,так же вы можете обратиться в поддержку",
                new CornerRadius(0,30,30,30),
                80, 90),
            new HintWondow("Здесь отображается твой рейтинг, за выполнение каждой задачи твой рейтинг может расти",
                new CornerRadius(0,30,30,30),
                190, 240),
            new HintWondow("Здесь ты можешь увидеть все свои выполненные и не выполненные задачи",
                new CornerRadius(30,30,30,0),
                850, 60),
        };

        private int hintIndex = 0;


        public EmployeeTasksPage(bool showHints)
        {
            InitializeComponent();

            tbUserInfo.Text = App.Context.Работники
                .FirstOrDefault(x => x.ID_пользователя == App.CurrentUser.ID_пользователя)
                .ФИО_работника;

            lvAvailableTasks.Items.Clear();

            var employee = App.Context.Работники
                .FirstOrDefault(x => x.ID_пользователя == App.CurrentUser.ID_пользователя);
            var raiting = App.Context.Рейтинг
                .FirstOrDefault(x => x.ID_работника == employee.ID_работника);
            var tasks = App.Context.Задачи
                .Where(x => x.Сложность_задачи * 0.5 < (double)raiting.Значение && x.ID_работника == null)
                .ToList();

            lvAvailableTasks.ItemsSource = tasks;
            Rating.Text = raiting.Значение.ToString();

            InfoFrame.NavigationService.Navigate(new EmployeeTasks());

            if (showHints)
            {
                foreach (var hint in hints)
                {
                    hint.OnClosedEvent += ChangeIndex;
                }

                ShowHint();
            }
        }

        private void ShowHint()
        {
            hints[hintIndex].Show();
        }

        private void ChangeIndex()
        {
            hintIndex++;
            if (hintIndex < hints.Count) ShowHint();
        }

        private void EventShowAvailableTasks(object sender, RoutedEventArgs e)
        {
            InfoFrame.NavigationService.Navigate(new EmployeeTasks());
        }

        private void lvAvailableTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoFrame.NavigationService.Navigate(new EmployeeTaskPage((Задачи) lvAvailableTasks.SelectedItem));
        }
    }
}