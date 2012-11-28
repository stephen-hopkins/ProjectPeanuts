using Peanuts.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Peanuts
{
    public class SeriesSummary
    {
        private string title;
        private Uri image;
        private string synopsis;
        private string year;
        private string id;

        public SeriesSummary() { }

        public SeriesSummary(SeriesSummary copy) {
            this.title = copy.Title;
            this.image = copy.Image;
            this.synopsis = copy.Synopsis;
            this.year = copy.Year;
            this.id = copy.ID;
        }

        public SeriesSummary(string title, Uri image, string synopsis, string year, string id) {
            this.title = title;
            this.image = image;
            this.synopsis = synopsis;
            this.year = year;
            this.id = id;
        }

        public string Title { get { return title; } set { title = value; } }
        public Uri Image { get { return image; } set { image = value; } }
        public string Synopsis { get { return synopsis; } set { synopsis = value; } }
        public string Year { get { return year; } set { year = value; } }
        public string ID { get { return id; } set { id = value; } }
    }



    public class Series : SeriesSummary, IXmlSerializable
    {
  
        private List<Episode> episodes;
        private Episode nextEpisode;

        public string Channel { get; set; }

        public Series()
        {
            episodes = new List<Episode>();
        }

        public Series(SeriesSummary inputSS)
            : base(inputSS) {
            episodes = new List<Episode>();
        }



        public Episode NextEpisode
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

        public List<Episode> Episodes
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


        public void AddEpisode(Episode episode)
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

        public Episode GetEpisodeByRoviID(string roviID)
        {
            Episode result = null;
            foreach (Episode ep in episodes)
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

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteString(this.ID);
            writer.WriteString(this.Title);
            writer.WriteString(this.Year);
            writer.WriteString(this.Synopsis);
            //writer.WriteString(this.Image.ToString());
        }
    }

}

