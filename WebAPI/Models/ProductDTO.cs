using NovaLab.Models.Dto.Base;

namespace WebAPI.Models
{
    public class ProductDTO : DtoBase<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
