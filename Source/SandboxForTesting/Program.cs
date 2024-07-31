using Microsoft.Data.Sqlite;
using System;
using System.Text.RegularExpressions;
using System.Reflection;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message = "{1:F01PRCBBGSFAXXX1111111111}{2:O7991111111111ABGRSWACAXXX11111111111111111111N}{4:\r\n:20:67-C111111-KNTRL \r\n:21:30-111-1111111\r\n:79:NA VNIMANIETO NA: OTDEL BANKOVI GARANTSII\r\n.\r\nOTNOSNO: POTVARJDENIE NA AVTENTICHNOST NA\r\n         PRIDRUJITELNO PISMO KAM ISKANE ZA\r\n         PLASHTANE PO BANKOVA GARANCIA\r\n.\r\nUVAJAEMI KOLEGI,\r\n.\r\nUVEDOMJAVAME VI, CHE IZPRASHTAME ISKANE ZA \r\nPLASHTANE NA STOYNOST BGN 3.100,00, PREDSTAVENO \r\nOT NASHIA KLIENT.\r\n.\r\nS NASTOYASHTOTO POTVARZHDAVAME AVTENTICHNOSTTA NA \r\nPODPISITE VARHU PISMOTO NI, I CHE TEZI LICA SA \r\nUPALNOMOSHTENI DA PODPISVAT TAKAV DOKUMENT OT \r\nIMETO NA BANKATA AD.\r\n.\r\nPOZDRAVI,\r\nTARGOVSKO FINANSIRANE\r\n-}{5:{MAC:00000000}{CHK:111111111111}}"; // Your SWIFT MT 799 message here
            SwiftMT799Message parsedMessage = SwiftMT799Message.Parse(message);

            // Use reflection to list all properties and their values
            foreach (PropertyInfo property in typeof(SwiftMT799Message).GetProperties())
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(parsedMessage)?.ToString() ?? "null";
                Console.WriteLine($"{propertyName}: {propertyValue}");
            }

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
    }




    public class SwiftMT799Message
    {
        // Block 1
        public string ApplicationID { get; set; }
        public string ServiceID { get; set; }
        public string SenderBankCode { get; set; }
        public string SenderCountryCode { get; set; }
        public string SenderLocationCode { get; set; }
        public string SenderLogicalTerminal { get; set; }
        public string SenderSessionNumber { get; set; }
        public string SenderSequenceNumber { get; set; }

        // Block 2
        public string InputTime { get; set; }
        public string ReceiverBankCode { get; set; }
        public string ReceiverCountryCode { get; set; }
        public string ReceiverLocationCode { get; set; }
        public string ReceiverLogicalTerminal { get; set; }
        public string ReceiverBranchCode { get; set; }
        public string MessageInputReference { get; set; }
        public string MessagePriority { get; set; }

        // Block 4
        public string TransactionReferenceNumber { get; set; }
        public string RelatedReference { get; set; }
        public string NarrativeText { get; set; }

        // Block 5
        public string MessageAuthenticationCode { get; set; }
        public string Checksum { get; set; }

        public static SwiftMT799Message Parse(string message)
        {
            string pattern = @"\s*\{1:(?<ApplicationID>[A-Z]{1})(?<ServiceID>\d{2})(?<SenderBankCode>[A-Z]{4})(?<SenderCountryCode>[A-Z]{2})(?<SenderLocationCode>[A-Z0-9]{2})(?<SenderLogicalTerminal>[A-Z0-9]{1})(?<SenderSessionNumber>[A-Z0-9]{3})(?<SenderSequenceNumber>\d{1,10})\}\s*\{2:O799(?<InputTime>\d{1,16})(?<ReceiverBankCode>[A-Z]{4})(?<ReceiverCountryCode>[A-Z]{2})(?<ReceiverLocationCode>[A-Z0-9]{2})(?<ReceiverLogicalTerminal>[A-Z0-9]{1})(?<ReceiverBranchCode>[A-Z0-9]{3})(?<MessageInputReference>\d{1,20})(?<MessagePriority>[NUR])\}\s*\{4:\s*:20:(?<TransactionReferenceNumber>[-\w]{1,16})\s*:21:(?<RelatedReference>[-\w]{1,16})\s*:79:(?<NarrativeText>(?s).*?)-\}\s*\{5:\{MAC:(?<MessageAuthenticationCode>[0-9A-F]{8})\}\{CHK:(?<Checksum>\d{1,12})\}\}\s*";

            Regex regex = new Regex(pattern);
            Match match = regex.Match(message);

            if (!match.Success)
            {
                throw new ArgumentException("The message does not match the expected format.");
            }

            SwiftMT799Message swiftMessage = new SwiftMT799Message();

            foreach (PropertyInfo property in typeof(SwiftMT799Message).GetProperties())
            {
                if (match.Groups[property.Name].Success)
                {
                    property.SetValue(swiftMessage, match.Groups[property.Name].Value);
                }
            }

            return swiftMessage;
        }
    }
}