using System;
using System.Collections.Generic;
using NHibernate;
using OSIM.Core.Entities;

namespace OSIM.Persistence
{
    public interface IItemTypeRepository
    {
        int Save(ItemType itemType);
        ItemType GetById(int id);
        IList<ItemType> GetAll { get; }
    }

    public class ItemTypeRepository : IItemTypeRepository
    {
        private ISessionFactory _sessionFactory;

        public ItemTypeRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public int Save(ItemType itemType)
        {
            int id;
            using (var session = _sessionFactory.OpenSession())
            {
                id = (int) session.Save(itemType);                
                session.Flush();
            }
            return id;
        }

        public ItemType GetById(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<ItemType>(id);
            }
        }

        public IList<ItemType> GetAll
        {
            get
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    return session.CreateCriteria(typeof(ItemType)).List<ItemType>();
                }
            }
        }
    }
}