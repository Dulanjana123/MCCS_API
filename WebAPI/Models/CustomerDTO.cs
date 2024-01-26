using NovaLab.Models.Dto.Base;

namespace WebAPI.Models
{
    public class CustomerDTO : DtoBase<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
