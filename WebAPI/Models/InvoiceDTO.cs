using NovaLab.Models.Dto.Base;
using System;
using System.Collections.Generic;


namespace WebAPI.Models
{
    public class InvoiceDTO : CreateInvoiceDTO
    {

        //public int ProductId { get; set; }
        public int Id { get; set; }
        public CustomerDTO Customer { get; set; }
        //public ProductDTO Product { get; set; }
        public List<InvoiceProductDTO> Products { get; set; }
    }

    public class CreateInvoiceDTO 
    {
        //public int CustomerId { get; set; }
        ////public int ProductId { get; set; }
        //public List<InvoiceProductDTO> Products { get; set; }
        //public int ProductQty { get; set; }
        //public decimal Amount { get; set; }
        //public decimal DiscountAmount { get; set; }
        //public decimal TotalAmount { get; set; }
        //public DateTime TransactionDate { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int CreatedBy { get; set; }

        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CreatedBy { get; set; }
        public List<InvoiceProductDTO> Products { get; set; }
    }
}
