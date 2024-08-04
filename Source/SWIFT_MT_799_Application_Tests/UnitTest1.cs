using ApplicationLogic.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using static ApplicationLogic.Commands.SaveMessageCommand;
using SWIFT_MT799_Logic;

namespace SWIFT_MT_799_Application_Tests
{
    public class Tests
    {

        private ISWIFT_MT799_WebApiDataProvider dataProvider;
        private ISwiftMT799Parser parser;
        private IMapper mapper;
        private ILogger<SaveMessageCommandHandler> logger;


        [SetUp]
        public void Setup()
        {
            this.dataProvider = new SWIFT_MT799_StubDataProvider();
            this.parser = new SwiftMT799Parser();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}