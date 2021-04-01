using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;
using Project1;
using Project1.Models;

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
        public void Pass()
        {
            int huj = 1;
            Assert.Equal(1, huj);
        }
    }
}
