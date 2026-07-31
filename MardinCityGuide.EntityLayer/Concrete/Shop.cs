using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Shops")]
    public class Shop // "Curated Shops": Artuklu Gümüş, Mardin Sa...
    {
        [PrimaryKey, AutoIncrement]
        public int ShopId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;
        
        [NotNull]
        public string ImageUrl { get; set; } = default!;
        public decimal? Rating { get; set; }           // 4.9
        public bool IsRecommended { get; set; }        // "4 RECOMMENDED"

        public int LocationId { get; set; }

        [Ignore]
        public Location Location { get; set; } = default!;

        public int? BazaarId { get; set; }             // hangi çarşıya bağlı

        [Ignore]
        public Bazaar? Bazaar { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
