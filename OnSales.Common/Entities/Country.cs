using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSales.Common.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Estate> Estates { get; set; }

        public int EstatesNumber => Estates == null ? 0 : Estates.Count;

    }
}
