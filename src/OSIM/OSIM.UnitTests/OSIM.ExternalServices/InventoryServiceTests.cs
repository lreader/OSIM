using System.Collections.Generic;
using OSIM.Core.Entities;
using System.Linq;
using Moq;
using NBehave.Spec.NUnit;
using NUnit.Framework;    
using OSIM.Core.Services;    
using OSIM.ExternalServices;   

namespace OSIM.UnitTests.OSIM.ExternalServices
{
    public class when_using_the_external_inventory_service : Specification
    {
        
    }

    public class and_getting_a_list_of_item_types : when_using_the_external_inventory_service
    {
        private IInventoryService _inventoryService;
        private string[] _result; 
        private Mock<IItemTypeService> _itemTypeService;
        private int _expectedNumberOfItems;
        private string _itemOneName;
        private string _itemTwoName;
        private string _itemThreeName;

        protected override void Establish_context()
        {
            base.Establish_context();

            _itemTypeService = new Mock<IItemTypeService>();
            _inventoryService = new InventoryService(_itemTypeService.Object);

            _itemOneName = "USB drives";
            _itemTwoName = "Nerf darts";
            _itemThreeName = "Flying Monkeys";
            var itemTypeOne = new ItemType {Id = 1, Name = _itemOneName};
            var itemTypeTwo = new ItemType {Id = 2, Name = _itemTwoName};
            var itemTypeThree = new ItemType {Id = 3, Name = _itemThreeName};
            var itemTypeList = new List<ItemType>
                                   {
                                       itemTypeOne, 
                                       itemTypeTwo, 
                                       itemTypeThree
                                   };
            _expectedNumberOfItems = itemTypeList.Count;
            _itemTypeService.Setup(x => x.GetItemTypes())
                .Returns(itemTypeList);
        }

        protected override void Because_of()
        {
            _result = _inventoryService.GetItemTypes();
        } 

        [Test]
        public void then_a_list_of_item_types_should_be_returned()
        {
            _result.Count().ShouldEqual(_expectedNumberOfItems);
            _result.OfType<string>().Select(x => x == _itemOneName)
                .FirstOrDefault()
                .ShouldNotBeNull();
            _result.OfType<string>().Select(x => x == _itemTwoName)
                .FirstOrDefault()
                .ShouldNotBeNull();
            _result.OfType<string>().Select(x => x == _itemThreeName)
                .FirstOrDefault()
                .ShouldNotBeNull();            
        }
    }
}