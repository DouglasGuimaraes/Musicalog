using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository.Context;

namespace Repository.Repositories
{
    public class AlbumRepository : EFCoreRepository<Album, MusicalogContext>
    {
        private readonly MusicalogContext musicalogContext;
        public AlbumRepository(MusicalogContext context) : base(context)
        {
            musicalogContext = context;
        }

        public async Task<List<Album>> GetByFilter(string title, string artistName)
        {
            bool filterTitle = !string.IsNullOrEmpty(title);
            bool filterArtist = !string.IsNullOrEmpty(artistName);

            return await musicalogContext.Albums.Where(x =>
                        (!filterTitle || x.Title.ToLower().Contains(title.ToLower())) &&
                        (!filterArtist || x.ArtistName.ToLower().Contains(artistName.ToLower())))
                .ToListAsync();
        }
    }
}
