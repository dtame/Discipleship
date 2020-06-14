using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WandaWebAdmin.Helpers
{
    public interface IVimeoHelper
    {
        Task<VimeoDotNet.Models.Paginated<VimeoDotNet.Models.Album>> GetAlbums();
        Task<VimeoDotNet.Models.Paginated<VimeoDotNet.Models.Video>> GetVideos();
        VimeoDotNet.Models.Paginated<VimeoDotNet.Models.Video> GetVideosByAlbum(long albumId);
    }
}
