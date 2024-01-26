namespace NovaLab.Models.Entities.BaseEntities.Abstracts
{

    /// <summary>
    /// Entity base
    /// </summary>
    /// <typeparam name="T">id type</typeparam>
    public interface IEntityBase<T>
    {
        /// <summary>
        /// Gets or sets id
        /// </summary>
        T Id { get; set; }
    }
}