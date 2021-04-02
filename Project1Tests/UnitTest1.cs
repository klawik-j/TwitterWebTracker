using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;
using Project1;
using Project1.Models;
using System.Text.Json;

namespace Project1Tests
{
    public class UnitTest1
    {
        private readonly int _param1;

        public UnitTest1()
        {
            _param1 = 1;
        }

        [Theory]
        [InlineData("POTUS")]
        [InlineData("AndrzejDuda")]
        [InlineData("LanaRhoades")]
        public void ValidUserUrl(string UserName)
        {
            string expecting = $"https://api.twitter.com/2/users/by/username/{ UserName }?user.fields=profile_image_url";
            string result = TwitterApiProcessor.makeUserUrl(UserName);
            Assert.Equal(expecting, result);
        }

        [Fact]
        public void InvalidUserUrl()
        {
            string InvalidUserName = " ";
            Assert.Throws<ArgumentException>(() => TwitterApiProcessor.makeUserUrl(InvalidUserName));
        }

        [Theory]
        [InlineData("1349149096909668363")]
        [InlineData("202086424")]
        [InlineData("715757297075625984")]
        public void ValidTwittUrl(string UserId)
        {
            string expecting = $"https://api.twitter.com/2/users/{ UserId }/tweets?tweet.fields=created_at,possibly_sensitive&max_results=15";
            string result = TwitterApiProcessor.makeTwittUrl(UserId);
            Assert.Equal(expecting, result);
        }

        [Fact]
        public void InvalidTwittUrl()
        {
            string InvalidUserId = " ";
            Assert.Throws<ArgumentException>(() => TwitterApiProcessor.makeTwittUrl(InvalidUserId));
        }

        [Fact]
        public void UserExistNoErrorTwitterUserSerializer()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string data = "{\"data\":{\"profile_image_url\":\"https://pbs.twimg.com/profile_images/1349837426626330628/CRMNXzQJ_normal.jpg\",\"name\":\"President Biden\",\"id\":\"1349149096909668363\",\"username\":\"POTUS\"}}";
            TwitterUser User = JsonSerializer.Deserialize<TwitterUser>(data, options);

            Assert.NotNull(User.Data.Id);
        }

        [Fact]
        public void UserDoesNotExistTwitterUserSerializerTest()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string data = "{\"errors\":[{\"value\":\"TwitterDec\",\"detail\":\"Could not find user with username: [TwitterDec].\",\"title\":\"Not Found Error\",\"resource_type\":\"user\",\"parameter\":\"username\",\"resource_id\":\"TwitterDec\",\"type\":\"https://api.twitter.com/2/problems/resource-not-found\"}]}";
            TwitterUser User = JsonSerializer.Deserialize<TwitterUser>(data, options);
            Assert.NotEmpty(User.Errors);
        }
        
        [Fact]
        public void UserHasTwittsTwitterTwittSerializerTest()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string data = "{\"data\":[{\"id\":\"1377428415716950023\",\"possibly_sensitive\":false,\"created_at\":\"2021-04-01T01:11:40.000Z\",\"text\":\"Two years ago, I began my campaign in Pittsburgh, saying I was running to rebuild the backbone of America.Today, I returned as president to lay out a vision for how we do that.We're going to rebuild the middle class - and this time, we're going to bring everyone along. https://t.co/hSNbI08mDF\"},{\"id\":\"1377402906157084681\",\"possibly_sensitive\":false,\"created_at\":\"2021-03-31T23:30:18.000Z\",\"text\":\"Let me be clear: We're going to ensure corporations finally pay their fair share. https://t.co/k5Kz1NaLdi\"},{\"id\":\"1377389132775743488\",\"possibly_sensitive\":false,\"created_at\":\"2021-03-31T22:35:34.000Z\",\"text\":\"The American Jobs Plan will create millions of good paying jobs, grow our economy, make us more competitive, promote our national security interests, and put us in a position to win the global competition with China. https://t.co/yaVX6MDB3M\"},{\"id\":\"1377367570831921152\",\"possibly_sensitive\":false,\"created_at\":\"2021-03-31T21:09:53.000Z\",\"text\":\"Historically, infrastructure has been bipartisan. There is no reason it can't be bipartisan again.The divisions of the moment shouldn't stop us from doing right by the future.\"}]}";
            TwitterTwitts Twitt = JsonSerializer.Deserialize<TwitterTwitts>(data, options);
            Assert.NotEmpty(Twitt.Data);
        }

        [Fact]
        public void UserHasNoTwittsTwitterTwittSerializerTest()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string data = "{\"data\":[]}";
            TwitterTwitts Twitt = JsonSerializer.Deserialize<TwitterTwitts>(data, options);
            Assert.Empty(Twitt.Data);
        }
    }
}
