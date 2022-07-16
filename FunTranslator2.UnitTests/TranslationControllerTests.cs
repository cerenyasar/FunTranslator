using FunTranslator2.Controllers;
using FunTranslator2.Dtos;
using FunTranslator2.Models;
using FunTranslator2.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FunTranslator2.UnitTests
{
    [TestClass]
    public class TranslationControllerTests
    {
        [TestMethod]
        public void InsertTranslationTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Translation>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Translations).Returns(mockSet.Object);
            var service = new Mock<IFunTranslatorService>();
            var testTranslation = new ResponseDTO
            {
                contents = new ContentDTO
                {
                    Text = "Hello",
                    Translated = "]-[3llO",
                    Translation = "leetspeak"
                }
            };

            // Act
            var controller = new TranslationController(service.Object, mockContext.Object);
            controller.InsertTranslation(testTranslation);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Translation>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [TestMethod]
        public void GetTranslationRecordsTest()
        {
            //Arrange
            var expectedList = new List<Translation>
            {
                new Translation { Id= 1, Text= "welcome",Translated = "wElkOmE", TranslationType = "leetspeak" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Translation>>();
            mockSet.As<IQueryable<Translation>>().Setup(m => m.Provider).Returns(expectedList.Provider);
            mockSet.As<IQueryable<Translation>>().Setup(m => m.Expression).Returns(expectedList.Expression);
            mockSet.As<IQueryable<Translation>>().Setup(m => m.ElementType).Returns(expectedList.ElementType);
            mockSet.As<IQueryable<Translation>>().Setup(m => m.GetEnumerator()).Returns(expectedList.GetEnumerator());
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Translations).Returns(mockSet.Object);

            var service = new Mock<IFunTranslatorService>();

            //Act
            var controller = new TranslationController(service.Object, mockContext.Object);
            var returned = controller.GetTranslationRecords();
            List<Translation> actualList = (List<Translation>)returned.Data;

            //Assert
            Assert.AreEqual(expectedList.Count(), actualList.Count);
            Assert.AreEqual(expectedList.ToList()[0].Text, actualList[0].Text);
            Assert.AreEqual(expectedList.ToList()[0].Translated, actualList[0].Translated);
        }
    }
}
