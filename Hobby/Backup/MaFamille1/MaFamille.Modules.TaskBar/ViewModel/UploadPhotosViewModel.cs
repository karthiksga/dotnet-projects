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
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.IO;
using System.Collections.Generic;
using MaFamille.Modules.TaskBar.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using System.ServiceModel;
using MaFamille.Modules.TaskBar.Converters;
using MaFamille.Modules.TaskBar;
namespace MaFamille.Modules.TaskBar.ViewModel
{
    public class UploadPhotosViewModel : INotifyPropertyChanged
    {
        public ICommand CommandClick { get; set; }
        const int CHUNK_SIZE = 3000000;//4194304;//30000; //30KB
        ObservableCollection<FileUploadModel> _files;
        public bool _cancel;

        public ObservableCollection<FileUploadModel> Files
        {
            get { return _files; }
            set 
            { 
                    _files = value;
                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("Files"));
                    });
            }
        }
        private Dispatcher Dispatcher;

        private FileUploadModel fileUpload;
        public FileUploadModel FileUpload
        {
            get { return fileUpload; }
            set
            {
                fileUpload = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("BytesUploaded"));
                });
            }
        }

        private long _totalUploadSize;
        public long TotalUploadSize 
        {
            get { return _totalUploadSize; }
            set
            {
                _totalUploadSize = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("TotalUploadSize"));
                });
            }
        }

        private int _filesCount;
        public int FilesCount
        {
            get { return _filesCount; }
            set
            {
                _filesCount = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FilesCount"));
                });
            }
        }

        private string _txtTotalFiles;
        public string TxtTotalFiles
        {
            get { return _txtTotalFiles; }
            set
            {
                _txtTotalFiles = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("TxtTotalFiles"));
                });
            }
        }

        private string _txtTotalSize;
        public string TxtTotalSize
        {
            get { return _txtTotalSize; }
            set
            {
                _txtTotalSize = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("TxtTotalSize"));
                });
            }
        }

        private long _totalUploaded;
        public long TotalUploaded
        {
            get { return _totalUploaded; }
            set
            {
                _totalUploaded = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("TotalUploaded"));
                });
            }
        }

        private bool _isUploadEnabled;

        public bool IsUploadEnabled
        {
            get { return _isUploadEnabled; }
            set
            {
                _isUploadEnabled = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsUploadEnabled"));
                });
            }
        }

        private string _albumName;
        public string AlbumName
        {
            get { return _albumName; }
            set
            {
                _albumName = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("AlbumName"));
                });
            }
        }

        public UploadPhotosViewModel(Dispatcher dispatcher)
        {
            this.Dispatcher = dispatcher;            
            _files = new ObservableCollection<FileUploadModel>();
            TotalUploaded = 0;
            TotalUploadSize = 0;
            _files.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_files_CollectionChanged);
            CommandClick = new DelegateCommand<object>(argument =>
            {
                switch (argument.ToString())
                {                    
                    case "Clear":
                        _cancel = true;
                        Clear();
                        break;
                    default:
                        break;
                }
            });
        }

        void _files_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FilesCount = _files.Count;
            TotalUploadSize = _files.Sum(f => f.Length);
            TxtTotalFiles = string.Format("Total:{0}", FilesCount);
            TxtTotalSize = string.Format("{0} of {1}",
                new ByteConverter().Convert(TotalUploaded, this.GetType(), null, null).ToString(),
                new ByteConverter().Convert(TotalUploadSize, this.GetType(), null, null).ToString());
        }

        public void Clear()
        {

            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            foreach (FileUploadModel file in _files)
            {
                if (file.Status == FileUploadStatus.Uploading)
                {
                    client.RemovePhotoCompleted += delegate(object sender, MaFamilleService.RemovePhotoCompletedEventArgs e)
                    {
                        FileUploadModel f = _files.Single(m => m.ID == e.Result);
                        f.Status = FileUploadStatus.Removed;
                    };
                    client.RemovePhotoAsync(file.Name, file.ID);
                }
                else
                {
                    file.Status = FileUploadStatus.Removed;
                }
            }
            TotalUploaded = 0;
            TotalUploadSize = 0;
            TxtTotalSize = "";
            TxtTotalFiles = "";
        }

        public void BrowseFile()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            if ((bool)fd.ShowDialog())
            {
                foreach (FileInfo file in fd.Files)
                {
                    if (_files.Count > 0 && _files.Any(m => m.Name == file.Name))
                        continue;
                    FileUpload = new FileUploadModel(Dispatcher);
                    FileUpload.Name = file.Name;
                    fileUpload.AlbumName = AlbumName;
                    FileUpload.ID = _files.Count + 1;
                    Stream s = file.OpenRead();
                    FileUpload.FileContent = new byte[s.Length];
                    fileUpload.Length = s.Length;
                    s.Read(FileUpload.FileContent, 0, (int)s.Length);
                    s = file.OpenRead();
                    FileUpload.Size = s.Length / 1000;
                    int chunkSize = (int)s.Length;
                    FileUpload.Contents = new List<byte[]>();
                    while (s.Position > -1 && s.Position < s.Length)
                    {
                        if (s.Length - s.Position >= CHUNK_SIZE)
                            chunkSize = CHUNK_SIZE;
                        else
                            chunkSize = (int)(s.Length - s.Position);

                        byte[] fileBytes = new byte[chunkSize];
                        int byteCount = s.Read(fileBytes, 0, chunkSize);
                        FileUpload.Contents.Add(fileBytes);
                    }
                    s.Close();
                    FileUpload.UploadPercent = 0;
                    FileUpload.StatusChanged += new EventHandler(FileUpload_StatusChanged);
                    _files.Add(FileUpload);
                }
            }
        }

        void FileUpload_StatusChanged(object sender, EventArgs e)
        {
            if (((FileUploadModel)sender).Status == FileUploadStatus.Complete)
                UploadFile();
            else if (((FileUploadModel)sender).Status == FileUploadStatus.Removed)
                _files.Remove((FileUploadModel)sender);
        }

        public void UploadFile()
        {
            FileUploadModel fileModel = null;
            if (_files.Count(f => f.Status == FileUploadStatus.Pending) > 0)
                fileModel = _files.First(m => m.Status == FileUploadStatus.Pending);

            if (fileModel != null)
            {
                fileModel.Status = FileUploadStatus.Uploading;
                MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
                client.IsFileExistsCompleted += delegate(object sender, MaFamilleService.IsFileExistsCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        if (e.Result)
                        {
                            MessageBoxResult result = MessageBox.Show("File already exists, overwrite?", "Overwrite?", MessageBoxButton.OKCancel);
                            if (result == MessageBoxResult.OK)
                                SendFile(fileModel, false);
                            else
                                fileModel.Status = FileUploadStatus.Canceled;
                        }
                        else
                        {
                            SendFile(fileModel, false);
                        }
                    }
                };
                client.IsFileExistsAsync(AlbumName, fileModel.Name, fileModel.Length);
            }
        }

        public void SendFile(FileUploadModel filemodel, bool append)
        {
            try
            {
                if (_cancel)
                    return;
                MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
                client.UploadPhotoCompleted += delegate(object sender, MaFamilleService.UploadPhotoCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        MaFamilleService.FileUploadModel fi = e.Result;
                        FileUploadModel f = _files.Single(m => m.ID == fi.ID);

                        if (f.Contents.Count > 0)
                        {
                            if (_cancel)
                                return;
                            TotalUploaded += f.Contents[0].Length;
                            TxtTotalSize = string.Format("{0} of {1}",
                                                    new ByteConverter().Convert(TotalUploaded, this.GetType(), null, null).ToString(),
                                                    new ByteConverter().Convert(TotalUploadSize, this.GetType(), null, null).ToString());
                            f.Contents.RemoveAt(0);
                        }
                        if (f.Contents.Count == 0)
                        {
                            f.UploadPercent = 100;
                            f.Status = FileUploadStatus.Complete;
                        }
                        else
                        {
                            f.UploadPercent = 100 - Convert.ToInt32(((f.Contents.Count - 1) * CHUNK_SIZE + f.Contents[f.Contents.Count - 1].Length) * 0.1 / f.Size);
                            SendFile(f, true);
                        }
                    }
                    else
                    {
                        MessageBox.Show(e.Error.Message);
                    }
                };
                client.UploadPhotoAsync(filemodel.AlbumName, filemodel.Name, filemodel.Contents[0], filemodel.ID, append);
            }
            catch (FaultException<MaFamilleService.CustomException> ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
