namespace Clock {
    public partial class MainPage : ContentPage {
        Thread timerWorker;
        private ManualResetEvent pauseEvent = new ManualResetEvent(true); 
        private bool isRunning = true;

        public MainPage() {
            InitializeComponent();
            DrawClock();
            timerWorker = new Thread(Timer);
            timerWorker.IsBackground = true;
            timerWorker.Start();
        }

        private void Timer() {
            while(isRunning) {
                pauseEvent.WaitOne();
                DateTime time = DateTime.Now;
                Dispatcher.Dispatch(() => {
                    clockhandHour.Rotation = 30 * (time.Hour % 12) + time.Minute * 0.6;
                    clockhandMinute.Rotation = 6 * time.Minute;
                    clockhandSecond.Rotation = 6 * time.Second + time.Millisecond * 0.006;
                    timeLabel.Text = $"{time.Hour:00} : {time.Minute:00} : {time.Second:00}";
                });
                Thread.Sleep(1);
            }
        }

        private void DrawClock() {
            int centerX = 250, centerY = 245, radius = 215;
            AbsoluteLayout clockLayout = Content.FindByName<AbsoluteLayout>("ClockLayout");
            for(int i = 1; i <= 12; i++) {
                double angle = (i * 30 - 90) * (Math.PI / 180);
                Label numberLabel = new Label {
                    Text = i.ToString(),
                    FontSize = 32,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Black,
                    TranslationX = centerX + (radius - 40) * Math.Cos(angle) - 15,
                    TranslationY = centerY + (radius - 40) * Math.Sin(angle) - 15
                };
                clockLayout.Children.Add(numberLabel); 
            }
        }

        private void PausePlay(object sender, EventArgs e) {
            if(pauseEvent.WaitOne(0)) {
                pauseEvent.Reset();
                pausePlay.Text = "▶";
                pausePlay.Rotation = 0;
            } else {
                pauseEvent.Set();
                pausePlay.Text = "=";
                pausePlay.FontAttributes = FontAttributes.Bold;
                pausePlay.Rotation = 90;
            }
        }
        protected override void OnDisappearing() {
            base.OnDisappearing();
            isRunning = false;
            pauseEvent.Set();
        }
    }
}