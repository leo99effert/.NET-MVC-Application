using Microsoft.AspNetCore.Mvc.Rendering;

namespace NextHome.Models
{
    public class EstateFilter
    {
        public List<Estate>? Estates { get; set; }
        public SelectList? TypesOfEstates { get; set; }
        public string? SelectedType { get; set; }
        public string? SearchString { get; set; }
        public int? MinRooms { get; set; }
        public int? MinSize { get; set; }
        public int? MaxPrice { get; set; }
    }
}
