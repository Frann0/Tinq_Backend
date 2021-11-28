using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using System.Collections.Generic;
using qwertygroup.Core.Models;
using System.Linq;

namespace qwertygroup.Domain.Test.IRepositories
{
    public class IBodyRepositoryTest
    {

        [Fact]
        public void IBodyRepository_IsAvailable()
        {
            //instantiate a mock repository instance
            var repository = new Mock<IBodyRepository>().Object;
            Assert.NotNull(repository);
        }
        
        [Fact]
        public void GetBody_WithNoParam_ReturnsListOfAllBodies()
        {
            //Set up mock repository and define get bodies method
            var mock = new Mock<IBodyRepository>();
            var fakeList = new List<Body>();
            mock.Setup(r => r.GetBodies()).Returns(fakeList);

            var repository = mock.Object;
            Assert.Equal(fakeList, repository.GetBodies());
        }

        [Fact]
        public void Create_Body_Method_Exists_And_Returns_The_Created_Body()
        {
            //Set up mock repository and define methods
            var mock = new Mock<IBodyRepository>();
            Body body = new Body { Text = "someBody" };
            mock.Setup(r => r.CreateBody(body.Text)).Returns(body);

            var repository = mock.Object;
            Assert.Equal(body, repository.CreateBody(body.Text));
        }

        [Fact]
        public void Delete_Body_Method_Exists_And_Deletes_Given_Body()
        {
            //Setup the mock repository and define methods
            var mock = new Mock<IBodyRepository>();
            Body body = new Body { Text = "someBody", Id = 1 };
            var fakeList = new List<Body> { body };
            var repository = mock.Object;
            mock.Setup(r => r.GetBodies()).Returns(fakeList);
            mock.Setup(r => r.DeleteBody(body.Id)).Callback(() => fakeList.RemoveAll(b => b.Id == body.Id));

            repository.DeleteBody(body.Id);

            Assert.Empty(fakeList);
        }

        [Fact]
        public void Update_Body_Method_Exists_And_Updates_Given_Body()
        {
            //Setup the mock repository and define methods
            var mock = new Mock<IBodyRepository>();
            Body body = new Body { Text = "someBody", Id = 1 };
            var fakeList = new List<Body> { body };
            Body body2 = new Body { Text = "someBody2", Id = 1 };
            
            var repository = mock.Object;
            mock.Setup(r => r.GetBodies()).Returns(fakeList);
            mock.Setup(r => r.UpdateBody(body2)).Callback(() => fakeList.First(b => b.Id == body2.Id).Text=body2.Text);

            repository.UpdateBody(body2);

            Assert.True(body.Text==body2.Text);
        }
    }
}