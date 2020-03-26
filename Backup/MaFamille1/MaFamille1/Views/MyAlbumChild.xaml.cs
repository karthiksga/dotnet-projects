using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MaFamille1.Validation;
using System.ComponentModel;
using System.Collections;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Composite.Presentation.Commands;
using MaFamille1.Model;

namespace MaFamille1
{
    public class AlbumChild : ViewModelBase, INotifyDataErrorInfo//IDataErrorInfo
    {
        private ModelValidator _validator;

        public ModelValidator Validator
        {
            get { return _validator; }
            set { _validator = value; }
        }
        
        //= new ModelValidator();

        private string albumName;
        public string AlbumName
        {
            get { return albumName; }
            set
            {
                albumName = value;
                RaisePropertyChanged("AlbumName");
            }
        }

        public ICommand Click { get; set; }

        public AlbumChild(string albumName)
        {
            Validator = new ModelValidator();
            AlbumName = albumName;            
            this._validator.AddValidationFor(() => this.AlbumName).NotEmpty().Show("Enter Album Name");
            this._validator.AddValidationFor(() => this.AlbumName).Must(() => this.AlbumName != "New Album").Show("Enter valid Name");
            this.PropertyChanged += (s, e) =>
                {
                    if (this._validator.PropertyNames.Contains(e.PropertyName))
                    {
                        this._validator.ValidateProperty(e.PropertyName);
                        OnErrorsChanged(e.PropertyName);
                    }
                };
        }

        public bool Validate()
        {
            if (this.Validator.ValidateAll())
            {
                return true;
            }
            else
            {
                this.Validator.PropertyNames.ToList().ForEach(RaisePropertyChanged);
                return false;
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string propertyName)
        //{
        //    if (this.PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        #region INotifyDataErrorInfo

        public IEnumerable GetErrors(string propertyName)
        {
            return this._validator.GetErrors(propertyName);
        }

        public bool HasErrors
        {
            get { return this._validator.ErrorMessages.Count > 0; }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            this.RaisePropertyChanged("HasErrors");
        }
        #endregion

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { return this._validator.GetErrors(columnName).FirstOrDefault(); }
        }
    }
    public partial class MyAlbumChild : ChildWindow
    {
        //public MyAlbumChild(AlbumChild ch)
        //{
        //    InitializeComponent();
        //    this.Loaded += new RoutedEventHandler(MyAlbumChild_Loaded);
        //    Child = ch;
        //}
        AlbumModel album;
        public MyAlbumChild(AlbumModel _album)
        {
            InitializeComponent();
            album = _album;
            this.Loaded += new RoutedEventHandler(MyAlbumChild_Loaded);            
        }

        void MyAlbumChild_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = album;
            //txtAlbumName.Text = AlbumName;
        }
        
        public event EventHandler SubmitClicked;

        //private string _albumName;
        //public string AlbumName
        //{
        //    get { return _albumName; }
        //    set { _albumName = value; }
        //}

        private AlbumChild child;

        public AlbumChild Child
        {
            get { return child; }
            set { child = value; }
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubmitClicked != null)
            {                
                //if (Child.Validate())
                if (album.Validate())
                {
                    SubmitClicked(this, new EventArgs());
                    this.DialogResult = true;
                }                
            }            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            album.AlbumName = album.OldAlbumName;
            this.DialogResult = false;
        }
    }
}

