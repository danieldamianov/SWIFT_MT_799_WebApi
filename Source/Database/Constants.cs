using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal static class Constants
    {
        internal static string FILE_PATH = "Database.sqlite";

        internal static string CREATE_TABLE_COMMAND =
    "CREATE TABLE IF NOT EXISTS SWIFT_MT799_Messages (" +
    "ApplicationID TEXT, " +
    "ServiceID TEXT, " +
    "SenderBankCode TEXT, " +
    "SenderCountryCode TEXT, " +
    "SenderLocationCode TEXT, " +
    "SenderLogicalTerminal TEXT, " +
    "SenderSessionNumber TEXT, " +
    "SenderSequenceNumber TEXT, " +
    "InputTime TEXT, " +
    "ReceiverBankCode TEXT, " +
    "ReceiverCountryCode TEXT, " +
    "ReceiverLocationCode TEXT, " +
    "ReceiverLogicalTerminal TEXT, " +
    "ReceiverBranchCode TEXT, " +
    "MessageInputReference TEXT NOT NULL, " +
    "MessagePriority TEXT, " +
    "TransactionReferenceNumber TEXT NOT NULL, " +
    "RelatedReference TEXT, " +
    "NarrativeText TEXT, " +
    "MessageAuthenticationCode TEXT, " +
    "Checksum TEXT, " +
    "PRIMARY KEY (TransactionReferenceNumber, MessageInputReference)" +
    ");";

        internal static string INSERT_MESSAGE_COMMAND = @"
                    INSERT INTO SWIFT_MT799_Messages (
                        ApplicationID, ServiceID, SenderBankCode, SenderCountryCode, SenderLocationCode, 
                        SenderLogicalTerminal, SenderSessionNumber, SenderSequenceNumber, InputTime, 
                        ReceiverBankCode, ReceiverCountryCode, ReceiverLocationCode, ReceiverLogicalTerminal, 
                        ReceiverBranchCode, MessageInputReference, MessagePriority, TransactionReferenceNumber, 
                        RelatedReference, NarrativeText, MessageAuthenticationCode, Checksum
                    ) VALUES (
                        @ApplicationID, @ServiceID, @SenderBankCode, @SenderCountryCode, @SenderLocationCode, 
                        @SenderLogicalTerminal, @SenderSessionNumber, @SenderSequenceNumber, @InputTime, 
                        @ReceiverBankCode, @ReceiverCountryCode, @ReceiverLocationCode, @ReceiverLogicalTerminal, 
                        @ReceiverBranchCode, @MessageInputReference, @MessagePriority, @TransactionReferenceNumber, 
                        @RelatedReference, @NarrativeText, @MessageAuthenticationCode, @Checksum
                    )";

        internal static string GET_MESSAGES_BY_SENDER_COMMAND = @"
                    SELECT ApplicationID, ServiceID, SenderBankCode, SenderCountryCode, SenderLocationCode,
                           SenderLogicalTerminal, SenderSessionNumber, SenderSequenceNumber, InputTime,
                           ReceiverBankCode, ReceiverCountryCode, ReceiverLocationCode, ReceiverLogicalTerminal,
                           ReceiverBranchCode, MessageInputReference, MessagePriority, TransactionReferenceNumber,
                           RelatedReference, NarrativeText, MessageAuthenticationCode, Checksum
                    FROM SWIFT_MT799_Messages
                    WHERE SenderBankCode = @SenderBankCode";
    }
}
