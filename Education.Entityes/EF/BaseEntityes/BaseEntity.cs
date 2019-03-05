using Education.Entityes.EF.BaseEntityes.Interfaces;

namespace Education.Entityes.EF.BaseEntityes
{
    /// <summary>
    /// Базовая сущность БД
    /// </summary>
    public abstract class BaseEntity: IBaseEntity
    {
        public int Id { get; set; }
    }
}