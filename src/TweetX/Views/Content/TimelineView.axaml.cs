﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TweetX.Views.Content
{
    public class TimelineView : UserControl
    {
        public TimelineView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}