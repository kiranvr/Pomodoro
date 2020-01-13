using Pomodoro.Core;
using System;
using System.Collections.Generic;
using System.IO;
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
 
 
namespace Pomodoro
{
   

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PomoDoroCore pomodoro = new PomoDoroCore();
            pomodoro.TimeElapsed += Pomodoro_OnTick;
            pomodoro.PomodoroCompleted += Pomodoro_PomodoroCompleted;
            pomodoro.StartTimerTest();
            //----------

            PomoDoroCore pomoDoroCore = new PomoDoroCore();
            pomoDoroCore.AddTask("first task", "test..1", 3);
            pomoDoroCore.AddTask("second task", "test.2.", 4);

            var firstTask = pomoDoroCore.Tasks.FirstOrDefault(x => x.Name == "first task");
            var firstTaskId_firstPomoUnit = firstTask.PomodoroUnits[0];
            pomoDoroCore.RunAPomodoroOnTask(firstTask.Id, firstTaskId_firstPomoUnit.Id);




          
        }

        private void Pomodoro_PomodoroCompleted(object obj)
        {
            this.ShowNotification();
        }

        private void Pomodoro_OnTick(TimeElaspedInfo info)
        {
            lblSeconds.Content = string.Format("passed:{0} secs, remaining secs: {1} or  {2}",
                                                info.TimeElapesedInSeconds,
                                                info.TimeRemainingInSeconds,
                                                info.TimeRemainingMinutes
                                                );
        }


        void ShowNotification()
        {
            WindowsNotifierBeta notifierWindow = new WindowsNotifierBeta("Some Text","OK", "Do Not Show Again");
            notifierWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowNotification();
        }
    }

}
