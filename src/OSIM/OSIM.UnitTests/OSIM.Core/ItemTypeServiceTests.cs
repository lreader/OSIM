using System.Collections.Generic;
using System.Linq;
using Moq;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using OSIM.Core.Entities;
using OSIM.Core.Services;
using OSIM.Persistence;

namespace OSIM.UnitTests.OSIM.Core
{
    public class when_using_the_item_type_service : Specification
    {
        
    }

    public class and_getting_a_list_of_item_types : when_using_the_item_type_service
    {
        private IList<ItemType> _result;
        private int _expectedNumberOfItemsToReturn;
        private int _itemTypeOneId;
        private int _itemTypeTwoId;
        private int _itemTypeThreeId;
        private IItemTypeService _itemTypeService;

        protected override void Establish_context()
        {
            base.Establish_context();

            var itemTypeRepository = new Mock<IItemTypeRepository>();
            _itemTypeService = new ItemTypeService(itemTypeRepository.Object);

            _itemTypeOneId = 1;
            _itemTypeTwoId = 2;
            _itemTypeThreeId = 3;

            var itemTypeOne = new ItemType {Id = _itemTypeOneId};
            var itemTypeTwo = new ItemType {Id = _itemTypeTwoId};
            var itemTypeThree = new ItemType {Id = _itemTypeThreeId};
            var itemTypeList = new List<ItemType> {itemTypeOne, itemTypeTwo, itemTypeThree};
            _expectedNumberOfItemsToReturn = itemTypeList.Count;
            itemTypeRepository.Setup(x => x.GetAll).Returns(itemTypeList);
        }

        protected override void Because_of()
        {
            _result = _itemTypeService.GetItemTypes();
        }

        [Test]
        public void then_the_list_of_items_should_be_returned()
        {
            _result.ShouldNotBeNull();
            _result.Count.ShouldEqual(_expectedNumberOfItemsToReturn);
            _result.Where(x => x.Id == _itemTypeOneId).Count().ShouldEqual(1);
            _result.Where(x => x.Id == _itemTypeTwoId).Count().ShouldEqual(1);
            _result.Where(x => x.Id == _itemTypeThreeId).Count().ShouldEqual(1);
        }
    }
}