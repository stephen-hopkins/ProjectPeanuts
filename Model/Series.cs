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
        private string roviID;
        private string cosmoID;

        public string Title { get { return title; } set { title = value; } }
        public Uri Image { get { return image; } set { image = value; } }
        public string Synopsis { get { return synopsis; } set { synopsis = value; } }
        public string Year { get { return year; } set { year = value; } }
        public string RoviID { get { return roviID; } set { roviID = value; } }
        public string CosmoID { get { return cosmoID; } }

        public SeriesSummary() { }

        public SeriesSummary(SeriesSummary copy) {
            this.title = copy.Title;
            this.image = copy.Image;
            this.synopsis = copy.Synopsis;
            this.year = copy.Year;
            this.roviID = copy.RoviID;
            this.cosmoID = copy.cosmoID;
        }

        public SeriesSummary(string title, Uri image, string synopsis, string year, string roviID, string cosmoID) {
            this.title = title;
            this.image = image;
            this.synopsis = synopsis;
            this.year = year;
            this.roviID = roviID;
            this.cosmoID = cosmoID;
        }


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

        public bool Equals(Series other)
        {
            return this.RoviID.Equals(other.RoviID);
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
            writer.WriteString(this.RoviID);
            writer.WriteString(this.Title);
            writer.WriteString(this.Year);
            writer.WriteString(this.Synopsis);
            //writer.WriteString(this.Image.ToString());
        }
    }

}

