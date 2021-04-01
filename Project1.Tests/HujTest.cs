using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project1.Tests
{
    public class HujTest
    {
        [Fact]
        public void HujPass()
        {
            int huj = 1;
            Assert.Equal(1, huj);
        }
    }
}
