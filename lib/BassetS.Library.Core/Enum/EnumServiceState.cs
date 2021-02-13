namespace BassetS.Library.Core.Enum{
    /// <summary>
    /// Статус работы сервиса 
    /// </summary>
    public enum EnumServiceState{
        /// <summary>
        /// Остановлен 
        /// </summary>
        Stopped,
        /// <summary>
        /// В работе 
        /// </summary>
        Runned,
        /// <summary>
        /// Остановлен из-за ошибки 
        /// </summary>
        ErrorStop
    }
}