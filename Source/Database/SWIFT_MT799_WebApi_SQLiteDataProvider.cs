using ApplicationLogic.Interfaces;
using ApplicationLogic.Models;
using Microsoft.Data.Sqlite;
using System.Reflection;

namespace Database
{
    public class SWIFT_MT799_WebApi_SQLiteDataProvider : ISWIFT_MT799_WebApiDataProvider
    {
        public SWIFT_MT799_WebApi_SQLiteDataProvider()
        {
            //using (var connection = new SqliteConnection("Data Source=hello.db"))
            //{
            //    connection.Open();
            //
            //    var command = connection.CreateCommand();
            //    command.CommandText =
            //    @"
            //        CREATE TABLE user (
            //            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            //            name TEXT NOT NULL
            //        );
            //
            //        INSERT INTO user
            //        VALUES (1, 'Brice'),
            //               (2, 'Alexander'),
            //               (3, 'Nate');
            //    ";
            //    command.ExecuteNonQuery();
            //
            //    Console.Write("Name: ");
            //    var name = Console.ReadLine();
            //
            //    #region snippet_Parameter
            //    command.CommandText =
            //    @"
            //        INSERT INTO user (name)
            //        VALUES ($name)
            //    ";
            //    command.Parameters.AddWithValue("$name", name);
            //    #endregion
            //    command.ExecuteNonQuery();
            //
            //    command.CommandText =
            //    @"
            //        SELECT last_insert_rowid()
            //    ";
            //    var newId = (long)command.ExecuteScalar();
            //
            //    Console.WriteLine($"Your new user ID is {newId}.");
            //}
            //
            //Console.Write("User ID: ");
            //var id = int.Parse(Console.ReadLine());
            //
            //#region snippet_HelloWorld
            //using (var connection = new SqliteConnection("Data Source=hello.db"))
            //{
            //    connection.Open();
            //
            //    var command = connection.CreateCommand();
            //    command.CommandText =
            //    @"
            //        SELECT name
            //        FROM user
            //        WHERE id = $id
            //    ";
            //    command.Parameters.AddWithValue("$id", id);
            //
            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            var name = reader.GetString(0);
            //
            //            Console.WriteLine($"Hello, {name}!");
            //        }
            //    }
            //}
            //#endregion
            //
            //// Clean up
            //File.Delete("hello.db");
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
            // TODO: make it asychronous
            // TODO: implement correctly
            var result = new List<SWIFT_MT799_Message_Model>();

            using (var connection = new SqliteConnection($"Data Source={Constants.FILE_PATH}"))
            {
                await connection.OpenAsync();

                var queryCommand = connection.CreateCommand();
                queryCommand.CommandText = @"
                    SELECT ApplicationID, ServiceID, SenderBankCode, SenderCountryCode, SenderLocationCode,
                           SenderLogicalTerminal, SenderSessionNumber, SenderSequenceNumber, InputTime,
                           ReceiverBankCode, ReceiverCountryCode, ReceiverLocationCode, ReceiverLogicalTerminal,
                           ReceiverBranchCode, MessageInputReference, MessagePriority, TransactionReferenceNumber,
                           RelatedReference, NarrativeText, MessageAuthenticationCode, Checksum
                    FROM SWIFT_MT799_Messages
                    WHERE SenderBankCode = @SenderBankCode";

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

                var result = await insertCommand.ExecuteNonQueryAsync();

                return result > 0;
            }
        }
    }
}
