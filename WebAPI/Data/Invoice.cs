﻿using NovaLab.Models.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models;

namespace WebAPI.Data
{
    //public class Invoice : EntityBase<int>
    //{
    //    public int CustomerId { get; set; }
    //    public int ProductId { get; set; }
    //    public int ProductQty { get; set; }
    //    public decimal Amount { get; set; }
    //    public decimal DiscountAmount { get; set; }
    //    public decimal TotalAmount { get; set; }
    //    public DateTime TransactionDate { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public int CreatedBy { get; set; }
    //    public virtual Customer Customer { get; set; }
    //    public virtual List<InvoiceProduct> Products { get; set; }
    //    public virtual List<Product> Product { get; set; }
    //}

    public class Invoice : EntityBase<int>
    {
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CreatedBy { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<InvoiceProduct> Products { get; set; }
    }
}
