using Xunit;
namespace Dianzhu.XTest
{
    public class Class1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 4);
        }
        [Fact]
        public void FailingTest()
        {
            Assert.Equal(4, 5);
        }
    }
}
