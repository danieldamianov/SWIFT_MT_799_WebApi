﻿using ApplicationLogic.Interfaces;
using ApplicationLogic.Models;

namespace Database
{
    public class ISWIFT_MT799_WebApi_SQLiteDataProvider : ISWIFT_MT799_WebApiDataProvider
    {
        public ISWIFT_MT799_WebApi_SQLiteDataProvider()
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
            throw new NotImplementedException();
        }

        public ICollection<SWIFT_MT799_Message_Model> GetMessagesFromSpecificSenderBank(string senderBankCode)
        {
            throw new NotImplementedException();
        }

        public bool SaveMessageToDatabase(SWIFT_MT799_Message_Model message)
        {
            throw new NotImplementedException();
        }
    }
}