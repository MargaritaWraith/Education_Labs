using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Entityes.EF.BaseEntityes.Interfaces
{
    /// <summary>
    /// Интерфейс - Сущность базы данных
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Первичный ключ id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int Id { get; set; }
    }
}
