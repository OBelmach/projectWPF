using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Lesson_13_2
{
    public partial class ActionLog : Window
    {
        public ActionLog()
        {
            InitializeComponent();
            string msg = File.ReadAllText("actionLog.json");
            string[] actionLogs = msg.Split('#');
            foreach (string s in actionLogs)
            {
                outputActionLog.Text += $"{s}\n";
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
