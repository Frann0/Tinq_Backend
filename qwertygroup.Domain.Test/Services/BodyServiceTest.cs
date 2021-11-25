using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using qwertygroup.Core.IServices;

namespace qwertygroup.Domain.Test.Services
{
    public class BodyServiceTest
    {
        private readonly Mock<IBodyRepository> _mockBodyRepository;
        private readonly IBodyService _bodyService;
    
        public BodyServiceTest(){
            _mockBodyRepository=new Mock<IBodyRepository>();
            _bodyService=new BodyService(_mockBodyRepository.Object);
        }
        
        [Fact]
        public void BodyService_IsIBodyService(){
            Assert.IsAssignableFrom<IBodyService>(_bodyService);
        }

        [Fact]
        public void BodyService_WithNullProductRepository_ThrowsInvalidDataException(){
            Assert.Throws<System.MissingFieldException>(()=>new BodyService(null));
        }
    }
}