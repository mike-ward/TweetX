﻿using System;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Loon.Behaviors;

namespace Loon.Commands
{
    public class TabGoBackCommand : ICommand
    {
        public static readonly TabGoBackCommand Command = new TabGoBackCommand();

        public TabItem? LastSelectedTab { get; set; }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var tabControl = App.MainWindow.FindDescendantOfType<TabControl>();
            tabControl.SelectedIndex = PreviousIndexBehavior.GetPreviousIndex(tabControl);
        }

        public event EventHandler? CanExecuteChanged;

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }
    }
}