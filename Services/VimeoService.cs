using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WandaWebAdmin.Data;
using WandaWebAdmin.Helpers;
using WandaWebAdmin.Models.ViewModels;
using WandaWebAdmin.Services.Contracts;

namespace WandaWebAdmin.Services
{
    public class VimeoService : IVimeoService
    {
        private IVimeoHelper _vimeoHelper;
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public VimeoService(IVimeoHelper vimeoHelper, ApplicationDbContext context, ILogger<VimeoService> logger)
        {
            _vimeoHelper = vimeoHelper;
            _context = context;
            _logger = logger;
        }

        public List<AlbumViewModel> GetAlbumsWithVideos()
        {
            try
            {
                List<AlbumViewModel> result = new List<AlbumViewModel>();
                var albums = _context.Albums.ToList();
                foreach (var album in albums)
                {
                    AlbumViewModel albumViewModel = new AlbumViewModel
                    {
                        Id = album.Id,
                        Code = album.Code,
                        Title = album.Title
                    };

                    //get videos
                    var videos = _context.VideoModels.Where(x => x.AlbumId == album.Id).ToList();
                    foreach (var video in videos)
                    {
                        var thumb = _context.Thumnails.First(x => x.VideoId == video.Id);
                        albumViewModel.Videos.Add(video.ToViewModel(thumb));
                    }
                    result.Add(albumViewModel);
                }
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<AlbumViewModel>();
            }
        }

        public void SyncDataFromVimeo()
        {
            //get the list of albums - showcase (this represent a set of videos like "Action")
            var albums = _vimeoHelper.GetAlbums().Result;
            List<VimeoDotNet.Models.Album> newAlbums = new List<VimeoDotNet.Models.Album>();

            if (albums != null && albums.Data.Count > 0)
            {
                //get new albums 
                foreach (var album in albums.Data)
                {
                    if (!_context.Albums.Any(x => x.Code == album.ToModel().Code))
                    {
                        newAlbums.Add(album);
                    }
                }

                //save new albums
                foreach (var album in newAlbums)
                {
                    var albumModel = album.ToModel();
                    _context.Albums.Add(albumModel);
                    _context.SaveChanges();

                    //get the list of videos of a specific album and save
                    var videos = _vimeoHelper.GetVideosByAlbum(albumModel.Code);
                    foreach (var video in videos.Data)
                    {
                        var videoModel = video.ToModel(albumModel.Id);
                        _context.VideoModels.Add(videoModel);
                        _context.SaveChanges();

                        //add thumbnail
                        var thumbnail = video.GetThumnail(videoModel.Id);
                        _context.Thumnails.Add(thumbnail);
                        _context.SaveChanges();
                    }
                }

                //delete albuns and artefacts in db deleted in vimeo
                var albumsToDelete = _context.Albums.Where(x => !albums.Data.Select(c => c.GetAlbumId()).Contains(x.Code)).ToList();
                if (albumsToDelete != null && albumsToDelete.Count > 0)
                {
                    foreach (var album in albumsToDelete)
                    {
                        var videos = _context.VideoModels.Where(x => x.AlbumId == album.Id).ToList();
                        var thumbnails = _context.Thumnails.Where(x => videos.Select(c => c.Id).Contains(x.VideoId)).ToList();

                        _context.Thumnails.RemoveRange(thumbnails);
                        _context.SaveChanges();

                        _context.VideoModels.RemoveRange(videos);
                        _context.SaveChanges();
                    }
                    _context.Albums.RemoveRange(albumsToDelete);
                    _context.SaveChanges();
                }

                // update existing albums
                var albumsToUpdate = albums.Data.Where(x => _context.Albums.Select(c => c.Code).ToList()
                                                .Contains(GetAlbumId(x.Uri)))
                                                .ToList();
                if (albumsToUpdate != null && albumsToUpdate.Count > 0)
                {
                    foreach (var album in albumsToUpdate)
                    {
                        //update album
                        var alb = _context.Albums.First(x => x.Code == GetAlbumId(album.Uri));
                        alb.Title = album.Name;
                        _context.Albums.Update(alb);
                        _context.SaveChanges();

                        //update videos
                        var videos = _vimeoHelper.GetVideosByAlbum(alb.Code);
                        foreach (var video in videos.Data)
                        {
                            if (_context.VideoModels.Any(x => x.Code == video.Id))
                            {
                                var dbVideo = _context.VideoModels.First(x => x.Code == video.Id);

                                //update thumbnail
                                var thumbnail = _context.Thumnails.First(x => x.VideoId == dbVideo.Id);
                                thumbnail.Heigth = video.Pictures.Sizes.First().Height;
                                thumbnail.Width = video.Pictures.Sizes.First().Width;
                                thumbnail.Link = video.Pictures.Sizes.First().Link;
                                //update video                        
                                dbVideo.Name = video.Name;
                                dbVideo.Description = video.Description;
                                _context.VideoModels.Update(dbVideo);
                            }
                            else
                            {
                                //add new video in existing album
                                var vid = video.ToModel(alb.Id);
                                _context.VideoModels.Add(vid);
                                _context.SaveChanges();

                                //save thumbnail
                                var thumbnail = video.GetThumnail(vid.Id);
                                _context.Thumnails.Add(thumbnail);
                                _context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        private long GetAlbumId(string uri)
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
