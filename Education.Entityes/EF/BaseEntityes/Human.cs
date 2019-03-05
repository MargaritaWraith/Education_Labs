using Education.Entityes.EF.BaseEntityes.Interfaces;

namespace Education.Entityes.EF.BaseEntityes
{
    /// <summary>
    /// Сущность - Человек
    /// </summary>
    public abstract class Human : NamedEntity, IHuman
    {
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}