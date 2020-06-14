using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VimeoDotNet;
using WandaWebAdmin.Models;

namespace WandaWebAdmin.Helpers
{
    public class VimeoHelper : IVimeoHelper
    {
        private readonly VimeoConfiguration _vimeoConfig;
        private readonly VimeoClient _vimeoClient;
        private VimeoDotNet.Models.User _user;
        public VimeoHelper(VimeoConfiguration config)
        {
            _vimeoConfig = config;
            _vimeoClient = new VimeoClient(_vimeoConfig.Token);
            
        }

        public async Task<VimeoDotNet.Models.Paginated<VimeoDotNet.Models.Album>> GetAlbums()
        {
            _user = _user ?? await _vimeoClient.GetAccountInformationAsync();
            return  await _vimeoClient.GetAlbumsAsync(_user.Id);
        }

        public async Task<VimeoDotNet.Models.Paginated<VimeoDotNet.Models.Video>> GetVideos()
        {
            _user = _user ?? await _vimeoClient.GetAccountInformationAsync();
            return await _vimeoClient.GetVideosAsync(_user.Id, 1, 50);
        }

        public VimeoDotNet.Models.Paginated<VimeoDotNet.Models.Video> GetVideosByAlbum(long albumId)
        {
            _user = _user ?? _vimeoClient.GetAccountInformationAsync().Result;
            var result =  _vimeoClient.GetAlbumVideosAsync(_user.Id, albumId, 1, 50).Result;
            return result;
        }
    }
}
