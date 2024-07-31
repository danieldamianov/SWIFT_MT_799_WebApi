using Microsoft.Data.Sqlite;
using System;
using System.Text.RegularExpressions;
using System.Reflection;
using SWIFT_MT799_Logic;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISwiftMT799Parser swiftMT799Parser = new SwiftMT799Parser();

            string message = "{1:F01PRCBBGSFAXXX1111111111}{2:O7991111111111ABGRSWACAXXX11111111111111111111N}{4:\r\n:20:67-C111111-KNTRL \r\n:21:30-111-1111111\r\n:79:NA VNIMANIETO NA: OTDEL BANKOVI GARANTSII\r\n.\r\nOTNOSNO: POTVARJDENIE NA AVTENTICHNOST NA\r\n         PRIDRUJITELNO PISMO KAM ISKANE ZA\r\n         PLASHTANE PO BANKOVA GARANCIA\r\n.\r\nUVAJAEMI KOLEGI,\r\n.\r\nUVEDOMJAVAME VI, CHE IZPRASHTAME ISKANE ZA \r\nPLASHTANE NA STOYNOST BGN 3.100,00, PREDSTAVENO \r\nOT NASHIA KLIENT.\r\n.\r\nS NASTOYASHTOTO POTVARZHDAVAME AVTENTICHNOSTTA NA \r\nPODPISITE VARHU PISMOTO NI, I CHE TEZI LICA SA \r\nUPALNOMOSHTENI DA PODPISVAT TAKAV DOKUMENT OT \r\nIMETO NA BANKATA AD.\r\n.\r\nPOZDRAVI,\r\nTARGOVSKO FINANSIRANE\r\n-}{5:{MAC:00000000}{CHK:111111111111}}"; // Your SWIFT MT 799 message here
            SwiftMT799Message parsedMessage = swiftMT799Parser.ParseSwiftMT799Message(message);

            // Use reflection to list all properties and their values
            foreach (PropertyInfo property in typeof(SwiftMT799Message).GetProperties())
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(parsedMessage)?.ToString() ?? "null";
                Console.WriteLine($"{propertyName}: {propertyValue}");
            }
        }
    }
}