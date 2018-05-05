using System;
using NUnit.Framework;
using Moq;


using AGLTest.Services;
using AGLTest.Controllers;
using AGLTest.Models;
using System.Collections.Generic;
namespace AGLTest.UnitTests
{
    [TestFixture]
    public class PetControllerTests
    {
        private Mock<IPetService> _petService;

        [SetUp]
        public void Setup()
        {
            _petService = new Mock<IPetService>();
        }

        public PetController CreatePetController()
        {
            var petController = new PetController(_petService.Object);

            return petController;
        }

        [Test]
        public void GetCatsByOwnerGender_CatsByOwnderGenderListReturned()
        {
            //arrange
            
            var cat = new List<Cat>()
            {
                new Cat()
                {
                    Name = "Bob",
                    OwnerGender = "Male"
                },
                new Cat()
                {
                    Name = "smith",
                    OwnerGender = "Male"
                },

            };

            _petService.Setup(o => o.GetCatsByOwnerGender()).Returns(cat);

            var compareLogic = CompareLogicCreator.Create();

            var petsController = CreatePetController();

            //act
            var allCats = petsController.Index();

            var compareResults = compareLogic.Compare(cat,allCats);

            //assert
            NUnit.Framework.Assert.IsTrue(compareResults.AreEqual, compareResults.DifferencesString);

        }

    }
}
