﻿using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Loon.Extensions;
using Loon.Interfaces;
using Loon.Models;
using Twitter.Models;

#pragma warning disable S1075 // URIs should not be hardcoded

namespace Loon.ViewModels
{
    internal class MainWindowViewModel : NotifyPropertyChanged
    {
        public ISettings Settings { get; }
        public ITwitterService TwitterService { get; }
        public User? UserProfileContext { get => Getter<User>(); set => Setter(value); }

        public MainWindowViewModel(ISettings settings, ITwitterService twitterService)
        {
            Settings = settings;
            TwitterService = twitterService;
            Settings.PropertyChanged += OnSettingsUpdated;
        }

        public void Load(IWindow window)
        {
            Settings.Load();
            SetWindowLocation(window);
            window.Closing += delegate { Save(window); };
        }

        public void Save(IWindow window)
        {
            UpdateWindowLocation(window);
            Settings.Save();
        }

        private void SetWindowLocation(IWindow window)
        {
            window.Width = Settings.Location.Width;
            window.Height = Settings.Location.Height;
            window.Position = new PixelPoint(Settings.Location.X, Settings.Location.Y);
        }

        private void UpdateWindowLocation(IWindow window)
        {
            Settings.Location.X = window.Position.X;
            Settings.Location.Y = window.Position.Y;
            Settings.Location.Width = window.Width;
            Settings.Location.Height = window.Height;
        }

        private void OnSettingsUpdated(object? _, PropertyChangedEventArgs e)
        {
            TwitterService.AuthenticationTokens(
                Settings.AccessToken,
                Settings.AccessTokenSecret);

            UpdateTheme(e.PropertyName);
        }

        private void UpdateTheme(string? propertyName)
        {
            if (propertyName.IsEqualTo(nameof(ISettings.UseLightTheme)))
            {
                // This is slated to change in the next release of Avalonia
                var styles = new StyleInclude(new Uri("resm:Styles"))
                {
                    Source = Settings.UseLightTheme
                        ? new Uri("avares://Avalonia.Themes.Default/Accents/BaseLight.xaml")
                        : new Uri("avares://Avalonia.Themes.Default/Accents/BaseDark.xaml")
                };

                App.Current.Styles[1] = styles;
            }
        }
    }
}