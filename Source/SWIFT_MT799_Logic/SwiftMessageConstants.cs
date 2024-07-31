using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWIFT_MT799_Logic
{
    internal static class SwiftMessageConstants
    {
        internal const string SWIFT_MT799_REGEX =
            @"\s*\{1:(?<ApplicationID>[A-Z]{1})" +
            @"(?<ServiceID>\d{2})(?<SenderBankCode>[A-Z]{4})(?<SenderCountryCode>[A-Z]{2})" +
            @"(?<SenderLocationCode>[A-Z0-9]{2})(?<SenderLogicalTerminal>[A-Z0-9]{1})" +
            @"(?<SenderSessionNumber>[A-Z0-9]{3})(?<SenderSequenceNumber>\d{1,10})\}" +
            @"\s*\{2:O799(?<InputTime>\d{1,16})(?<ReceiverBankCode>[A-Z]{4})" +
            @"(?<ReceiverCountryCode>[A-Z]{2})(?<ReceiverLocationCode>[A-Z0-9]{2})" +
            @"(?<ReceiverLogicalTerminal>[A-Z0-9]{1})(?<ReceiverBranchCode>[A-Z0-9]{3})" +
            @"(?<MessageInputReference>\d{1,20})(?<MessagePriority>[NUR])\}\s*" +
            @"\{4:\s*:20:(?<TransactionReferenceNumber>[-\w]{1,16})\s*:21:" +
            @"(?<RelatedReference>[-\w]{1,16})\s*:79:(?<NarrativeText>(?s).*?)-\}\s*" +
            @"\{5:\{MAC:(?<MessageAuthenticationCode>[0-9A-F]{8})\}\{CHK:(?<Checksum>\d{1,12})\}\}\s*";

    }
}
