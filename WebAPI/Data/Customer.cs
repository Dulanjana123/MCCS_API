using NovaLab.Models.Entities.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data
{
    public class Customer : EntityBase<int>
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Invoice> Invoice { get; set; }

    }



    

    
}
