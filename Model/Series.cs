﻿using Peanuts.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public class SeriesSummary
    {
        private string title;
        private Uri image;
        private string synopsis;
        private string year;
        private string id;
        private string channel;

        public SeriesSummary(string title, Uri image, string synopsis, string year, string id) {
            this.title = title;
            this.image = image;
            this.synopsis = synopsis;
            this.year = year;
            this.id = id;
        }

        public SeriesSummary() { }

        public string Title { get; set; }
        public Uri Image { get; set; }
        public string Synopsis { get; set; }
        public string Year { get; set; }
        public string ID { get; set; }
        public string Channel { get; set; }
    }



    public class Series : SeriesSummary, ISeries
    {
  
        private List<IEpisode> episodes;
        private IEpisode nextEpisode;

        public Series()
        {
            episodes = new List<IEpisode>();
        }

        public IEpisode NextEpisode
        {
            get
            {
                if (nextEpisode == null)
                {
                    SetNextEpisode();
                }
                return nextEpisode;
            }
        }

        public List<IEpisode> Episodes
        {
            get
            {
                return episodes;
            }
            set
            {
                episodes = value;
            }
        }

        private void SetNextEpisode()
        {
            nextEpisode = Episodes.First();
        }


        public void AddEpisode(IEpisode episode)
        {
            episodes.Add(episode);
        }

        public string NextEpisodeTitle
        {
            get
            {
                try
                {
                    return nextEpisode.Title;
                }
                catch (Exception)
                {
                    SetNextEpisode();
                    return NextEpisodeTitle;
                }
            }
        }

        public string NextEpisodeNumber
        {
            get
            {
                try
                {
                    string sNumber = "";
                    if (nextEpisode.SeasonNumber < 10)
                    {
                        sNumber = "S0" + nextEpisode.SeasonNumber;
                    }
                    else
                    {
                        sNumber = "S" + nextEpisode.SeasonNumber;
                    }
                    string eNumber = "";
                    if (nextEpisode.EpisodeNumber < 10)
                    {
                        eNumber = "E0" + nextEpisode.EpisodeNumber;
                    }
                    else
                    {
                        eNumber = "E" + nextEpisode.EpisodeNumber;
                    }
                    return sNumber + eNumber;
                }
                catch (Exception)
                {
                    SetNextEpisode();
                    return NextEpisodeNumber;
                }
            }
        }

        public string NextEpisodeImage
        {
            get
            {
                try
                {
                    return nextEpisode.ImageURI;
                }
                catch (Exception)
                {
                    SetNextEpisode();
                    return NextEpisodeImage;
                }
            }
        }

        public string NextEpisodeRoviID
        {
            get
            {
                try
                {
                    return nextEpisode.RoviID;
                }
                catch (Exception)
                {
                    SetNextEpisode();
                    return NextEpisodeRoviID;
                }
            }
        }

        public IEpisode GetEpisodeByRoviID(string roviID)
        {
            IEpisode result = null;
            foreach (IEpisode ep in episodes)
            {
                if (ep.RoviID == roviID)
                {
                    result =  ep;
                    break;
                }
            }
            return result;
        }

        public bool Equals(Series other)
        {
            return this.ID.Equals(other.ID);
        }
    }

}

