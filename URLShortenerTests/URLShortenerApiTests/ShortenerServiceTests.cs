using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using URLShortenerApi.Helpers;
using URLShortenerTests.Utils;
using URLShortner.Code;
using URLShortner.Dtos;

namespace URLShortenerTests
{
    [TestClass]
    public class ShortenerServiceTests
    {
        
        private  IConfiguration _config;
        private  IHttpClientFactory _clientFactory;
        private IURLHelper _helper;
        private ShortenerService _service;
        private UrlShortnerDbContext dbContext;
        [TestInitialize]
        public void init()
        {

            _config = Substitute.For<IConfiguration>();
            _clientFactory = Substitute.For<IHttpClientFactory>();
            _helper = Substitute.For<IURLHelper>();
            dbContext = new UrlShortnerDbContext(TestDbContext.GetOptions());
        }
        #region OrignalUrl
        [TestMethod]
        public async Task OrignalUrl_Success()
        {
            DeleteDatabase();
            using(var _dbContext = dbContext)
            {
                var TestData = GetTestData();
                foreach (var Data in TestData)
                    _dbContext.Urls.Add(Data);
                _dbContext.SaveChanges();
                _service = new ShortenerService(_dbContext, _config, _clientFactory, _helper);
                string input = "hfRa";
                _helper.Decode(Arg.Any<string>()).Returns(1000012);

                var resp = await _service.OrignalURL(input);

                Assert.IsNotNull(resp);
                Assert.IsNull(resp.Message);
                Assert.IsNotNull(resp.Url);
                Assert.AreEqual(resp.Url.ShortUrl, input);
            }
            
            
        }
        [TestMethod]
        public async Task OrignalUrl_InvalidInput()
        {
            DeleteDatabase();
            using (var _dbContext = dbContext)
            {
                var TestData = GetTestData();
                foreach (var Data in TestData)
                    _dbContext.Urls.Add(Data);
                _dbContext.SaveChanges();
                _service = new ShortenerService(_dbContext, _config, _clientFactory, _helper);

                string input = "a";
                _helper.Decode(Arg.Any<string>()).Returns(1000001);

                var resp = await _service.OrignalURL(input);

                Assert.IsNotNull(resp);
                Assert.IsNull(resp.Url);
                Assert.IsNotNull(resp.Message);
                Assert.AreEqual(resp.Message, "Invalid Input");
            }
            
        }
        #endregion
        #region ShortenUrl
        [TestMethod]
        public async Task ShortenUrl_Success()
        {
            DeleteDatabase();
            using (var _dbContext = dbContext)
            {
                var TestData = GetTestData();
                foreach (var Data in TestData)
                    _dbContext.Urls.Add(Data);
                _dbContext.SaveChanges();
                _service = new ShortenerService(_dbContext, _config, _clientFactory, _helper);

                var input = GetShortenUrlInput();
                _helper.Encode(Arg.Any<int>()).Returns("hfRd");
                _helper.GetTitle(Arg.Any<string>()).Returns("Gmail");

                var resp = await _service.ShortenURL(input);

                Assert.IsNotNull(resp);
                Assert.IsNull(resp.Message);
                Assert.IsNotNull(resp.Url);
                Assert.AreEqual(resp.Url.OrignalUrl, input.Url);
            }
            
        }
        #endregion
        #region GetUrlListById
        [TestMethod]
        public async Task GetUrlListById_Success()
        {
            DeleteDatabase();
            using (var _dbContext = dbContext)
            {
                var TestData = GetTestData();
                foreach (var Data in TestData)
                    _dbContext.Urls.Add(Data);
                _dbContext.SaveChanges();
                _service = new ShortenerService(_dbContext, _config, _clientFactory, _helper);

                var input = "9b495ebe-045f-494f-b813-17b363f4a859";

                var resp = await _service.GetUrlListById(input);

                Assert.IsNotNull(resp);
                Assert.IsNull(resp.Error);
                Assert.AreEqual(resp.UrlList.Count, 2);
                Assert.IsNotNull(resp.UrlList[0]);
                Assert.IsNotNull(resp.UrlList[1]);
            }
            
        }
        #endregion
        #region DeleteUrlById
        [TestMethod]
        public async Task DeleteUrlById_Success()
        {
            DeleteDatabase();
            using (var _dbContext = dbContext)
            {
                var TestData = GetTestData();
                foreach (var Data in TestData)
                    _dbContext.Urls.Add(Data);
                _dbContext.SaveChanges();
                _service = new ShortenerService(_dbContext, _config, _clientFactory, _helper);

                var input = GetDeleteUrlInput();

                var resp = await _service.DeleteUrlById(input);

                Assert.IsNotNull(resp);
                Assert.AreEqual(resp.Message, "Delete Success");
            }
            
        }
        [TestMethod]
        public async Task DeleteUrlById_URLNotFound()
        {
            DeleteDatabase();
            using (var _dbContext = dbContext)
            {
                var TestData = GetTestData();
                foreach (var Data in TestData)
                    _dbContext.Urls.Add(Data);
                _dbContext.SaveChanges();
                _service = new ShortenerService(_dbContext, _config, _clientFactory, _helper);

                var input = GetDeleteUrlInput();
                input.UrlId = 11;
                var resp = await _service.DeleteUrlById(input);

                Assert.IsNotNull(resp);
                Assert.AreEqual(resp.Message, "No url found to delete");
            }
            
        }
        #endregion
        #region Private Methods
        private static OrignalUrlResponse GetOrignalUrlResponse()
        {
            return new OrignalUrlResponse()
            {
                Url = new Url
                {
                    Id = 1,
                    ShortUrl = "hfRa",
                    OrignalUrl = "https://youtube.com",
                    Clicks = 1,
                    Title = "Youtube"
                }
            };
        }
        private static DeleteUrlInput GetDeleteUrlInput()
        {
            return new DeleteUrlInput()
            {
                UrlId = 2,
                UserId = "9b495ebe-045f-494f-b813-17b363f4a859"
            };
        }
        private static UrlInfo GetShortenUrlInput()
        {
            return new UrlInfo()
            {
                Url = "https://youtube.com",
                UserId = "9b495ebe-045f-494f-b813-17b363f4a859"
            };
        }
        private static List<UrlInfo> GetTestData()
        {
            return new List<UrlInfo>
            {
                new UrlInfo 
                { 
                    Id = 2, 
                    Key = 1000012, 
                    Url = "https://youtube.com", 
                    UrlHash = "hfRa", 
                    UserId = "9b495ebe-045f-494f-b813-17b363f4a859", 
                    Clicks = 0, 
                    Title = "Youtube" 
                },
                new UrlInfo 
                { 
                    Id = 4, 
                    Key = 1000014, 
                    Url = "https://google.com", 
                    UrlHash = "hfRc", 
                    UserId = "9b495ebe-045f-494f-b813-17b363f4a859", 
                    Clicks = 1, 
                    Title = "Gmail"
                }
            };
        }
        private void DeleteDatabase()
        {
            dbContext.Database.EnsureDeleted();
            return;
        }
        #endregion
    }
}
