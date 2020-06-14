using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WandaWebAdmin.Models.ViewModels
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
}
