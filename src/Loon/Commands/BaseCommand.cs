﻿using System;
using System.Windows.Input;

namespace Loon.Commands
{
    public class BaseCommand : ICommand
    {
        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public virtual void Execute(object? parameter)
        {
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public event EventHandler? CanExecuteChanged;
    }
}