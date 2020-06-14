using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WandaWebAdmin.Models.ViewModels
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        public long Code { get; set; }
        public string Title { get; set; }
        public List<VideoViewModel> Videos { get; set; } = new List<VideoViewModel>();
    }
}
