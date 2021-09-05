using Models.Enum;
using Models.Interfaces.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModel
{
    public class AlbumViewModel : IViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public AlbumType AlbumType { get; set; }
        public int Stock { get; set; }
    }
}
