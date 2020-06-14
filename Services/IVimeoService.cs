using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WandaWebAdmin.Models.ViewModels;

namespace WandaWebAdmin.Services
{
    public interface IVimeoService
    {
        void SyncDataFromVimeo();
        List<AlbumViewModel> GetAlbumsWithVideos();
    }
}
