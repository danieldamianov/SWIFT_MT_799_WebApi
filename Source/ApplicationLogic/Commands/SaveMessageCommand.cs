using ApplicationLogic.Interfaces;
using ApplicationLogic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands
{
    public class SaveMessageCommand : IRequest<bool>
    {
        private readonly SWIFT_MT799_Message_Model message;

        public SaveMessageCommand(SWIFT_MT799_Message_Model message)
        {
            this.message = message;
        }

        public class SaveMessageCommandHandler : IRequestHandler<SaveMessageCommand, bool>
        {
            private readonly ISWIFT_MT799_WebApiDataProvider dataProvider;

            public SaveMessageCommandHandler(ISWIFT_MT799_WebApiDataProvider dataProvider)
            {
                this.dataProvider = dataProvider;
            }

            public async Task<bool> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
            {
                await dataProvider.SaveMessageToDatabase(request.message); 

                return true;
            }
        }
    }
}
