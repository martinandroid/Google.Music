﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GoogleMusicApi;
using GoogleMusicApi.Requests;
using GoogleMusicApi.Requests.Data;
using GooglePlayMusic.Desktop.Common;
using GooglePlayMusic.Desktop.Managers;

namespace GooglePlayMusic.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();
            LoadingOverlay.Visibility = Visibility.Visible;
        }

        private async void Index_OnLoaded(object sender, RoutedEventArgs e)
        {
            await LoadListenNowSituations();
            await LoadListenNowData();

            LoadingOverlay.Visibility = Visibility.Hidden;
        }

        private async Task LoadListenNowSituations()
        {
            var data = await SessionManager.MobileClient.ListListenNowSituationsAsync();
            if (data == null) return;
            SessionManager.ListenNowSituationResponse = data;
            SituationTitle.Text = data.PrimaryHeader;
            SituationDescription.Text = data.SubHeader;
            foreach (var situation in data.Situations)
            {
                var card = new Card(new BitmapImage(new Uri(situation.ImageUrl)),
                    situation.Title, "")
                {
                    Width = 175,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    CardSectionHeight = new GridLength(0, GridUnitType.Auto)
                };
                ListenNowSituationPanel.Children.Add(card);
            }

        }

        private async Task LoadListenNowData()
        {
            var data = await SessionManager.MobileClient.ListListenNowTracksAsync();
            SessionManager.ListenNowTracksResponse = data;
            if (data == null) return;
            foreach (var item in data.Items)
            {
                if (item.CompositeArtRefs != null && item.CompositeArtRefs.Length > 0)
                {
                    var card =
                        new Card(
                            new BitmapImage(new Uri(item.CompositeArtRefs.First(x => x.AspectRatio == "1").Url)),
                            item.Album != null ? item.Album.Title : item.RadioStation.Title, item.SuggestionText)
                        {
                            Width = (BaseStackPanel.ActualWidth - (7*4*2) - 20)/4,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            MinWidth = 125
                        };
                    ListenNowWrapPanel.Children.Add(card);
                }
            }
        }

        private void Page_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeListenNowSuggestionCards();
        }

        private void ResizeListenNowSuggestionCards()
        {

            foreach (Control child in ListenNowWrapPanel.Children)
            {
                if (ActualWidth < 800)
                {
                    child.Width = (BaseStackPanel.ActualWidth - (7 * 4 * 2) - 20) / 2;
                }
                else
                {
                    child.Width = (BaseStackPanel.ActualWidth - (7*4*2) - 20)/4;
                }
            }
        }
    }
}
