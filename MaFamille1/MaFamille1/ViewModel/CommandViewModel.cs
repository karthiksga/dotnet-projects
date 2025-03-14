﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MaFamille1
{
    public class CommandViewModel:ViewModelBase
    {
        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentException("command");
            base.DisplayName = displayName;
            this.Command = command;            
        }

        public ICommand Command { get; private set; }
    }
}
