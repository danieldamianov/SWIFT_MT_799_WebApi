using ApplicationLogic.Interfaces;
using ApplicationLogic.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Database
{
    public class SWIFT_MT799_WebApi_SQLiteDataProvider : ISWIFT_MT799_WebApiDataProvider
    {
        private readonly ILogger<SWIFT_MT799_WebApi_SQLiteDataProvider> logger;

        public SWIFT_MT799_WebApi_SQLiteDataProvider(ILogger<SWIFT_MT799_WebApi_SQLiteDataProvider> logger)
        {
            this.logger = logger;
        }

        public bool EnsureDataStorageExists()
        {
            if (!File.Exists(Constants.FILE_PATH))
            {
                using (var connection = new SqliteConnection($"Data Source={Constants.FILE_PATH}"))
                {
                    connection.Open();

                    var createTable = new SqliteCommand(Constants.CREATE_TABLE_COMMAND, connection);
                    createTable.ExecuteNonQuery();
                }
            }

            return true;
        }

        public async Task<ICollection<SWIFT_MT799_Message_Model>> GetMessagesFromSpecificSenderBankAsync(string senderBankCode)
        {

            var result = new List<SWIFT_MT799_Message_Model>();

            using (var connection = new SqliteConnection($"Data Source={Constants.FILE_PATH}"))
            {
                await connection.OpenAsync();

                var queryCommand = connection.CreateCommand();
                queryCommand.CommandText = Constants.GET_MESSAGES_BY_SENDER_COMMAND;

                queryCommand.Parameters.AddWithValue("@SenderBankCode", senderBankCode);

                using (var reader = await queryCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var message = new SWIFT_MT799_Message_Model();

                        foreach (PropertyInfo property in typeof(SWIFT_MT799_Message_Model).GetProperties())
                        {
                            var columnName = property.Name;
                            var value = reader[columnName];
                            property.SetValue(message, Convert.ChangeType(value, property.PropertyType));
                        }

                        result.Add(message);
                    }
                }
            }

            this.logger.LogInformation($"Successfully retrieved {result.Count} messages " +
                $"with sender bank {senderBankCode} from the database");

            return result;
        }

        public async Task<bool> SaveMessageAsync(SWIFT_MT799_Message_Model message)
        {
            using (var connection = new SqliteConnection($"Data Source={Constants.FILE_PATH}"))
            {
                await connection.OpenAsync();

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = Constants.INSERT_MESSAGE_COMMAND;

                foreach (PropertyInfo property in typeof(SWIFT_MT799_Message_Model).GetProperties())
                {
                    string? value = (string?)property.GetValue(message);
                    insertCommand.Parameters.AddWithValue($"@{property.Name}", value);
                }

                int rowsAffected;
                
                try
                {
                    rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                }
                catch (Microsoft.Data.Sqlite.SqliteException)
                {
                    LogUnsuccessfulMessageSaving(message);

                    return false;
                }

                if (rowsAffected > 0)
                {
                    this.logger.LogInformation($"Message:" +
                    $" TransactionReferenceNumber - \"{message.TransactionReferenceNumber}\" " +
                    $" MessageInputReference - \"{message.MessageInputReference}\" " +
                    $"successfully saved to database.");

                    return true;
                }

                LogUnsuccessfulMessageSaving(message);

                return false;
            }
        }

        private void LogUnsuccessfulMessageSaving(SWIFT_MT799_Message_Model message)
        {
            this.logger.LogError($"An error occurred while executing" +
                                $" the sql query for saving message:" +
                                $" TransactionReferenceNumber - \"{message.TransactionReferenceNumber}\" " +
                                $" MessageInputReference - \"{message.MessageInputReference}\" ");
        }
    }
}
