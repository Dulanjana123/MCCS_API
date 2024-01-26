using NovaLab.Models.Entities.BaseEntities.Abstracts;

namespace NovaLab.Models.Entities.BaseEntities
{
    /// <summary>
    /// Entity base
    /// </summary>
    /// <typeparam name="T">id type</typeparam>
    public abstract class EntityBase<T> : IEntityBase<T>
    {
        public T Id { get; set; }
    }
}
