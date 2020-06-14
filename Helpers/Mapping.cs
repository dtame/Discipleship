using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WandaWebAdmin.Models.ViewModels;

namespace WandaWebAdmin.Helpers
{
    public static class Mapping 
    {
        public static WandaWebAdmin.Models.Album ToModel(this VimeoDotNet.Models.Album album)
        {
            return new WandaWebAdmin.Models.Album
            {
                Code = GetCode(album.Uri),
                Title = album.Name                
            };
        }

        public static WandaWebAdmin.Models.VideoModel ToModel(this VimeoDotNet.Models.Video video, int albumId)
        {
            return new Models.VideoModel
            {
                AlbumId = albumId,
                Code = (long)video.Id,
                Name = video.Name,
                Description = video.Description
            };
        }

        public static VideoViewModel ToViewModel(this WandaWebAdmin.Models.VideoModel video, WandaWebAdmin.Models.Thumbnail thumbnail)
        {
            return new VideoViewModel
            {
                Id = video.Id,
                Name = video.Name,
                Code = video.Code,
                Description = video.Description,
                AlbumId = video.AlbumId,
                Thumbnail = new Models.Thumbnail
                {
                    Id = thumbnail.Id,
                    VideoId = thumbnail.VideoId,
                    Link = thumbnail.Link,
                    Width = thumbnail.Width,
                    Heigth = thumbnail.Heigth
                }
            };
        }

        public static WandaWebAdmin.Models.Thumbnail GetThumnail(this VimeoDotNet.Models.Video video, int videoId)
        {
            var thumbnail = video.Pictures.Sizes.First(x => x.Width > 250);
            return new Models.Thumbnail
            {
                VideoId = videoId,  
                Link = thumbnail.Link,
                Heigth = thumbnail.Height,
                Width = thumbnail.Width
            };
        }
        
        public static string ToJson(this List<AlbumViewModel> albums)
        {
            return JsonConvert.SerializeObject(albums);
        }

        private static long GetCode(string uri)
        {
            long result = 0;
            if (!string.IsNullOrWhiteSpace(uri) && uri.Contains("/"))
            {

                int lastIndex = uri.LastIndexOf('/');
                string id = uri.Substring(lastIndex + 1);
                result = long.Parse(id);
            }
            return result;
        }
    }
}
