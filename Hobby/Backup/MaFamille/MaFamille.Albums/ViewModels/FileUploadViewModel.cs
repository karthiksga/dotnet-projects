using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Jounce.Core.ViewModel;
using System.Windows.Threading;
using System.ComponentModel.Composition;
using Jounce.Core.View;
using System.Linq;
using MaFamille.Albums.Views;

namespace MaFamille.Albums.ViewModels
{
    [ExportAsViewModel("FileUpload")]
    public class FileUploadViewModel:BaseViewModel
    {
        private DispatcherTimer _timer;
        private TimeSpan _time;

        private int _count;
        public int Count 
        { 
            get 
            {
                return _count;
            } 
            set 
            {
                _count = value;
                RaisePropertyChanged(() => Count); 
            } 
        }

        public FileUploadViewModel()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            Count += 1;
        }
    }
}
