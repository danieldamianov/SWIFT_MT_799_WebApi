using ApplicationLogic.Interfaces;
using ApplicationLogic.Models;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SWIFT_MT799_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands
{
    public class SaveMessageCommand : IRequest<bool>
    {
        private readonly string message;

        public SaveMessageCommand(string message)
        {
            this.message = message;
        }

        public class SaveMessageCommandHandler : IRequestHandler<SaveMessageCommand, bool>
        {
            private readonly ISWIFT_MT799_WebApiDataProvider dataProvider;
            private readonly ISwiftMT799Parser parser;
            private readonly IMapper mapper;
            private readonly ILogger<SaveMessageCommandHandler> logger;

            public SaveMessageCommandHandler(ISWIFT_MT799_WebApiDataProvider dataProvider,
                ISwiftMT799Parser parser,
                IMapper mapper,
                ILogger<SaveMessageCommandHandler> logger)
            {
                this.dataProvider = dataProvider;
                this.parser = parser;
                this.mapper = mapper;
                this.logger = logger;
            }

            public async Task<bool> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
            {
                SwiftMT799Message message;
                try
                {
                    message = this.parser.ParseSwiftMT799Message(request.message);
                }
                catch (ArgumentException)
                {
                    this.logger.LogError($"Message :"
                        + request.message + " is not a valid SWIFTMT_799 Message! ");
                    return false;
                }

                this.logger.LogInformation($"Message:" +
                    $" TransactionReferenceNumber - \"{message.TransactionReferenceNumber}\" " +
                    $" MessageInputReference - \"{message.MessageInputReference}\" " +
                    $"successfully parsed.");

                bool operationResult = await dataProvider.SaveMessageAsync(this.mapper
                    .Map<SWIFT_MT799_Message_Model>(message));

                return operationResult;
            }
        }
    }
}
