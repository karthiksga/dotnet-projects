using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MaFamille1.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows;

namespace MaFamille1
{
    public class AlbumViewModel:INotifyPropertyChanged
    {
        //private string _albumName;
        //public string AlbumName
        //{
        //    get
        //    {
        //        return _albumName;
        //    }
        //    set
        //    {
        //        _albumName = value;
        //        NotifyPropertyChanged("AlbumName");
        //    }
        //}
        //private AlbumModel _albumModel;
        //public AlbumModel AlbumModel
        //{
        //    get
        //    {
        //        return _albumModel;
        //    }
        //    set
        //    {
        //        _albumModel = value;
        //        NotifyPropertyChanged("AlbumModel");
        //    }
        //}

        private AlbumModel _selectedItem;
        public AlbumModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<AlbumModel> _albumList;
        public ObservableCollection<AlbumModel> AlbumList
        {
            get
            {
                return _albumList;
            }
            set
            {
                _albumList = value;
                NotifyPropertyChanged("AlbumList");
            }
        }

        public ICommand Click { get; set; }

        public AlbumViewModel()
        {
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
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            client.GetAlbumsCompleted += delegate(object sender, MaFamilleService.GetAlbumsCompletedEventArgs e)
            {
                AlbumList = new ObservableCollection<AlbumModel>();
                if (e.Result != null)
                {
                    ObservableCollection<MaFamilleService.AlbumModel> albums = e.Result as ObservableCollection<MaFamilleService.AlbumModel>;
                    foreach (MaFamilleService.AlbumModel album in albums)
                    {
                        AlbumModel _album = new AlbumModel();
                        _album.AlbumName = album.AlbumName;
                        _album.OldAlbumName = album.AlbumName;
                        _album.Click = new DelegateCommand<object>(obj =>
                        {
                            if (obj != null)
                            {
                                this.SelectedItem = obj as AlbumModel;
                                EditPhoto();
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
                                    if (albumEventargs.Result)
                                    {
                                        ((AlbumModel)s).AlbumName = ((AlbumModel)s).OldAlbumName;
                                        MessageBox.Show("Album already exists!");                                        
                                    }
                                    else
                                    {
                                        ((AlbumModel)s).OldAlbumName = ((AlbumModel)s).AlbumName;
                                    }
                                }
                            };
                            albumClient.IsAlbumExistsAsync(((AlbumModel)s).AlbumName, ((AlbumModel)s).OldAlbumName);                            
                        };
                        AlbumList.Add(_album);                        
                    }
                    AddDefaultAlbum();

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
                }
            };
            client.GetAlbumsAsync();
        }

        void AddDefaultAlbum()
        {
            AlbumModel _albumNew = new AlbumModel();
            _albumNew.AlbumName = "New Album";
            _albumNew.OldAlbumName = "New Album";
            _albumNew.Click = new DelegateCommand<object>(obj =>
            {
                if (obj != null)
                {
                    this.SelectedItem = obj as AlbumModel;
                    EditPhoto();
                }
            });
            _albumNew.AlbumNameChanged += (s, ea) =>
            {
                if (((AlbumModel)s).AlbumName == ((AlbumModel)s).OldAlbumName)
                    return;
                MaFamilleService.ServiceClient albumClient = new MaFamilleService.ServiceClient();
                albumClient.IsAlbumExistsCompleted += delegate(object albumSender, MaFamilleService.IsAlbumExistsCompletedEventArgs albumEventargs)
                {
                    if (albumEventargs.Error == null)
                    {
                        if (albumEventargs.Result)
                        {
                            ((AlbumModel)s).AlbumName = ((AlbumModel)s).OldAlbumName;
                            MessageBox.Show("Album already exists!");                            
                        }
                        else
                        {
                            ((AlbumModel)s).OldAlbumName = ((AlbumModel)s).AlbumName;
                            AddDefaultAlbum();
                        }
                    }
                };
                albumClient.IsAlbumExistsAsync(((AlbumModel)s).AlbumName, ((AlbumModel)s).OldAlbumName);
                //((AlbumModel)s).AlbumName = ((AlbumModel)s).OldAlbumName;
            };
            AlbumList.Add(_albumNew);
        }       

        public void EditPhoto()
        {
            //MyAlbumChild child = new MyAlbumChild(new AlbumChild(SelectedItem.AlbumName));
            MyAlbumChild child = new MyAlbumChild(SelectedItem);
            child.SubmitClicked += new EventHandler(child_SubmitClicked);
            //child.AlbumName = SelectedItem.AlbumName;
            child.Show();
        }

        void child_SubmitClicked(object sender, EventArgs e)
        {            
            RenameAlbum(SelectedItem);
        }

        public void RenameAlbum(AlbumModel album)
        {
            if (album.AlbumName == album.OldAlbumName)
                return;
            MaFamilleService.ServiceClient albumClient = new MaFamilleService.ServiceClient();
            albumClient.IsAlbumExistsCompleted += delegate(object albumSender, MaFamilleService.IsAlbumExistsCompletedEventArgs albumEventargs)
            {
                if (albumEventargs.Error == null)
                {
                    if (albumEventargs.Result)
                    {
                        album.AlbumName = album.OldAlbumName;
                        MessageBox.Show("Album already exists!");
                    }
                    else
                    {
                        if(album.OldAlbumName=="New Album")
                            AddDefaultAlbum();
                        album.OldAlbumName = album.AlbumName;                        
                    }
                }
            };
            albumClient.IsAlbumExistsAsync(album.AlbumName, album.OldAlbumName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
