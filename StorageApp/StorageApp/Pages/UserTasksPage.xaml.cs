using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for UserTasksPage.xaml
    /// </summary>
    public partial class UserTasksPage : Page
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

        public UserTasksPage(bool showHints)
        {
            InitializeComponent();

            InfoFrame.Navigate(new TasksPage());

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
    }
}
