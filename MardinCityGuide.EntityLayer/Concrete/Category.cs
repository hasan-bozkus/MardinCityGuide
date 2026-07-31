using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int CategoryId { get; set; }

        [NotNull]
        public string CategoryName { get; set; } = string.Empty;

        public SectionType? SectionType { get; set; }
        public int SortOrder { get; set; }

        [Ignore]
        public ICollection<Bazaar>? Bazaars { get; set; }

        [Ignore]
        public ICollection<Place>? Places { get; set; }
    }
}
