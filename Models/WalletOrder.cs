using System.ComponentModel.DataAnnotations.Schema;

namespace FBC.Models
{
    public partial class WalletOrder
    {
        public int WalletOrderId { get; set; }

        public string? BankAcountName { get; set; }

        public string? PaymentCode { get; set; }

        public string? Description { get; set; }

        public decimal? Amount { get; set; }
        public decimal? Credit { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual User User { get; set; } = null!;
    }
}
