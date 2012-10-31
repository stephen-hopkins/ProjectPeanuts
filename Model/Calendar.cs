using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peanuts.Interfaces;
using System.Collections.ObjectModel;

namespace Peanuts.Model
{
    public sealed class Calendar
    {

        private static Calendar calendar = new Calendar();

        private ObservableCollection<IEpisode> _episodes = new ObservableCollection<IEpisode>();
        public ObservableCollection<IEpisode> Episodes
        {
            get { return this._episodes; }
        }

        public void AddEpisode(IEpisode episode)
        {
            calendar.Episodes.Add(episode);
        }

        //public Calendar() { }
            
    }
}
