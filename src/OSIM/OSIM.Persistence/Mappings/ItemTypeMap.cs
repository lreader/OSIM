using FluentNHibernate.Mapping;
using OSIM.Core.Entities;

namespace OSIM.Persistence.Mappings
{
    public class ItemTypeMap : ClassMap<ItemType>
    {
        public ItemTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}