using System.ComponentModel.DataAnnotations;
using Education.Entityes.EF.BaseEntityes.Interfaces;

namespace Education.Entityes.EF.BaseEntityes
{
    /// <summary>
    /// Именованная сущность БД
    /// </summary>
    public abstract class NamedEntity : BaseEntity, INamedEntity
    {
        [Display(Name = "Имя"), Required]
        public string Name { get; set; }
    }
}