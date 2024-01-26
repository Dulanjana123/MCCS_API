using NovaLab.Models.Dto.Base;

namespace WebAPI.Models
{
    public class InvoiceProductDTO : DtoBase<int>
    {
        //public int ProductId { get; set; }
        //public int InvoiceId { get; set; }
        //public int ProductQty { get; set; }
        //public decimal Amount { get; set; }
        //public decimal DiscountAmount { get; set; }
        //public decimal TotalAmount { get; set; }

        public int ProductId { get; set; }
        public int ProductQty { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int InvoiceId { get; set; }
        public virtual InvoiceDTO Invoice { get; set; }
        public virtual ProductDTO Product { get; set; }


    }

}
