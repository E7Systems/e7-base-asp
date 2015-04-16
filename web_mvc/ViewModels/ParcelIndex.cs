using Rsff.BusinessLayer;
using web_mvc.Infrastructure;

namespace web_mvc.ViewModels
{
    public class ParcelIndex
    {
        public PagedData<Parcel> Parcels { get; set; }

        //Sort related
        public string SortOrder { get; set; }
        public string SortBy { get; set; }

        //Search Related
        public string APNSearchTerm { get; set; }
        public string StreetSearchTerm { get; set; }
        public string CitySearchTerm { get; set; }
        public string StateSearchTerm { get; set; }
        public string ZipSearchTerm { get; set; }
    }
}