using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using URLShortenerApi.Helpers;
using URLShortenerTests.Utils;
using URLShortner.Code;
using URLShortner.Dtos;

namespace URLShortenerTests.URLShortenerApiTests
{
    [TestClass]
    public class URLHelperTests
    {
        private IConfiguration _config;
        private IHttpClientFactory _clientFactory;
        private URLHelper _helper;

        [TestInitialize]
        public void init()
        {

            var fakeValues = new Dictionary<string, string>
            {
                {"IntToChar", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"},
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(fakeValues)
                .Build();
            _clientFactory = Substitute.For<IHttpClientFactory>();

            _helper = new URLHelper( _config, _clientFactory);
        }
        #region OrignalUrl
        [TestMethod]
        public async Task Encode_Success()
        {
            int key = 1000005;

            var response = await _helper.Encode(key);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, "hfQT");
        }
        [TestMethod]
        public async Task Decode_Success()
        {
            string hash = "hfQT";

            var response = await _helper.Decode(hash);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, 1000005);
        }
        [TestMethod]
        public async Task GetTitle_Success()
        {
            string Url = "https://github.com";
            var MessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            MessageHandler.Protected()
           .Setup<Task<HttpResponseMessage>>("SendAsync",
                                             ItExpr.IsAny<HttpRequestMessage>(),
                                             ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent("Github")
           })
           .Verifiable();
            var httpClient = new HttpClient(MessageHandler.Object)
            {
                BaseAddress = new Uri("https://textance.herokuapp.com/title/https://github.com"),
            };


            _clientFactory.CreateClient().Returns(httpClient);

            var response = await _helper.GetTitle(Url);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, "Github");
        }

        #endregion
        #region Private Methods

        #endregion
    }
}

