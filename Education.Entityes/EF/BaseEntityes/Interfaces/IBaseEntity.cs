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
        int Id { get; set; }
    }
}
