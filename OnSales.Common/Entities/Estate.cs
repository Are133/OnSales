using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnSales.Common.Entities
{
    public class Estate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Municipality> Municipalities { get; set; }

        public int NumberMunicipalities => Municipalities == null ? 0 : Municipalities.Count;

        [JsonIgnore]
        [NotMapped]
        public int IdCountry { get; set; }
    }
}
