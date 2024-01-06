using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Product:EntityBase
    {
        public Product()
        {
            Gallery = new Collection<Galleries>();
        }

        public string Title { get; set; } // Başlığı
        public string? SeoTitle { get; set; } // Seo Başlığı
        public string? SeoDescpription { get; set; } // Seo Açıklaması
        public string SeoUrl { get; set; } // Seo Url
        public string? Explanation { get; set; } // Açıklaması
        public decimal Price { get; set; } // Fiyatı
        public decimal? DiscountPrice { get; set; } // İndirimli Fiyatı
        public string FeaturedImage { get; set; } // Öne Çıkarılan Resim
        public bool Situation { get; set; } // Durum
        public int Arrangement { get; set; } // Sıralama
        public int Stock { get; set; } // Stok
        public ICollection<Colors> Colors { get; set; } = new HashSet<Colors>();
        public ICollection<Galleries> Gallery { get; set; }

    }
}
