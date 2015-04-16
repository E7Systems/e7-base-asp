using Rsff.BusinessLayer;

namespace web_mvc.ViewModels
{
    public class ParcelCreateOrEdit
    {
        public Parcel Parcel { get; set; }
        public ParcelCreateOrEdit()
        {
            this.Parcel = new Parcel();
        }
    }
}