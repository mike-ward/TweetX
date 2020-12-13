﻿using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Loon.Services;
using Twitter.Models;

namespace Loon.Views.Content.Timelines.TweetItem
{
    public class TweetItemImage : UserControl
    {
        public bool Clearing { get; set; }

        public TweetItemImage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public async void LoadMediaAsync(object? sender, EventArgs e)
        {
            try
            {
                Clearing = false;

                if (sender is Image image)
                {
                    image.Source = null;
                    await Task.Delay(30).ConfigureAwait(true);

                    if (Clearing)
                    {
                        return;
                    }

                    var media = image.DataContext as Media;

                    if (media?.MediaUrl.Length > 0)
                    {
                        image.Source = await ImageService.GetImageAsync(media.MediaUrl, () => Clearing).ConfigureAwait(true);
                    }
                }
            }
            catch (Exception ex)
            {
                TraceService.Message(ex.Message);
            }
        }

        public void OpenInViewer(object? sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed &&
                sender is Grid grid &&
                grid.Children[0] is Image image)
            {
                ImageService.OpenInViewer(image);
            }
        }
    }
}