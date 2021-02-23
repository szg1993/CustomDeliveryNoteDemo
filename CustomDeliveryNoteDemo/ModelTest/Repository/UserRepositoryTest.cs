using Model;
using Model.Models;
using Model.Repository;
using ModelTest.Sample;
using ModelTest.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ModelTest.Repository
{
    public class UserRepositoryTest
    {
        [Fact]
        public void GetUserByNameAndPassword_ShouldReturnUser_WhenExists()
        {
            //Arrange
            var sampleFactory = new SampleFactory();
            var sampleUsers = sampleFactory.GetSampleUserList();
            var mockSet = DbSetMock<User>.CreateDbSetMock(sampleUsers);
            var mockContext = new Mock<CustomDeliveryNoteContext>();
            mockContext.Setup(r => r.User).Returns(mockSet.Object);

            //Act
            var sut = new UserRepository(mockContext.Object);
            var actual = sut.GetUserByNameAndPassword("admin", "admin");

            //Assert
            Assert.NotNull(actual);
        }
    }
}
