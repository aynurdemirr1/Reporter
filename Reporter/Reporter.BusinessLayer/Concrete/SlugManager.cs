using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Reporter.BusinessLayer.Abstract;

namespace Reporter.BusinessLayer.Concrete
{
    public class SlugManager : ISlugService
    {
        public string GenerateSlug(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return Guid.NewGuid().ToString(); // Başlık boşsa benzersiz id döner

            string str = phrase.ToLowerInvariant();

            // Boşlukları tire yap
            str = Regex.Replace(str, @"\s+", "-");

            // Geçersiz karakterleri temizle (yalnızca harf, rakam ve tire)
            str = Regex.Replace(str, @"[^a-z0-9\-]", "");

            // Çoklu tireyi tek tireye indir ve baş/son tireyi kırp
            str = Regex.Replace(str, @"-+", "-").Trim('-');

            return str;
        }
    }         
}

