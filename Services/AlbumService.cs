using System;
using System.Collections.Generic;
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
            if (IsValidAlbumType(albumViewModel.AlbumType))
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

            if (IsValidAlbumType(albumViewModel.AlbumType))
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

        private bool IsValidAlbumType(AlbumType albumType)
        {
            int albumTypeValue = (int)albumType;
            if (albumTypeValue == 0 || albumTypeValue == 1)
                return true;

            return false;
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
