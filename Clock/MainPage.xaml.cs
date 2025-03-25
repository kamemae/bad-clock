using Microsoft.Maui.Controls.Shapes;

namespace Clock {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
            DrawClock();
            Thread startTimer = new Thread(Timer);
            startTimer.Start();
        }
        private void Timer() {
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
        private void DrawClock() {
            int centerX = 250, centerY = 245, radius = 215;
            AbsoluteLayout clockLayout = Content.FindByName<AbsoluteLayout>("ClockLayout");
            for(int i = 1; i <= 12; i++) {
                double angle = (i * 30 - 90) * (Math.PI / 180);

                Line labelLine = new Line {
                    Stroke = Colors.Black,
                    StrokeThickness = 5,

                    X1 = centerX + (radius + 35) * Math.Cos(angle),
                    Y1 = centerY + (radius + 35) * Math.Sin(angle),

                    X2 = centerX + (radius - 1) * Math.Cos(angle),
                    Y2 = centerY + (radius - 1) * Math.Sin(angle)
                };
                clockLayout.Children.Add(labelLine); 

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
    }
}