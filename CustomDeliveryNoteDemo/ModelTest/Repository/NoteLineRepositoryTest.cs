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
    public class NoteLineRepositoryTest
    {
        [Fact]
        public void GetNoteLinesInNote_ShouldReturnNoteLines_WhenTheyExist()
        {
            //Arrange
            var sampleFactory = new SampleFactory();
            var sampleNoteLines = sampleFactory.GetSampleNoteLineList();
            var mockSet = DbSetMock<NoteLine>.CreateDbSetMock(sampleNoteLines);
            var mockContext = new Mock<CustomDeliveryNoteContext>();
            mockContext.Setup(r => r.NoteLine).Returns(mockSet.Object);

            //Act
            var sut = new NoteLineRepository(mockContext.Object);
            var actual = sut.GetNoteLinesInNote(1);

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Count() == 2);
        }
    }
}
