using System.ComponentModel.DataAnnotations;
using Education.Entityes.EF.BaseEntityes.Interfaces;

namespace Education.Entityes.EF.BaseEntityes
{
    /// <summary>
    /// Сущность - Человек
    /// </summary>
    public abstract class Human : NamedEntity, IHuman
    {
        [Display(Name = "Фамилия"), Required]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
    }
}