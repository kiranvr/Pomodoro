using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for WindowsNotifierBeta.xaml
    /// </summary>
    public partial class WindowsNotifierBeta : Window
    {
        bool voiceAlert = false;

        public WindowsNotifierBeta()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="buttonText">button will be hidden if supplied empty string(or left to default val)</param>
        /// <param name="checkboxText">checkbox will be hidden if supplied empty string(or left to default val)</param>
        ///
        public WindowsNotifierBeta(string message, string buttonText = "", string checkboxText = "", bool voiceAlert = true)
        {
            InitializeComponent();
            ShowNotification();


            this.txtMessage.Text = message;
            this.okayButton.Content = buttonText;
            this.chkBox.Content = checkboxText;

            if (checkboxText.Length.Equals(0))
            {
                chkBox.Visibility = Visibility.Hidden;
            }
            if (buttonText.Length.Equals(0))
            {
                okayButton.Visibility = Visibility.Hidden;
            }

            this.voiceAlert = voiceAlert;
        }



        public void ShowNotification()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth - 100;
                this.Top = corner.Y - this.ActualHeight;
            }));
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Activate();
        }

        private void Window_Activated(object sender, EventArgs e)
        {

            this.Topmost = true;
            PlaySoundAlert();
        }
        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        async void PlaySoundAlert()
        {

            await Task.Run(() => HeavyMethod());
        }
        internal void HeavyMethod()
        {

            SystemSounds.Hand.Play();
            Thread.Sleep(5000);
            SystemSounds.Hand.Play();
        }
    }
}

