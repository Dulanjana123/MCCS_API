
namespace NovaLab.Models.Dto.Base
{
    /// <summary>
    /// Base of dto
    /// </summary>
    /// <typeparam name="TId">type of id</typeparam>
    public class DtoBase<TId>
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public TId Id { get; set; }

    }
}
