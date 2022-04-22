using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnSales.Common.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Estate> Estates { get; set; }

        public int EstatesNumber => Estates == null ? 0 : Estates.Count;

        public string UrlImage { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public string ImageFullPath => UrlImage == null ? "https://tiendacomercial.blob.core.windows.net/images/noimage.png" : $"https://tiendacomercial.blob.core.windows.net/flags/{UrlImage}";
    }
}
