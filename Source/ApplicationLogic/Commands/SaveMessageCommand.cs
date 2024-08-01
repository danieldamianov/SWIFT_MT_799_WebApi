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

            public SaveMessageCommandHandler(ISWIFT_MT799_WebApiDataProvider dataProvider,
                ISwiftMT799Parser parser,
                IMapper mapper)
            {
                this.dataProvider = dataProvider;
                this.parser = parser;
                this.mapper = mapper;
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
                    return false;
                }

                bool operationResult = await dataProvider.SaveMessageAsync(this.mapper
                    .Map<SWIFT_MT799_Message_Model>(this.parser.ParseSwiftMT799Message(request.message)));

                // TODO:: RETURN SOME IACTION RESULT
                return operationResult;
            }
        }
    }
}
