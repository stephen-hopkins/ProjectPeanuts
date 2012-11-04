﻿using Peanuts.View.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238


// PEANUTS!!

namespace Peanuts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CalendarController calendarController;
        private static List<ISeries> series { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        } 

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            calendarController = new CalendarController();
            series = Calendar.GetCalendarSeries();
            ItemListView.DataContext = series;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Series selectedSeries = (Series)e.AddedItems.First();
            EpisodePage.episode = selectedSeries.NextEpisode;
            Frame.Navigate(typeof(Peanuts.View.Pages.EpisodePage));
        }

        private void StackPanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            Appbar.IsOpen = true;
        }
    }
}
