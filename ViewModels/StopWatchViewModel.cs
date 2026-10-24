using S4.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;




namespace S4.ViewModels
{
    public class StopWatchViewModel : INotifyPropertyChanged
    {
        private readonly StopWatch  _watch=new StopWatch();
        private readonly DispatcherTimer _timer=new DispatcherTimer();
        public RelayCommand StartCommand { get; }
        public RelayCommand StopCommand { get; }
        public RelayCommand ResetCommand { get; }


        public StopWatchViewModel()
        {
            _timer.Interval=TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            StartCommand = new RelayCommand(Start, () => !_watch.IsRunning);
            StopCommand = new RelayCommand(Stop, () => _watch.IsRunning);
            ResetCommand = new RelayCommand(Reset, () => !_watch.IsRunning && _watch.ElapsedSeconds > 0);
        }

        private void Start()
        {
            _watch.Start();
            _timer.Start();
            CommandManager.InvalidateRequerySuggested();  // refresh buttons
        }

        private void Stop()
        {
            _watch.Stop();
            _timer.Stop();
            CommandManager.InvalidateRequerySuggested();
        }

        private void Reset()
        {
            _watch.Reset();
            OnPropertyChanged(nameof(SecondsAngle));  // hands go back to 12
            OnPropertyChanged(nameof(MinutesAngle));
            CommandManager.InvalidateRequerySuggested();
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
            _watch.Tick();                          
            OnPropertyChanged(nameof(SecondsAngle));  
            OnPropertyChanged(nameof(MinutesAngle));
        }

            public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    
}
