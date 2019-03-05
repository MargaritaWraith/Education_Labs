using System.ComponentModel.DataAnnotations;

namespace Education.Entityes.EF.BaseEntityes.Interfaces
{
    /// <summary>
    /// Интерфейс - Именованная сущность
    /// </summary>
    public interface INamedEntity : IBaseEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        string Name { get; set; }
    }
}