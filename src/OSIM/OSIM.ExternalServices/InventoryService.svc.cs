using System.Linq;
using OSIM.Core.Services;

namespace OSIM.ExternalServices
{
    public class InventoryService : IInventoryService
    {
        private readonly IItemTypeService _itemTypeService;

        public InventoryService(IItemTypeService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }

        public string[] GetItemTypes()
        {
            var itemTypeList = _itemTypeService.GetItemTypes()
                .Select(x => x.Name)
                .ToArray();
            
            return itemTypeList;
        }
    }
}
