using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace qwertygroup.Domain.Test.Services
{
    public class BodyServiceTest
    {
        private readonly Mock<IBodyRepository> _mockBodyRepository;
        private readonly IBodyService _bodyService;
        private readonly List<Body> _expected;
        private readonly List<Body> _repoList = new List<Body>();

        public BodyServiceTest()
        {
            _mockBodyRepository = new Mock<IBodyRepository>();
            _bodyService = new BodyService(_mockBodyRepository.Object);
            _expected = new List<Body>{
                new Body{Id=1,Text="asd1"},
                new Body{Id=2,Text="asd2"}
        };
        }

        [Fact]
        public void BodyService_IsIBodyService()
        {
            Assert.IsAssignableFrom<IBodyService>(_bodyService);
        }

        [Fact]
        public void BodyService_With_Null_BodyRepository_Throws_MissingFieldException()
        {
            Assert.True(
                Assert.Throws<System.MissingFieldException>(() => new BodyService(null)).Message
                ==
                "BodyService Must have a BodyRepository!");
        }

        [Fact]
        public void GetBodies_CallsBodyRepositoriesFindAll_ExactlyOnce()
        {
            _bodyService.GetBodies();
            _mockBodyRepository.Verify(r => r.GetBodies(), Times.Once);
        }

        [Fact]
        public void BodyService_CreateBody_AddsTheBodyToTheListOfBodies()
        {
            List<Body> fakeList = new List<Body>();
            foreach (Body body in _expected)
            {
                _mockBodyRepository.Setup(r => r.CreateBody(body.Text))
                .Returns(body);
                fakeList.Add(_bodyService.CreateBody(body.Text));
            }
            _mockBodyRepository.Setup(r => r.GetBodies()).Returns(fakeList);
            Assert.NotEmpty(_bodyService.GetBodies());
            Assert.Equal(_bodyService.GetBodies(), _expected);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(16)]
        public void BodyService_CreateBody_Text_MustBeLongerThanThreeCharacters(int value)
        {
            string concat = "";
            for (int i = 0; i < value; i++)
                concat += "1";
            if (value <= 3)
                Assert.True(
                    Assert.Throws<System.InvalidOperationException>(() =>
                    _bodyService.CreateBody(concat)).Message
                    .Equals(
                    "A Post's body must be longer than three characters!"
                ));
        }

        [Fact]
        public void Delete_Body_Method_Exists_And_Deletes_Given_Body()
        {
            //Setup the mock repository and define methods
            int idToDelete = 1;
            _mockBodyRepository.Setup(r => r.GetBodies()).Returns(_repoList);
            int intialSize = _bodyService.GetBodies().Count;
            _mockBodyRepository.Setup(r => r.DeleteBody(idToDelete)).Callback(() => _repoList.RemoveAll(b => b.Id == idToDelete));

            _bodyService.DeleteBody(idToDelete);

            Assert.True(_bodyService.GetBodies().Count < 2);
        }


        [Theory]
        [InlineData(-3)]
        [InlineData(3)]
        public void Delete_Body_Method_Throws_Exception_When_Id_Is_Invalid(int idToDelete)
        {
            //Setup the mock repository and define methods
            _mockBodyRepository.Setup(r => r.GetBodies()).Returns(_repoList);
            string message = $"No body with given id: {idToDelete}";
            _mockBodyRepository.Setup(r => r.DeleteBody(idToDelete)).Callback(() =>
            {
                if (idToDelete <= 0)
                {
                    message = "Id must be greater than 0!";
                    throw new System.InvalidOperationException(message);
                }
                else if (_repoList.RemoveAll(b => b.Id == idToDelete) <= 0)
                    throw new System.InvalidOperationException(message);
            });

            Assert.Throws<System.InvalidOperationException>(() => _bodyService.DeleteBody(idToDelete));
        }

        [Fact]
        public void Update_Body_Method_Exists_And_Updates_Given_Body()
        {
            _repoList.Add(new Body { Id = 2, Text = "old text" });
            //Setup the mock repository and define methods
            _mockBodyRepository.Setup(r => r.GetBodies()).Returns(_repoList);
            Body updatedBody = new Body { Id = 2, Text = "new text" };
            _mockBodyRepository.Setup(r => r.UpdateBody(updatedBody)).Callback(() =>
            {
                _repoList.First(b => b.Id == updatedBody.Id).Text = updatedBody.Text;
            });
            _bodyService.UpdateBody(updatedBody);

            Assert.True(_repoList.First(b => b.Id == updatedBody.Id).Text == updatedBody.Text);
        }


        [Theory]
        [InlineData(-3)]
        [InlineData(3)]
        public void Update_Body_Method_Throws_Exception_When_Id_Is_Invalid(int idToUpdate)
        {

            Body body = new Body { Id = idToUpdate, Text = "Some text" };
            //Setup the mock repository and define methods

            _mockBodyRepository.Setup(r => r.GetBodies()).Returns(_repoList);
            string message = $"No body with given id: {body.Id}";
            _mockBodyRepository.Setup(r => r.UpdateBody(body)).Callback(() =>
            {
                try
                {
                    Body resultBody = _bodyService.GetBodies().First(b => b.Id == body.Id);
                    resultBody.Text = body.Text;
                }
                catch (System.Exception e)
                {
                    throw new System.InvalidOperationException(message);
                }
            });

            Assert.Throws<System.InvalidOperationException>(() => _bodyService.UpdateBody(body));
        }
    }
}