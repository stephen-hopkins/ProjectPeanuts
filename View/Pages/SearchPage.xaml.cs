using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

namespace Peanuts.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {

        public static string searchString;
        private DataFetcher dataFetcher = new DataFetcher();
        private List<SeriesSummary> searchResult;

        public SearchPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {/**
            Task<List<SeriesSummary>> searchTask = dataFetcher.searchSeries(searchString);
            searchTask.Start();
            searchResult = searchTask.Result;
            foreach (SeriesSummary s in searchResult)
            {
                
                TextBlock label = new TextBlock();
                label.Text = s.Title;
                ResultListView.Items.Add(label);
            }
          **/
        }
    }
}
