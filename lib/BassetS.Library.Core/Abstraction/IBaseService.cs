namespace BassetS.Library.Core.Abstraction
{
    using BassetS.Library.Core.Enum;
    /// <summary>
    /// Базовые методы сервисов
    /// </summary>
    public interface IBaseService{
        EnumServiceState GetState();
    }
}