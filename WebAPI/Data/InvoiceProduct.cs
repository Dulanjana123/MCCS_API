using NovaLab.Models.Entities.BaseEntities;
using System.Collections.Generic;

namespace WebAPI.Data
{
    public class InvoiceProduct : EntityBase<int>
    {
        //public int ProductId { get; set; }
        //public int InvoiceId { get; set; }
        //public int ProductQty { get; set; }
        //public decimal Amount { get; set; }
        //public decimal DiscountAmount { get; set; }
        //public decimal TotalAmount { get; set; }
        //public ICollection<Invoice> Invoice { get; set; }

        public int ProductId { get; set; }
        public int ProductQty { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}
