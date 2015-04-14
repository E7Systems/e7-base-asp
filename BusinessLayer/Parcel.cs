
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rsff.BusinessLayer
{
    public class Parcel
    {
        [Key]
        public int ParcelID {get; set;}
        [Required, MaxLength(255)]
        public string APN {get; set;}
        [Required]
        public int OwnerPersonID {get; set;}
        [Required, MaxLength(255)]
        public string Street {get; set;}

        [Required, MaxLength(255)]
        public string City { get; set; }

        [Required, MaxLength(2), RegularExpression("[A-Z][A-Z]")]
        public string State { get; set; } //aliased as ParcelState in Data tier as State is a reserved keyword
        
        [Required, MaxLength(10), DataType(DataType.PostalCode)]
        public string Zip {get; set;}

        #region ToString
        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("ParcelID={0}, ", this.ParcelID);
            sb.AppendFormat("APN={0}, ", this.APN);
            sb.AppendFormat("OwnerPersonID={0}, ", this.OwnerPersonID);
            sb.AppendFormat("Street={0}, ", this.Street);
            sb.AppendFormat("City={0}, ", this.City);
            sb.AppendFormat("State={0}, ", this.State);
            sb.AppendFormat("Zip={0}, ", this.Zip);
            return sb.ToString();
        } 
        #endregion
    }
}
