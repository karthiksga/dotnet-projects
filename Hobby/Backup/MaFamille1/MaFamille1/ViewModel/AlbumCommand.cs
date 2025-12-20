using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MaFamille1
{
    public class AlbumCommand:ICommand
    {
        AlbumViewModel albumViewModel;

        public AlbumCommand(AlbumViewModel albumVM)
        {
            albumViewModel = albumVM;
        }
    
        public bool  CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler  CanExecuteChanged;

        public void  Execute(object parameter)
        {
            //albumViewModel.GetPhoto(albumViewModel.album);
        }
}
}
