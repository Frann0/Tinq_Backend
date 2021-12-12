using System;
using qwertygroup.Core.Models;
using Xunit;

namespace qwertygroup.Core.Test.Models
{
    public class MyIdentifyableTest
    {
        [Fact]
        public void MyIdentifyableExists()
        {
            var myIdentifyable = new MyIdentifyableExtendingClass();
            Assert.IsAssignableFrom<MyIdentifyable>(myIdentifyable);
        }
        [Theory]
        [InlineData(-1000)]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(int.MaxValue)]
        public void idMustBePositiveCheck(int id)
        {
            
            var myIdentifyable = new MyIdentifyableExtendingClass();
            if (id <= 0)
            {
                string message = "Id must be greater than 0!";
                Assert.Equal(message,
                Assert.Throws<InvalidOperationException>(()=>myIdentifyable.CheckId(id)).Message
                );
            }
            id=id+1;
            if(id>int.MaxValue){
                Assert.Throws<InvalidOperationException>(()=>myIdentifyable.CheckId(id));
            }
        }
    }

    public class MyIdentifyableExtendingClass : MyIdentifyable
    {
        public int Id { get; set; }
    }
}