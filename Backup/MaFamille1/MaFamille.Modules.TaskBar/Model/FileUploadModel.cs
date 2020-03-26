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
using System.Windows.Threading;
using System.IO;
using System.Collections.Generic;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace MaFamille.Modules.TaskBar.Model
{
    public enum FileUploadStatus
    {
        Pending,
        Uploading,
        Complete,
        Error,
        Canceled,
        Removed,
        Resizing
    }

    public class FileUploadModel : INotifyPropertyChanged
    {
        private Dispatcher Dispatcher;
        public event ProgressChangedEvent UploadProgressChanged;
        public event EventHandler StatusChanged;
        public int ID { get; set; }
        public string Name { get; set; }
        public string AlbumName { get; set; }
        public double Size { get; set; }
        public long Length { get; set; }
        public List<byte[]> Contents { get; set; }
        public ICommand CommandClick { get; set; }

        public FileUploadModel(Dispatcher dispatcher)
        {
            this.Dispatcher = dispatcher;
            Status = FileUploadStatus.Pending;
            CommandClick = new DelegateCommand<object>(argument =>
            {
                switch (argument.ToString())
                {
                    case "Remove":
                        Remove();
                        break;                    
                    default:
                        break;
                }
            });
        }
        private byte[] fileContent;

        public byte[] FileContent
        {
            get { return fileContent; }
            set
            {
                fileContent = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FileContent"));                    
                });
            }
        }

        private FileUploadStatus status;
        public FileUploadStatus Status
        {
            get { return status; }
            set
            {
                status = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                    if (StatusChanged != null)
                        StatusChanged(this, null);
                });
            }
        }

        private long bytesUploaded;
        public long BytesUploaded
        {
            get { return bytesUploaded; }
            set
            {
                bytesUploaded = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("BytesUploaded"));
                });
            }
        }

        private int uploadPercent;
        public int UploadPercent
        {
            get { return uploadPercent; }
            set
            {
                uploadPercent = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("UploadPercent"));
                });
            }
        }

        public void Remove()
        {
            if (this.Status != FileUploadStatus.Uploading)
                this.Status = FileUploadStatus.Removed;
        }

        //private FileInfo file;
        //public FileInfo File
        //{
        //    get { return file; }
        //    set
        //    {
        //        file = value;
        //        //Stream temp = file.OpenRead();
        //        //FileLength = temp.Length;
        //        //temp.Close();
        //    }
        //}

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public delegate void ProgressChangedEvent(object sender, UploadProgressChangedEventArgs args);
    public class UploadProgressChangedEventArgs
    {
        public int ProgressPercentage { get; set; }
        public long BytesUploaded { get; set; }
        public long TotalBytesUploaded { get; set; }
        public long TotalBytes { get; set; }
        public string FileName { get; set; }

        public UploadProgressChangedEventArgs() { }

        public UploadProgressChangedEventArgs(int progressPercentage, long bytesUploaded, long totalBytesUploaded, long totalBytes, string fileName)
        {
            ProgressPercentage = progressPercentage;
            BytesUploaded = bytesUploaded;
            TotalBytes = totalBytes;
            FileName = fileName;
            TotalBytesUploaded = totalBytesUploaded;
        }
    }
}
