using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Pomodoro.Core
{
    public delegate void TimeElapsedHandler(TimeElaspedInfo info);
    public delegate void PomodoroCompletedHandler(object obj);
        
    public class PomoDoroCore//Todo addl functionality -like task management goes in here in this class.
    {
        const int POMODORO_DURATION_IN_MINUTES = 25;
        PomoTimer timer = new PomoTimer(PomoTimer.GetMinutesToSeconds(POMODORO_DURATION_IN_MINUTES));
        public event TimeElapsedHandler TimeElapsed;
        public event PomodoroCompletedHandler PomodoroCompleted;
        List<Task> tasks = new List<Task>();

        public List<Task> Tasks { get => tasks; set => tasks = value; }

        public void StartTimerTest()
        {
            timer.TimeElapsed += Timer_TimeElapsed;
            timer.Start();
        }

        private void Timer_TimeElapsed(TimeElaspedInfo info)
        {
            if (this.TimeElapsed != null)
            {
                this.TimeElapsed(info);
            }
            if (info.TimeRemainingInSeconds < 1)
            {
                {
                    timer.Stop();
                    this.PomodoroCompleted(info);
                }
            }

        }

        public void AddTask(string name, string description, int numberOfPomodoros)
        {
            Task task = new Task();
            //TODO autogenerate ID

            task.Name = name;
            task.Id = GetUniqueId();
            task.Description = description;
            int i = 0;
            while (i < numberOfPomodoros)
            {
                PomodoroUnit pomodoroUnit = new PomodoroUnit(String.Format("{0}_{1}",task.Id.ToString(),i.ToString()));
                task.PomodoroUnits.Add(pomodoroUnit);
                i++;
            }
            this.tasks.Add(task);

        }

        private string GetUniqueId()
        {
            Guid g = Guid.NewGuid();
            var val=g.ToString() +"[" +DateTime.Now.ToString("MMddyyyy|hh:mm:ss tt") +"]";
            return val;
        }

        public void RunAPomodoroOnTask(string taskId, string pomodoroUnitId)
        {
            //Choose the proper pomodoro unit using task Id and unitId
            //PomodoroUnit.Run
        }

    }

    public class Task
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PomodoroUnit> PomodoroUnits { get; set; } = new List<PomodoroUnit>();

    }

    public class PomodoroUnit
    {

        private readonly string id;
        public PomodoroUnit(string id)
        {
            this.id = id;
        }
        public RunStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Id => id;

        public void Run()
        {
            //Create a timer and start it, report and record status.
        }

    }


    public class PomoTimer
    {

        int durationInSeconds = 0;
        int timeElapsedInSecs = 0;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public event TimeElapsedHandler TimeElapsed;


        public PomoTimer(int durationInSeconds)
        {
            this.durationInSeconds = durationInSeconds;
        }

        public void Start()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.timeElapsedInSecs++;

            if (this.TimeElapsed != null)
            {
                TimeElaspedInfo info = new TimeElaspedInfo();
                info.TimeElapesedInSeconds = timeElapsedInSecs;
                info.TimeRemainingInSeconds = durationInSeconds - timeElapsedInSecs;
                info.TimeRemainingMinutes = PomoTimer.GetSecondsToMinutes(info.TimeRemainingInSeconds);
                info.Status = RunStatus.Running; //TODO tis to be accurae; needs fixing.
                this.TimeElapsed(info);
            }


        }

        public static int GetMinutesToSeconds(int minutes)
        {
            return minutes * 60;
        }
        public static string GetSecondsToMinutes(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"mm\:ss");
        }

        internal void Stop()
        {
            dispatcherTimer.Stop();
        }
    }


    public class TimeElaspedInfo
    {
        public int TimeElapesedInSeconds { get; set; }
        public int TimeRemainingInSeconds { get; set; }
        public string TimeRemainingMinutes { get; set; }

        public RunStatus Status { get; set; }
    }

    public enum RunStatus
    {
        Paused,
        Running,
        Stopped
    }
}
