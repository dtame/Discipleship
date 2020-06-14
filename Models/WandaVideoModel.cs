using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WandaWebAdmin.Models
{
    public class Album
    {
        public Album()
        {
          //  Videos = new List<VideoModel>();
        }
        public int Id { get; set; }
        public long Code { get; set; }
        public string Title { get; set; }
        //public virtual List<VideoModel> Videos { get; set; }
    }

    public class VideoModel
    {
        private string _description;
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public string Description
        {
            get
            {
                return string.IsNullOrWhiteSpace(_description) ? string.Empty : _description;
            }
            set
            {
                _description = value;
            }
        }
        //public virtual Thumnail Thumbnail { get; set; }
    }

    public class Thumbnail
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string Link { get; set; }
        public int Width { get; set; }
        public int Heigth { get; set; }
    }
}
 