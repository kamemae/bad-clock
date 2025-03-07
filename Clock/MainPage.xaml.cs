namespace Clock {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
            Thread startTimer = new Thread(Timer);
            startTimer.Start();
        }
        public void Timer() {
    
            while(true) {
                DateTime time = DateTime.Now;
                Dispatcher.Dispatch(() => {
                    clockhandHour.Rotation = 30 * (time.Hour % 12) + time.Minute * 0.5;
                    clockhandMinute.Rotation = 6 * time.Minute;
                    clockhandSecond.Rotation = 6 * time.Second;
                    timeLabel.Text = $"{time.Hour:00} : {time.Minute:00} : {time.Second:00}";
                });
                Thread.Sleep(1000);

            }
          
        }
    }
}