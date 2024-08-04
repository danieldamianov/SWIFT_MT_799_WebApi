using ApplicationLogic.Interfaces;
using ApplicationLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWIFT_MT_799_Application_Tests
{
    internal class SWIFT_MT799_StubDataProvider : ISWIFT_MT799_WebApiDataProvider
    {
        private ICollection<SWIFT_MT799_Message_Model> messages;

        public SWIFT_MT799_StubDataProvider()
        {
            this.messages = new List<SWIFT_MT799_Message_Model>();
        }

        public bool EnsureDataStorageExists()
        {
            return true;
        }

        public async Task<ICollection<SWIFT_MT799_Message_Model>> GetMessagesFromSpecificSenderBankAsync(string senderBankCode)
        {
            ICollection<SWIFT_MT799_Message_Model> result = 
                this.messages.Where(m => m.SenderBankCode == senderBankCode).ToList();

            return await Task.FromResult(result);
        }

        public async Task<bool> SaveMessageAsync(SWIFT_MT799_Message_Model message)
        {
            this.messages.Add(message);

            return await Task.FromResult(true);
        }
    }
}
