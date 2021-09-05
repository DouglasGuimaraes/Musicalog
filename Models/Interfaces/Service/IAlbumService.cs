using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;
using Models.Interfaces.Service.Template;
using Models.ViewModel;

namespace Models.Interfaces.Service
{
    public interface IAlbumService : IService<AlbumViewModel>
    {
        Task<List<AlbumViewModel>> GetByFilter(string title, string artist);
    }
}
