using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Models
{
    public class SWIFT_MT799_Message_Model
    {
        // Block 1
        public string? ApplicationID { get; set; }
        public string? ServiceID { get; set; }
        public string? SenderBankCode { get; set; }
        public string? SenderCountryCode { get; set; }
        public string? SenderLocationCode { get; set; }
        public string? SenderLogicalTerminal { get; set; }
        public string? SenderSessionNumber { get; set; }
        public string? SenderSequenceNumber { get; set; }

        // Block 2   
        public string? InputTime { get; set; }
        public string? ReceiverBankCode { get; set; }
        public string? ReceiverCountryCode { get; set; }
        public string? ReceiverLocationCode { get; set; }
        public string? ReceiverLogicalTerminal { get; set; }
        public string? ReceiverBranchCode { get; set; }
        public string? MessageInputReference { get; set; }
        public string? MessagePriority { get; set; }

        // Block 4   
        public string? TransactionReferenceNumber { get; set; }
        public string? RelatedReference { get; set; }
        public string? NarrativeText { get; set; }

        // Block 5   
        public string? MessageAuthenticationCode { get; set; }
        public string? Checksum { get; set; }
    }
}
