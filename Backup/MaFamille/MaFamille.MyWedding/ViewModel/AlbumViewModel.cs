using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using Jounce.Core.ViewModel;
//using MaFamille.CommonModel;
using MaFamille.MyWedding.Model;
using Jounce.Framework.Command;
using System.ComponentModel.Composition;
using Jounce.Core.Event;
using Jounce.Core.View;
using Jounce.Core.Command;
using Jounce.Framework;
using System.Linq;
using Jounce.Core.Application;
using Jounce.Framework.View;
using System.Windows.Threading;
using MaFamille.MyWedding.Views;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using MaFamille.Common;
using System.Windows.Media;
using System.ServiceModel;
using MaFamille.MyWedding;

namespace MaFamille.MyWedding.ViewModel
{
    [ExportAsViewModel("AlbumViewModel")]
    public class AlbumViewModel : BaseViewModel
    {   
        public AlbumModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);                
            }
        }private AlbumModel _selectedItem;
        
        public ObservableCollection<AlbumModel> AlbumList
        {
            get
            {
                return _albumList;
            }
            set
            {
                _albumList = value;
                RaisePropertyChanged(() => AlbumList);                
            }
        }private ObservableCollection<AlbumModel> _albumList;

        public ObservableCollection<AlbumModel> TempAlbumList
        {
            get
            {
                return _tempAlbumList;
            }
            set
            {
                _tempAlbumList = value;
                RaisePropertyChanged(() => TempAlbumList);
            }
        }private ObservableCollection<AlbumModel> _tempAlbumList;

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                RaisePropertyChanged(() => IsLoaded);
            }
        }private bool _isLoaded;

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        public Action<Action> UIAction { get; set; }

        [Import]
        public VisualStateAggregator VsmAggregator { get; set; }

        private ModalDialogService modalDialogService;

        //public ICommand Click { get; set; }

        public AlbumViewModel()
        {
            if (TempAlbumList!=null && TempAlbumList.Count > 0)
                return;
            modalDialogService = new ModalDialogService();
            Deployment.Current.Dispatcher.BeginInvoke(() => LoadAlbums());
        }

        void LoadAlbums()
        {            
            MaFamille.MyWedding.MaFamilleService.ServiceClient client = new MaFamille.MyWedding.MaFamilleService.ServiceClient();
            client.GetWeddingAlbumsCompleted += delegate(object sender, MaFamille.MyWedding.MaFamilleService.GetWeddingAlbumsCompletedEventArgs e)
            {
                if (e.Error is FaultException<MaFamille.MyWedding.MaFamilleService.CustomException>)
                {
                    MessageBox.Show(e.Error.Message);
                    return;
                }
                AlbumList = new ObservableCollection<AlbumModel>();
                TempAlbumList = new ObservableCollection<AlbumModel>();
                if (e.Result != null)
                {
                    ObservableCollection<MaFamille.MyWedding.MaFamilleService.AlbumModel> albums = e.Result as ObservableCollection<MaFamille.MyWedding.MaFamilleService.AlbumModel>;
                    //Uri uri = new Uri("/MaFamille.Albums;component/Images/default.png", UriKind.Relative);
                    //Stream stream = System.Windows.Application.GetResourceStream(uri).Stream;
                    //byte[] defaultImageByte = new BinaryReader(stream).ReadBytes((int)stream.Length);

                    foreach (MaFamille.MyWedding.MaFamilleService.AlbumModel album in albums)
                    {
                        AlbumModel _album = new AlbumModel();
                        _album.AlbumName = album.AlbumName;                        
                        _album.AlbumImage = new ObservableCollection<AlbumImage>();
                        var albumImages = album.AlbumImage;
                        foreach (var albumImage in albumImages)
                        {
                            AlbumImage _albumImage = new AlbumImage();
                            _albumImage.ImageStream = albumImage.ImageStream;
                            _albumImage.Transform = albumImage.Transform;
                            _album.AlbumImage.Add(_albumImage);
                        }                        
                        _album.ClickCommand = new ActionCommand<object>(obj =>
                        {
                            EventAggregator.Publish("MyEngagement".AsViewNavigationArgs());
                            EventAggregator.Publish<AlbumModel>(_album);
                        });                        
                        _album.IsLoaded = true;
                        //AlbumList.Add(_album);
                        TempAlbumList.Add(_album);
                    }
                    //AddToTempAlbumList(AlbumList, 0);                    
                    IsLoaded = true;
                }
            };
            IsLoaded = false;
            client.GetWeddingAlbumsAsync();
        }

        void AddToTempAlbumList(ObservableCollection<AlbumModel> _albumList, int _index)
        {
            if (_index == _albumList.Count)
            {                
                return;
            }
            var item = _albumList[_index];
            TempAlbumList.Add(_albumList[_index]);

            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,0,100);
            timer.Tick += (s,e) =>
            {
                timer.Stop();
                AddToTempAlbumList(_albumList, _index + 1);
            };
            timer.Start();
        }        

        private byte[] CreateThumbnailImage(Stream stream, int width)
        {
            BitmapImage bi = new BitmapImage();
            bi.SetSource(stream);
            double cx = width;
            double cy = bi.PixelHeight * (cx / bi.PixelWidth);
            Image image = new Image();
            image.Source = bi;
            WriteableBitmap wb1 = new WriteableBitmap((int)cx, (int)cy);
            ScaleTransform transform = new ScaleTransform();
            transform.ScaleX = cx / bi.PixelWidth;
            transform.ScaleY = cy / bi.PixelHeight;
            wb1.Render(image, transform);
            wb1.Invalidate();
            WriteableBitmap wb2 = new WriteableBitmap((int)cx, (int)cy);
            for (int i = 0; i < wb2.Pixels.Length; i++)
                wb2.Pixels[i] = wb1.Pixels[i];
            wb2.Invalidate();

            int[] p = wb2.Pixels;
            int len = p.Length * 4;
            byte[] result = new byte[len]; // ARGB
            Buffer.BlockCopy(p, 0, result, 0, len);
            return result;
        }        

        protected override void ActivateView(string viewName, IDictionary<string, object> viewParameters)
        {
            base.ActivateView(viewName, viewParameters);
            EventAggregator.Publish<AlbumModel>(new AlbumModel { AlbumName = "" });            
        }        

        public byte[] ToByteArray(WriteableBitmap bmp)
        {
            // Init buffer
            int w = bmp.PixelWidth;
            int h = bmp.PixelHeight;
            int[] p = bmp.Pixels;
            int len = p.Length;
            byte[] result = new byte[4 * w * h];

            // Copy pixels to buffer
            for (int i = 0, j = 0; i < len; i++, j += 4)
            {
                int color = p[i];
                result[j + 0] = (byte)(color >> 24); // A
                result[j + 1] = (byte)(color >> 16); // R
                result[j + 2] = (byte)(color >> 8);  // G
                result[j + 3] = (byte)(color);       // B
            }

            return result;
        }

    }
}
