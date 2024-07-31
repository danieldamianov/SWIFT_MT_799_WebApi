using ApplicationLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface ISWIFT_MT799_WebApiDataProvider
    {
        void SaveMessageToDatabase(SWIFT_MT799_Message_Model message);

        ICollection<SWIFT_MT799_Message_Model> GetMessagesFromSpecificSenderBank(string senderBankCode);
    }
}
