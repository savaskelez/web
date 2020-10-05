using arsiv.Models.EntityFramework;
using System.Collections.Generic;
namespace arsiv.ViewModels
{
    public class KitaplarFormViewModel
    {
        public IEnumerable<Turler> Turlers { get; set; }
        public IEnumerable<Yazarlar> Yazarlars { get; set; }
        public Kitaplar Kitaplar { get; set; }
    }
}