using ApplicationLogic.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using static ApplicationLogic.Commands.SaveMessageCommand;
using SWIFT_MT799_Logic;
using Moq;
using ApplicationLogic.Commands;
using ApplicationLogic.Queries;
using static ApplicationLogic.Queries.GetMessagesFromSpecificSenderBankQuery;
using ApplicationLogic.Models;

namespace SWIFT_MT_799_Application_Tests
{
    public class Tests
    {

        private ISWIFT_MT799_WebApiDataProvider dataProvider;
        private ISwiftMT799Parser parser;
        private IMapper mapper;
        private Mock<ILogger<SaveMessageCommandHandler>> logger;
        private SaveMessageCommandHandler handlerSaveMessage;
        private GetMessagesFromSpecificSenderBankQueryHandler handlerGetAllMessagesByBankCode;

        [SetUp]
        public void Setup()
        {
            this.dataProvider = new SWIFT_MT799_StubDataProvider();
            this.parser = new SwiftMT799Parser();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            mapper = config.CreateMapper();

            logger = new Mock<ILogger<SaveMessageCommandHandler>>();

            handlerSaveMessage
                = new SaveMessageCommandHandler(dataProvider, parser, mapper, logger.Object);

            handlerGetAllMessagesByBankCode =
                new GetMessagesFromSpecificSenderBankQueryHandler(dataProvider);
        }

        [Test]
        public async Task TestAddingMessagesAndRetrievingThem()
        {
            SaveMessageCommand saveMessageCommand1 = new SaveMessageCommand("{1:F01PRCBBGSFAXXX" +
                "6666666666}{2:O7996666666666ABGRSWACAXXX66666666666666666666N}{4:\r\n:20:67-C666666-K" +
                "NTRL \r\n:21:30-666-6666666\r\n:79:Testing message 5\r\n-}{5:{MAC:00000000}{CHK:666" +
                "666666666}}");

            SaveMessageCommand saveMessageCommand2 = new SaveMessageCommand("{1:F01PRCBBGSFAXXX123456789" +
                "0}{2:O7991234567890ABGRSWACAXXX12345678901234567890N}{4:\r\n:20:67-C123456-KNT" +
                "RL \r\n:21:30-123-1234567\r\n:79:Testing message 13\r\n-}{5:{MAC:00000000}{CHK:1234" +
                "56789012}}");
            SaveMessageCommand saveMessageCommand3 = new SaveMessageCommand("{1:F01JJJJBGSFA" +
                "XXX2222222222}{2:O7992222222222ABGRSWACAXXX22222222222222222222N}{4:\r\n:2" +
                "0:67-C222222-KNTRL \r\n:21:30-222-2222222\r\n:79:message from bank JJJJ nu" +
                "mber 1\r\n-}{5:{MAC:00000000}{CHK:222222222222}}");

            SaveMessageCommand saveMessageCommand4 = new SaveMessageCommand("{1:F01JJJJB" +
                "GSFAXXX4444444444}{2:O7994444444444ABGRSWACAXXX4444444444444444444" +
                "4N}{4:\r\n:20:67-C444444-KNTRL \r\n:21:30-444-4444444\r\n:79:message fro" +
                "m bank JJJJ number 3\r\n-}{5:{MAC:00000000}{CHK:444444444444}}");



            await handlerSaveMessage.Handle(saveMessageCommand1, CancellationToken.None);
            await handlerSaveMessage.Handle(saveMessageCommand2, CancellationToken.None);
            await handlerSaveMessage.Handle(saveMessageCommand3, CancellationToken.None);
            await handlerSaveMessage.Handle(saveMessageCommand4, CancellationToken.None);

            IList<string?>
                BankJJJJMessages = (await handlerGetAllMessagesByBankCode
                .Handle(new GetMessagesFromSpecificSenderBankQuery("JJJJ"), CancellationToken.None))
                .Select(m => m.NarrativeText)
                .OrderBy(s => s)
                .ToList();

            Assert.That(BankJJJJMessages[0], Is.EqualTo("message from bank JJJJ number 1\r\n"));
            Assert.That(BankJJJJMessages[1], Is.EqualTo("message from bank JJJJ number 3\r\n"));

            Assert.That(BankJJJJMessages.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task TestThatTheCommandReturnsFalseWhenGivenInvalidMessage()
        {
            var result = await handlerSaveMessage
                .Handle(new SaveMessageCommand("invalid"), CancellationToken.None);

            Assert.That(result, Is.False);
        }
    }
}