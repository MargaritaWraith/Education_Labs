namespace Education.Entityes.EF.BaseEntityes.Interfaces
{
    /// <summary>
    /// Сущность - человек
    /// </summary>
    public interface IHuman : INamedEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        string Surname { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        string Patronymic { get; set; }
    }
}