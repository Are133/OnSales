using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnSales.Common.Entities
{
    public class Municipality
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdEstate { get; set; }
    }
}
