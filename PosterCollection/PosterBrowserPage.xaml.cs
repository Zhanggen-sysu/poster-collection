﻿using PosterCollection.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PosterCollection
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PosterBrowserPage : Page
    {
        private String url;
        private Pictures pictures;
        private ObservableCollection<Poster> allPosters = new ObservableCollection<Poster>();
        private ObservableCollection<Backdrop> allBackdrops = new ObservableCollection<Backdrop>();

        public PosterBrowserPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is String)
            {
                allPosters.Clear();
                allBackdrops.Clear();
                url = (String)e.Parameter;
                HttpClient client = new HttpClient();
                String Jresult = await client.GetStringAsync(url);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Pictures));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(Jresult));
                pictures = (Pictures)serializer.ReadObject(ms);
                foreach(var poster in pictures.posters)
                {
                    poster.file_path = "https://image.tmdb.org/t/p/w500" + poster.file_path;
                    allPosters.Add(poster);
                }
                if(allPosters.Count < 1)
                {
                    posterTextBlock.Visibility = Visibility.Collapsed;
                }
                foreach(var backdrop in pictures.backdrops)
                {
                    backdrop.file_path = "https://image.tmdb.org/t/p/w500" + backdrop.file_path;
                    allBackdrops.Add(backdrop);
                }
                if(allBackdrops.Count < 1)
                {
                    backdropTextBlock.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void GridView_postersItemClick(object sender, ItemClickEventArgs e)
        {
            Poster poster = (Poster)e.ClickedItem;
            this.Frame.Navigate(typeof(ShowPosterPage), poster.file_path);
        }

        private void GridView_backdropsItemClick(object sender, ItemClickEventArgs e)
        {
            Backdrop backdrop = (Backdrop)e.ClickedItem;
            this.Frame.Navigate(typeof(ShowPosterPage), backdrop.file_path);
        }
    }
}
