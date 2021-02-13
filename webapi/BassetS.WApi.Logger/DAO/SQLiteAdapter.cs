namespace BassetS.WApi.Logger.DAO{
    
    using System;
    using System.Threading.Tasks;
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;
    using BassetS.WApi.Logger.Models;
    using System.Globalization;
    /// <summary>
    /// Класс доступа к базе данных SQLite
    /// </summary>
    public class SQLiteAdapter: IDisposable{
        private readonly SqliteConnection connection;
        private CurrentDate current = new CurrentDate();
        public SQLiteAdapter(IConfiguration config){
            string connectionString = $"Data Source={config[General.ConfigPath+"general:dataPath"]}nodeLog.sqlitedb";
            connection = new SqliteConnection(connectionString);
            connection.Open();
            CreateTablesAsync().GetAwaiter().GetResult();
        }
        
        public async Task CreateTablesAsync(){
            var command = connection.CreateCommand();
            string sql ="CREATE TABLE IF NOT EXISTS "+"log"+DateTime.Now.ToString("yyyyMMdd")+
                        " ( id INTEGER PRIMARY KEY, " +
                        "   level INTEGER NOT NULL DEFAULT 0, " +
                        "   dt TEXT NOT NULL, " +
                        "   source TEXT NOT NULL, " +
                        "   message TEXT NOT NULL, "+
                        "   area TEXT);";
            command.CommandText = sql;
            await command.ExecuteNonQueryAsync();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public async Task SaveMessageAsync(MessageLogDto message){
            if (message.Source==null)throw new ArgumentNullException("src");
            if (message.MessArea==null)throw new ArgumentNullException("area");
            if (message.MessArea==null)throw new ArgumentNullException("msg");
            CheckTable();
            var command = connection.CreateCommand();
            command.CommandText ="INSERT INTO "+GetCurrentTable()+" (dt, level, source, message, area) "+
            $"VALUES ($p1, 0, $p3, '{message.Message}', '{message.MessArea}')";
            command.Parameters.AddWithValue("p1",message.DT.ToString("yyyy-MM-dd HH:mm:ss.ms"));
            command.Parameters.AddWithValue("p2",0);
            command.Parameters.AddWithValue("p3",message.Source);
            command.Parameters.AddWithValue("p4",message.Message);
            command.Parameters.AddWithValue("p5",message.MessArea);
            Console.WriteLine(command.CommandText);
            await command.ExecuteNonQueryAsync();
        }

#region private
        /// <summary>
        /// Создает таблицу для нового дня, если это необходимо
        /// </summary>
        private void CheckTable(){
            lock(current){
                if ( current.DT != DateTime.Now.Date){
                    CreateTablesAsync().GetAwaiter().GetResult();
                    current.DT = DateTime.Now;
                }
            }
        }
        /// <summary>
        /// Возвращает название текущей таблицы
        /// </summary>
        /// <returns>Имя текущей таблицы</returns>
        private string GetCurrentTable(){
            return "log"+current.DT.ToString("yyyyMMdd");
        }
#endregion
    }

    class CurrentDate{
        public DateTime DT{get;set;}
        public CurrentDate(){
            DT = DateTime.Now.Date;
        }
    }
}