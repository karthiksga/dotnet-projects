using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using MaFamille.Modules.Core.Validation;
using System.Collections;

namespace MaFamille.Modules.Core.Model
{
    public class AlbumModel : ViewModelBase, INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private string _albumName;
        public string AlbumName
        {
            get
            {
                return _albumName;
            }
            set
            {
                _albumName = value;
                PropertyChange("AlbumName");
                //if (AlbumNameChanged != null)
                //    AlbumNameChanged(this, null);
            }
        }
        public event EventHandler AlbumNameChanged;

        private ModelValidator _validator;
        public ModelValidator Validator
        {
            get { return _validator; }
            set { _validator = value; }
        }

        private string _oldAlbumName;
        public string OldAlbumName
        {
            get
            {
                return _oldAlbumName;
            }
            set
            {
                _oldAlbumName = value;                
            }
        }

        private byte[] _imageStream;

        public byte[] ImageStream
        {
            get { return this._imageStream; }
            set {
                this._imageStream = value;
                PropertyChange("ImageStream");
                }
        }

        public ICommand Click { get; set; }
        public ICommand EditClick { get; set; }

        public void PropertyChange(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(property));
            }
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

        public AlbumModel()
        {
            Validator = new ModelValidator();            
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
    
        public event PropertyChangedEventHandler  PropertyChanged;

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
}
