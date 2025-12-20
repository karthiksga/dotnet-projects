using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using Jounce.Core.ViewModel;
//using MaFamille.CommonModel;
using MaFamille.Albums.Model;
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
using MaFamille.Albums.Views;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using MaFamille.Common;
using System.Windows.Media;
using System.ServiceModel;

namespace MaFamille.Albums.ViewModels
{
    [ExportAsViewModel("MyAlbum")]
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
            modalDialogService = new ModalDialogService();
            Deployment.Current.Dispatcher.BeginInvoke(() => LoadAlbums());
        }

        void LoadAlbums()
        {
            #region
            //AlbumList = new ObservableCollection<AlbumModel>();
            //AlbumList.Add(new AlbumModel { AlbumName = "Album1" });
            //AlbumList.Add(new AlbumModel { AlbumName = "Album2"});
            //AlbumList.Add(new AlbumModel { AlbumName = "Album3"});
            //AlbumList.Add(new AlbumModel { AlbumName = "Album4"});
            //AlbumList.Add(new AlbumModel { AlbumName = "New Album"});

            //AlbumList.Add(new AlbumModel { AlbumName = "Album1", Click = new DelegateCommand<object>(obj => { this.SelectedItem = obj as AlbumModel; GetPhoto(); }) });
            //AlbumList.Add(new AlbumModel { AlbumName = "Album2", Click = new DelegateCommand<object>(obj => { this.SelectedItem = obj as AlbumModel; GetPhoto(); }) });
            //AlbumList.Add(new AlbumModel { AlbumName = "Album3", Click = new DelegateCommand<object>(obj => { this.SelectedItem = obj as AlbumModel; GetPhoto(); }) });
            //AlbumList.Add(new AlbumModel { AlbumName = "Album4", Click = new DelegateCommand<object>(obj => { this.SelectedItem = obj as AlbumModel; GetPhoto(); }) });
            //AlbumList.Add(new AlbumModel { AlbumName = "New Album", Click = new DelegateCommand<object>(obj => { this.SelectedItem = obj as AlbumModel; GetPhoto(); }) });

            //MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            //client.GetPhotosCompleted += delegate(object sender, MaFamilleService.GetPhotosCompletedEventArgs e)
            //{
            //    PictureList = new ObservableCollection<AlbumModel>();
            //    if (e.Result != null)
            //    {
            //        ObservableCollection<MaFamilleService.AlbumModel> lstPics = e.Result as ObservableCollection<MaFamilleService.AlbumModel>;
            //        foreach (MaFamilleService.AlbumModel pic in lstPics)
            //        {
            //            PictureList.Add(new AlbumModel
            //            {
            //                ImageStream=pic.ImageStream,
            //                Click=new DelegateCommand<object>(obj=>{
            //                    if (obj != null)
            //                        SelectedItem = obj as AlbumModel;
            //                })
            //            });
            //        }
            //    }

            //};
            //client.GetPhotosAsync();
            #endregion 'Commented
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            client.GetAlbumsCompleted += delegate(object sender, MaFamilleService.GetAlbumsCompletedEventArgs e)
            {
                if (e.Error is FaultException<MaFamilleService.CustomException>)
                {
                    MessageBox.Show(e.Error.Message);
                    return;
                }
                AlbumList = new ObservableCollection<AlbumModel>();                
                if (e.Result != null)
                {
                    ObservableCollection<MaFamilleService.AlbumModel> albums = e.Result as ObservableCollection<MaFamilleService.AlbumModel>;

                    Uri uri = new Uri("/MaFamille.Albums;component/Images/default.png", UriKind.Relative);
                    Stream stream = System.Windows.Application.GetResourceStream(uri).Stream;
                    byte[] defaultImageByte = new BinaryReader(stream).ReadBytes((int)stream.Length);

                    foreach (MaFamilleService.AlbumModel album in albums)
                    {
                        AlbumModel _album = new AlbumModel();
                        _album.AlbumName = album.AlbumName;
                        _album.OldAlbumName = album.AlbumName;
                        _album.AlbumImage = new ObservableCollection<AlbumImage>();
                        var albumImages = album.AlbumImage;
                        foreach (var albumImage in albumImages)
                        {
                            AlbumImage _albumImage = new AlbumImage();
                            _albumImage.ImageStream = albumImage.ImageStream;
                            _albumImage.Transform = albumImage.Transform;
                            _album.AlbumImage.Add(_albumImage);
                        }
                        //if (albumImages.Count < 3)
                        //{
                        //    int j = albumImages.Count;
                        //    for (int i = 0; i < 3 - j; i++)
                        //    {
                        //        AlbumImage _albumImage = new AlbumImage();
                        //        _albumImage.ImageStream = defaultImageByte;
                        //        _albumImage.Transform = (new Random().NextDouble() * 2.0 - 1.0) * 6.5;
                        //        _album.AlbumImage.Add(_albumImage);
                        //    }
                        //}
                        _album.ClickCommand = new ActionCommand<object>(obj =>
                        {
                            EventAggregator.Publish("MyPhoto".AsViewNavigationArgs());
                            EventAggregator.Publish<AlbumModel>(_album);
                        });
                        _album.EditCommand = new ActionCommand<object>(obj =>
                        {
                            if (obj != null)
                            {
                                this.SelectedItem = obj as AlbumModel;
                                EditAlbum();
                            }
                        });
                        _album.DeleteCommand = new ActionCommand<object>(obj =>
                            {
                                if (obj != null)
                                {
                                    this.SelectedItem = obj as AlbumModel;
                                    DeleteAlbum();
                                }
                            });
                        _album.AlbumNameChanged += (s, ea) =>
                        {
                            if (((AlbumModel)s).AlbumName == ((AlbumModel)s).OldAlbumName)
                                return;
                            MaFamilleService.ServiceClient albumClient = new MaFamilleService.ServiceClient();
                            albumClient.IsAlbumExistsCompleted += delegate(object albumSender, MaFamilleService.IsAlbumExistsCompletedEventArgs albumEventargs)
                            {
                                if (e.Error == null)
                                {
                                    MaFamilleService.AlbumModel newAlbum = albumEventargs.Result as MaFamilleService.AlbumModel;
                                    if (newAlbum.IsAlbumExists)
                                    {
                                        ((AlbumModel)s).AlbumName = ((AlbumModel)s).OldAlbumName;
                                        MessageBox.Show("Album already exists!");
                                    }
                                    else
                                    {
                                        AlbumModel nAlbum = (AlbumModel)s;
                                        nAlbum.OldAlbumName = nAlbum.AlbumName;
                                    }
                                }
                            };
                            albumClient.IsAlbumExistsAsync(((AlbumModel)s).AlbumName, ((AlbumModel)s).OldAlbumName, true);
                        };
                        _album.IsLoaded = true;
                        AlbumList.Add(_album);
                    }
                    //AddDefaultAlbum();
                    TempAlbumList = new ObservableCollection<AlbumModel>();
                    AddToTempAlbumList(AlbumList, 0);
                    #region
                    //AlbumModel _albumNew = new AlbumModel();
                    //_albumNew.AlbumName = "New Album";
                    //_albumNew.OldAlbumName = "New Album";
                    //_albumNew.Click = new DelegateCommand<object>(obj =>
                    //{
                    //    if (obj != null)
                    //    {
                    //        this.SelectedItem = obj as AlbumModel;
                    //        EditPhoto();
                    //    }
                    //});
                    //_albumNew.AlbumNameChanged += (s, ea) =>
                    //{
                    //    MaFamilleService.ServiceClient albumClient = new MaFamilleService.ServiceClient();
                    //    albumClient.IsAlbumExistsCompleted += delegate(object albumSender, MaFamilleService.IsAlbumExistsCompletedEventArgs albumEventargs)
                    //    {
                    //        if (e.Error == null)
                    //        {
                    //            if (albumEventargs.Result)
                    //            {
                    //                MessageBox.Show("Album already exists!");
                    //                ((AlbumModel)s).AlbumName = ((AlbumModel)s).OldAlbumName;
                    //            }
                    //            else
                    //            {
                    //                ((AlbumModel)s).OldAlbumName = ((AlbumModel)s).AlbumName;
                    //            }
                    //        }
                    //    };
                    //    albumClient.IsAlbumExistsAsync(((AlbumModel)s).AlbumName, ((AlbumModel)s).OldAlbumName);
                    //    //((AlbumModel)s).AlbumName = ((AlbumModel)s).OldAlbumName;
                    //};
                    //AlbumList.Add(_albumNew);
                    //AlbumList.Add(new AlbumModel { AlbumName = "New Album", Click = new DelegateCommand<object>(obj => { this.SelectedItem = obj as AlbumModel; EditPhoto(); }) });
                    #endregion 'Commented
                    IsLoaded = true;
                }
            };
            IsLoaded = false;
            client.GetAlbumsAsync();
        }

        void AddToTempAlbumList(ObservableCollection<AlbumModel> _albumList, int _index)
        {
            if (_index == _albumList.Count)
            {
                //TempAlbumList.Add(new AlbumModel { AlbumName = "New Album", ClickCommand = new ActionCommand<string>(obj =>
                //        {
                //            if (obj != null)
                //            {                                
                //                EditPhoto();
                //            }
                //        })});
                //AddDefaultAlbum();
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

        void AddDefaultAlbum()
        {
            AlbumModel _albumNew = new AlbumModel();            
            _albumNew.AlbumName = "";
            _albumNew.OldAlbumName = "New Album";
            _albumNew.ClickCommand = new ActionCommand<object>(obj =>
            {
                if (obj != null)
                {
                    this.SelectedItem = obj as AlbumModel;
                    if (this.SelectedItem.AlbumName == "")
                    {
                        EditAlbum();
                    }
                    else
                    {
                        EventAggregator.Publish("MyPhoto".AsViewNavigationArgs());
                        EventAggregator.Publish<AlbumModel>(this.SelectedItem);
                    }                    
                }
            });
            _albumNew.EditCommand = new ActionCommand<object>(obj =>
            {
                if (obj != null)
                {
                    this.SelectedItem = obj as AlbumModel;
                    EditAlbum();
                }
            });
            _albumNew.DeleteCommand = new ActionCommand<object>(obj =>
            {
                if (obj != null)
                {
                    this.SelectedItem = obj as AlbumModel;
                    DeleteAlbum();
                }
            });
            _albumNew.AlbumImage = new ObservableCollection<AlbumImage>();
            AlbumImage addAlbum = new AlbumImage();
            addAlbum.ImageStream = AddAlbumImage();
            _albumNew.AlbumImage.Add(addAlbum);
            _albumNew.AlbumNameChanged += (s, ea) =>
            {
                if (((AlbumModel)s).AlbumName == ((AlbumModel)s).OldAlbumName)
                    return;
                MaFamilleService.ServiceClient albumClient = new MaFamilleService.ServiceClient();
                albumClient.IsAlbumExistsCompleted += delegate(object albumSender, MaFamilleService.IsAlbumExistsCompletedEventArgs albumEventargs)
                {
                    if (albumEventargs.Error is FaultException<MaFamilleService.CustomException>)
                    {
                        MessageBox.Show(albumEventargs.Error.Message);
                        return;
                    }
                    if (albumEventargs.Error == null)
                    {
                        MaFamilleService.AlbumModel newAlbum = albumEventargs.Result as MaFamilleService.AlbumModel;
                        
                        if (newAlbum.IsAlbumExists)
                        {
                            ((AlbumModel)s).AlbumName = ((AlbumModel)s).OldAlbumName;
                            MessageBox.Show("Album already exists!");
                        }
                        else
                        {
                            AlbumModel nAlbum = s as AlbumModel;
                            //nAlbum.AlbumName = newAlbum.AlbumName;
                            //nAlbum.OldAlbumName = newAlbum.OldAlbumName;
                            nAlbum.AlbumImage.Clear();
                            AlbumImage aImage;
                            foreach (MaFamilleService.AlbumImage img in newAlbum.AlbumImage)
                            {
                                aImage = new AlbumImage();
                                aImage.ImageStream = CreateThumbnailImage(new MemoryStream(img.ImageStream),300);
                                aImage.Transform = img.Transform;
                                nAlbum.AlbumImage.Add(aImage);                                
                            }                           
                            AddDefaultAlbum();
                        }
                    }
                };
                albumClient.IsAlbumExistsAsync(((AlbumModel)s).AlbumName, ((AlbumModel)s).OldAlbumName,false);
                //((AlbumModel)s).AlbumName = ((AlbumModel)s).OldAlbumName;
            };
            //MaFamilleService.ServiceClient addAlbumClient = new MaFamilleService.ServiceClient();
            //addAlbumClient.GetDefaultAlbumImageCompleted+=(s,e) =>
            //{
            //    if (e.Error == null)
            //    {
            //        addAlbum.ImageStream=e.Result;
            //        _albumNew.AlbumImage.Add(addAlbum);
            //    }
            //};
            //addAlbumClient.GetDefaultAlbumImageAsync();
            _albumNew.IsLoaded = true;
            TempAlbumList.Add(_albumNew);
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
            
            //Image thumbnail = new Image();
            //thumbnail.Width = cx;
            //thumbnail.Height = cy;
            //thumbnail.Source = wb2;            
            //return thumbnail;
        } 

        public void EditAlbum()
        {
            modalDialogService.ShowDialog(new EditAlbum(), new EditAlbumViewModel { AlbumName = this.SelectedItem.AlbumName }, r =>
            {
                if(r.DialogResult.Value)
                    SelectedItem.AlbumName = r.AlbumName;
            });
        }
        
        void DeleteAlbum()
        {
            new DialogService().ShowDialog("Confirm", "Are you sure you want to delete all the photos in the album " + this.SelectedItem.AlbumName + "", true, r =>
            {
                if (r)
                {
                    MaFamilleService.ServiceClient albumClient = new MaFamilleService.ServiceClient();
                    albumClient.DeleteAlbumCompleted += (s, e) =>
                    {
                        if (e.Error is FaultException<MaFamilleService.CustomException>)
                        {
                            MessageBox.Show(e.Error.Message);
                            return;
                        }
                        if (e.Error == null)
                        {
                            this.SelectedItem.IsLoaded = false;
                            var timer = new DispatcherTimer();
                            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
                            timer.Tick += (se, es) =>
                            {
                                timer.Stop();
                                TempAlbumList.Remove(this.SelectedItem);
                            };
                            timer.Start();
                        }
                    };
                    albumClient.DeleteAlbumAsync(this.SelectedItem.AlbumName);
                }
            });            
        }

        //public void RenameAlbum(AlbumModel album)
        //{
        //    if (album.AlbumName == album.OldAlbumName)
        //        return;
        //    MaFamilleService.ServiceClient albumClient = new MaFamilleService.ServiceClient();
        //    albumClient.IsAlbumExistsCompleted += delegate(object albumSender, MaFamilleService.IsAlbumExistsCompletedEventArgs albumEventargs)
        //    {
        //        if (albumEventargs.Error == null)
        //        {
        //            if (albumEventargs.Result)
        //            {
        //                album.AlbumName = album.OldAlbumName;
        //                MessageBox.Show("Album already exists!");
        //            }
        //            else
        //            {
        //                if (album.OldAlbumName == "New Album")
        //                    AddDefaultAlbum();
        //                album.OldAlbumName = album.AlbumName;
        //            }
        //        }
        //    };
        //    albumClient.IsAlbumExistsAsync(album.AlbumName, album.OldAlbumName);
        //}        

        protected override void ActivateView(string viewName, IDictionary<string, object> viewParameters)
        {
            base.ActivateView(viewName, viewParameters);
            EventAggregator.Publish<AlbumModel>(new AlbumModel { AlbumName = "" });            
        }

        private byte[] AddAlbumImage()
        {
            Uri uri = new Uri("/MaFamille.Albums;component/Images/AddAlbum1.png", UriKind.Relative);
            Stream stream=System.Windows.Application.GetResourceStream(uri).Stream;
            return new BinaryReader(stream).ReadBytes((int)stream.Length);

            //WriteableBitmap wbmap = new WriteableBitmap(180, 90);
            //wbmap.SetSource(stream);
            //return ToByteArray(wbmap);

            //FileStream stream = new FileStream(uri.OriginalString, FileMode.Open, FileAccess.Read);
            //BinaryReader reader = new BinaryReader(stream);
            //return reader.ReadBytes((int)stream.Length);
            
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
