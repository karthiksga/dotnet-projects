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
using Jounce.Core.Command;
using Jounce.Framework.Command;
using Jounce.Core.Event;
using System.ComponentModel.Composition;
//using MaFamille.CommonModel;
using MaFamille.Albums.Model;

namespace MaFamille.Albums.ViewModels
{
    [ExportAsViewModel("MyPhotoChild")]
    public class MyPhotoChildViewModel : BaseViewModel, IEventSink<PhotoModel>, IPartImportsSatisfiedNotification
    {
        public IActionCommand<object> PreviousClickCommand { get; set; }
        public IActionCommand<object> NextClickCommand { get; set; }
        
        private PhotoModel _selectedPhoto;
        public PhotoModel SelectedPhoto
        {
            get { return this._selectedPhoto; }
            set
            {
                this._selectedPhoto = value;
                RaisePropertyChanged(() => SelectedPhoto);
            }
        }

        //private byte[] _imageStream;
        //public byte[] ImageStream
        //{
        //    get { return this._imageStream; }
        //    set
        //    {
        //        this._imageStream = value;
        //        RaisePropertyChanged(() => ImageStream);
        //    }
        //}

        public MyPhotoChildViewModel(Action<PhotoModel> handlePreviousClick, Action<PhotoModel> handleNextClick)
        {                      
            PreviousClickCommand = new ActionCommand<object>(obj =>
            {
                if(handlePreviousClick!=null) handlePreviousClick(SelectedPhoto);
            });
            NextClickCommand = new ActionCommand<object>(obj =>
            {
                if (handleNextClick != null) handleNextClick(SelectedPhoto);
            });
        }

        public void HandleEvent(PhotoModel publishedEvent)
        {
            SelectedPhoto = publishedEvent;            
        }

        public void OnImportsSatisfied()
        {
            EventAggregator.SubscribeOnDispatcher(this);
        }
    }
}
