using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using S4.Models;




namespace S4.ViewModels
{
    public class StopWatchViewModel : INotifyPropertyChanged
    {
        private readonly StopWatch  _watch=new StopWatch();
        private readonly DispatcherTimer _timer=new DispatcherTimer();

        public StopWatchViewModel()
        {
            _timer.Interval=TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            _watch.Start();
            _timer.Start();

        }
        public double SecondsAngle
        {
            get { return _watch.ElapsedSeconds % 60 * 6; }
        }

        public double MinutesAngle
        {
            get { return _watch.ElapsedSeconds / 60 % 60 * 6; }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _watch.Tick();                          // 1. tell the Model: +1 second
            OnPropertyChanged(nameof(SecondsAngle)); // 2. tell the View: angles changed
            OnPropertyChanged(nameof(MinutesAngle));
        }

            public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    
}
