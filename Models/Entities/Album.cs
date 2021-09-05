using Models.Enum;
using Models.Interfaces.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Entities
{
    public partial class Album : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public AlbumType AlbumType { get; set; }
        public int Stock { get; set; }
        public DateTime LastUpdate { get; set; }

        public void SetLastUpdate()
        {
            this.LastUpdate = DateTime.Now;
        }
    }
}
