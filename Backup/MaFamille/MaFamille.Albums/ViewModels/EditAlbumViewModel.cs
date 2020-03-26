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
using Jounce.Framework.ViewModel;
using MaFamille.Common;
using Jounce.Core.Command;
using Jounce.Framework.Command;
using System.Text.RegularExpressions;

namespace MaFamille.Albums.ViewModels
{
    //[ExportAsViewModel("EditAlbum")]
    public class EditAlbumViewModel : BaseEntityViewModel //, IModalWindow
    {
        public IActionCommand CancelCommand { get; private set; }        

        public EditAlbumViewModel()
        {
            CancelCommand = new ActionCommand<object>(obj => CancelClicked(), obj => !Committed);
            var committedProp = ExtractPropertyName(() => Committed);
            
            if (!InDesigner)
            {
                PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName.Equals(committedProp))
                    {
                        CancelCommand.RaiseCanExecuteChanged();
                    }
                };
            }
        }

        public string AlbumName
        {
            get { return _albumName; }
            set
            {
                _albumName = value;
                RaisePropertyChanged(() => AlbumName);
                ValidateName(ExtractPropertyName(() => AlbumName), value);
            }
        }private string _albumName;

        public Boolean? DialogResult
        {
            get { return blnDialogResult; }
            set
            {
                blnDialogResult = value;
                RaisePropertyChanged(() => DialogResult);
            }
        }private Boolean? blnDialogResult = null;

        private void ValidateName(string prop, string value)
        {
            ClearErrors(prop);

            if (string.IsNullOrEmpty(value))
            {
                SetError(prop, "The field is required.");
            }
            //else if (!(Regex.IsMatch(_albumName,"^[:][\\][^*?\"'<>|@)(}{\\[\\]]{4,253}$")))
            //{
            //    SetError(prop, "Album name can't contain \\/:*?\"<>|");                                         
            //}
        }

        protected override void ValidateAll()
        {
            ValidateName(ExtractPropertyName(() => AlbumName), _albumName);            
        }

        public void CancelClicked()
        {
            Committed = true;
            DialogResult = false;
        }

        protected override void OnCommitted()
        {
            Committed = true;
            DialogResult = true;
        }
    }
}
