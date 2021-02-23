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
    public class RecipientRepositoryTest
    {
        [Fact]
        public void IsRecipientExists_ShouldReturnTrue_WhenExists()
        {
            //Arrange
            var sampleFactory = new SampleFactory();
            var sampleRecipients = sampleFactory.GetSampleRecipientList();
            var mockSet = DbSetMock<Recipient>.CreateDbSetMock(sampleRecipients);
            var mockContext = new Mock<CustomDeliveryNoteContext>();
            mockContext.Setup(r => r.Recipient).Returns(mockSet.Object);

            //Act
            var sut = new RecipientRepository(mockContext.Object);
            var actual = sut.IsRecipientExists("Michael Jordan");

            //Assert
            Assert.True(actual);
        }
    }
}
