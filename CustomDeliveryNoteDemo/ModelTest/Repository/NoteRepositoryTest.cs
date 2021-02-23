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
    public class NoteRepositoryTest
    {
        [Fact]
        public void GetByNumber_ShouldReturnNote_WhenExists()
        {
            //Arrange
            var sampleFactory = new SampleFactory();
            var sampleNotes = sampleFactory.GetSampleNoteList();
            var mockSet = DbSetMock<Note>.CreateDbSetMock(sampleNotes);
            var mockContext = new Mock<CustomDeliveryNoteContext>();
            mockContext.Setup(r => r.Note).Returns(mockSet.Object);

            //Act
            var sut = new NoteRepository(mockContext.Object);
            var actual = sut.GetByNumber("N00001");

            //Assert
            Assert.NotNull(actual);
        }
    }
}
