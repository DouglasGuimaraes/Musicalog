using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Models.Entities;
using Models.Enum;
using Models.Interfaces.Service;
using Models.ViewModel;
using Repository.Repositories;

namespace Services
{
    public class AlbumService : IAlbumService
    {
        private readonly AlbumRepository _albumRepository;
        private readonly IMapper _mapper;


        public AlbumService(AlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        public async Task<AlbumViewModel> Add(AlbumViewModel albumViewModel)
        {
            if (IsValidAlbum(albumViewModel, isCreateAction: true))
            {
                var entity = GetAlbumMapper(albumViewModel);
                entity.SetLastUpdate();
                var addedEntity = await _albumRepository.Add(entity);
                var viewModelResult = GetAlbumViewModelMapper(addedEntity);
                return viewModelResult;
            }

            return null;
        }

        public async Task<AlbumViewModel> Delete(int id)
        {
            var deletedentity = await _albumRepository.Delete(id);
            var viewModel = GetAlbumViewModelMapper(deletedentity);
            return viewModel;
        }

        public async Task<AlbumViewModel> Get(int id)
        {
            var entity = await _albumRepository.Get(id);
            var viewModel = GetAlbumViewModelMapper(entity);
            return viewModel;
        }

        public async Task<List<AlbumViewModel>> GetAll()
            => GetListAlbumViewModelMapper(await _albumRepository.GetAll());

        public async Task<List<AlbumViewModel>> GetByFilter(string title, string artistName)
        {
            bool filterTitle = !string.IsNullOrEmpty(title);
            bool filterArtist = !string.IsNullOrEmpty(artistName);

            var albumList = await _albumRepository.GetByFilter(title, artistName);
            var viewModelList = GetListAlbumViewModelMapper(albumList);
            return viewModelList;
        }

        public async Task<AlbumViewModel> Update(AlbumViewModel albumViewModel)
        {
            if (IsValidAlbum(albumViewModel))
            {
                var toUpdateEntity = GetAlbumMapper(albumViewModel);
                toUpdateEntity.SetLastUpdate();
                var udatedEntity = await _albumRepository.Update(toUpdateEntity);
                var viewModelResult = GetAlbumViewModelMapper(udatedEntity);
                return viewModelResult;
            }
                
            return null;
        }

        #region Private Methods

        private bool IsValidAlbum(AlbumViewModel album, bool isCreateAction = false)
        {
            bool isValidAlbumType = (int)album.AlbumType == 0 || (int)album.AlbumType == 1;
            bool isValidTitle = !string.IsNullOrEmpty(album.Title);
            bool isValidArtistName = !string.IsNullOrEmpty(album.ArtistName);
            bool isValidStock = album.Stock >= 0;
            bool albumDoesNotExists = true;
            if(isCreateAction)
            {
                var albums = _albumRepository.GetByFilter(album.Title, album.ArtistName).Result;
                albumDoesNotExists = !albums.Any();
            }
            return isValidAlbumType && isValidTitle && isValidArtistName && isValidStock && albumDoesNotExists;
        }

        private Album GetAlbumMapper(AlbumViewModel viewModel) 
            => _mapper.Map<Album>(viewModel);

        private AlbumViewModel GetAlbumViewModelMapper(Album album)
            => _mapper.Map<AlbumViewModel>(album);

        private List<AlbumViewModel> GetListAlbumViewModelMapper(List<Album> albumList)
            => _mapper.Map<List<AlbumViewModel>>(albumList);

        #endregion
    }
}
