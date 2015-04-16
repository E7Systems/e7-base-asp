using Rsff.BusinessLayer;

namespace web_mvc.ViewModels
{
    public class ParcelEdit
    {
        public Parcel Parcel { get; set; }
        public ParcelEdit()
        {
            this.Parcel = new Parcel();
        }
    }
}