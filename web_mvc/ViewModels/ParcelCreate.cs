using Rsff.BusinessLayer;

namespace web_mvc.ViewModels
{
    public class ParcelCreate
    {
        public Parcel Parcel { get; set; }
        public ParcelCreate()
        {
            this.Parcel = new Parcel();
        }
    }
}