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
        bool EnsureDataStorageExists();
        
        Task<bool> SaveMessageAsync(SWIFT_MT799_Message_Model message);

        Task<ICollection<SWIFT_MT799_Message_Model>> GetMessagesFromSpecificSenderBankAsync
            (string senderBankCode);
    }
}
