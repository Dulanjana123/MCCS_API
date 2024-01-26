using NovaLab.Models.Entities.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data
{
    public class Product : EntityBase<int>
    {
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public bool IsActive { get; set; }
        //public ICollection<Invoice> Invoice { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<InvoiceProduct> InvoiceProducts { get; set; }

    }
}
