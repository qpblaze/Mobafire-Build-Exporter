using LoLSets.Infrastructure.Services;
using System;
using Xunit;

namespace LoLSets.Test
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            ItemService itemService = new ItemService();

            
        }

        [Fact]
        public void FailingTeset()
        {
            Assert.Equal(2, 3);
        }
    }
}
