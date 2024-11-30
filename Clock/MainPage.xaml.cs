using System.Timers;

namespace Clock
{
    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer timer;

        // цифры от 0 до 9
        private readonly bool[,,] digits = new bool[10, 7, 5]
        {
            // 0
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true }
            },
            // 1
            {
                { false, false, false, true, false },
                { false, false, true, true, false },
                { false, true, false, true, false },
                { false, false, false, true, false },
                { false, false, false, true, false },
                { false, false, false, true, false },
                { false, false, false, true, false }
            },
            // 2
            {
                { false, true, true, true, true },
                { true, false, false, false, true },
                { false, false, false, false, true },
                { false, false, false, true, false },
                { false, false, true, false, false },
                { false, true, false, false, false },
                { true, true, true, true, true }
            },
            // 3
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { false, false, false, false, true },
                { false, false, true, true, true },
                { false, false, false, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true }
            },
            // 4
            {
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true },
                { false, false, false, false, true },
                { false, false, false, false, true },
                { false, false, false, false, true }
            },
            // 5
            {
                { true, true, true, true, true },
                { true, false, false, false, false },
                { true, false, false, false, false },
                { true, true, true, true, true },
                { false, false, false, false, true },
                { false, false, false, false, true },
                { true, true, true, true, true }
            },
            // 6
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, false, false, false },
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true }
            },
            // 7
            {
                { true, true, true, true, true },
                { false, false, false, false, true },
                { false, false, false, false, true },
                { false, false, false, true, false },
                { false, false, true, false, false },
                { false, false, true, false, false },
                { false, false, true, false, false }
            },
            // 8
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true }
            },
            // 9
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true },
                { false, false, false, false, true },
                { false, false, false, false, true },
                { true, true, true, true, true }
            }
        };

        public MainPage()
        {
            InitializeComponent();
            CreateGrid();
            UpdateTime();
            StartTimer();
        }

        private void CreateGrid()
        {
            MainGrid.ColumnDefinitions.Clear();
            MainGrid.RowDefinitions.Clear();

            for (int i = 0; i < 7; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            }

            for (int i = 0; i < 41; i++) // 6 цифр * 5 колонок + 5 колонок для разделителей + 6 колонок для пробелов
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
            }

            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 41; col++)
                {
                    var box = new BoxView
                    {
                        Color = Colors.Transparent
                    };
                    Grid.SetRow(box, row);
                    Grid.SetColumn(box, col);
                    MainGrid.Children.Add(box);
                }
            }
        }

        private void StartTimer()
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            var now = DateTime.Now;
            var timeString = $"{now:HH:mm:ss}";

            MainThread.BeginInvokeOnMainThread(() =>
            {
                int position = 0;
                foreach (char c in timeString)
                {
                    if (c == ':')
                    {
                        DrawColon(position);
                        position += 2;
                    }
                    else
                    {
                        DrawDigit(int.Parse(c.ToString()), position);
                        position += 6;
                    }
                }
            });
        }
        private void DrawDigit(int digit, int position)
        {
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    var box = (BoxView)MainGrid.Children[row * 41 + col + position];
                    box.Color = digits[digit, row, col] ? Colors.Black : Colors.Transparent;
                }
            }
        }

        private void DrawColon(int position)
        {
            var box1 = (BoxView)MainGrid.Children[2 * 41 + position];
            var box2 = (BoxView)MainGrid.Children[4 * 41 + position];
            box1.Color = Colors.Black;
            box2.Color = Colors.Black;
        }
    }
}