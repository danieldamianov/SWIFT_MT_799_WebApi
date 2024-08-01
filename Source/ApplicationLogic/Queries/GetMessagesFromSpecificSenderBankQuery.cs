using ApplicationLogic.Interfaces;
using ApplicationLogic.Models;
using AutoMapper;
using MediatR;
using SWIFT_MT799_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Queries
{

    public class GetMessagesFromSpecificSenderBankQuery : IRequest<ICollection<SWIFT_MT799_Message_Model>>
    {
        private readonly string senderBankCode;

        public GetMessagesFromSpecificSenderBankQuery(string senderBankCode)
        {
            this.senderBankCode = senderBankCode;
        }

        // TODO:: add base class for commands and queries handlers that injects the dataProvider
        public class GetMessagesFromSpecificSenderBankQueryHandler
            : IRequestHandler<GetMessagesFromSpecificSenderBankQuery,
                ICollection<SWIFT_MT799_Message_Model>>
        {
            private readonly ISWIFT_MT799_WebApiDataProvider dataProvider;

            public GetMessagesFromSpecificSenderBankQueryHandler
                (ISWIFT_MT799_WebApiDataProvider dataProvider)
            {
                this.dataProvider = dataProvider;
            }

            public async Task<ICollection<SWIFT_MT799_Message_Model>> Handle
                (GetMessagesFromSpecificSenderBankQuery query, CancellationToken cancellationToken)
            {
                ICollection<SWIFT_MT799_Message_Model> result = await
                    this.dataProvider.GetMessagesFromSpecificSenderBankAsync(query.senderBankCode);

                return result;
            }
        }
    }
}
