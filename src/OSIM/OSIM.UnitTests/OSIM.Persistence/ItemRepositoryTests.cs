using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using OSIM.Core.Entities;
using OSIM.Persistence;

namespace OSIM.UnitTests.OSIM.Core
{
    public class when_working_with_the_item_type_repository : Specification
    {
        protected IItemTypeRepository _itemTypeRepository;
        protected Mock<ISessionFactory> _sessionFactory;
        protected Mock<ISession> _session;

        protected override void Establish_context()
        {
            base.Establish_context();

            _sessionFactory = new Mock<ISessionFactory>();
            _session = new Mock<ISession>();

            _sessionFactory.Setup(sf => sf.OpenSession()).Returns(_session.Object);            

            _itemTypeRepository = new ItemTypeRepository(_sessionFactory.Object);
        }
    }

    public class and_saving_an_invalid_item_type : when_working_with_the_item_type_repository
    {
        private Exception _result;

        protected override void Establish_context()
        {
            base.Establish_context();

            _session.Setup(s => s.Save(null)).Throws(new ArgumentNullException());
        }

        protected override void Because_of()
        {
            try
            {
                _itemTypeRepository.Save(null);
            }
            catch (Exception exception)
            {
                _result = exception;
            }
        }

        [Test]
        public void then_an_argument_null_exception_should_be_raised()
        {
            _result.ShouldBeInstanceOfType(typeof(ArgumentNullException));
        }
    }

    public class and_saving_a_valid_item_type : when_working_with_the_item_type_repository
    {
        private int _result;
        private ItemType _testItemType;
        private int _itemTypeId;

        protected override void Establish_context()
        {
            base.Establish_context();

            var randomNumberGenerator = new Random();
            _itemTypeId = randomNumberGenerator.Next(32000);

            _session.Setup(s => s.Save(_testItemType)).Returns(_itemTypeId);            
        }

        protected override void Because_of()
        {
            _result = _itemTypeRepository.Save(_testItemType);
        }

        [Test]
        public void then_a_valid_item_type_id_should_be_returned()
        {
            _result.ShouldEqual(_itemTypeId);
        }
    }

    public class and_getting_a_list_of_all_the_item_types : when_working_with_the_item_type_repository
    {
        private IList<ItemType> _result;
        private int _expectedNumberOfItemsToReturn;
        private int _itemTypeOneId;
        private int _itemTypeTwoId;
        private int _itemTypeThreeId;


        protected override void Establish_context()
        {
            base.Establish_context();

            _itemTypeOneId = 1;
            _itemTypeTwoId = 2;
            _itemTypeThreeId = 3;

            var itemTypeOne = new ItemType { Id = _itemTypeOneId };
            var itemTypeTwo = new ItemType { Id = _itemTypeTwoId };
            var itemTypeThree = new ItemType { Id = _itemTypeThreeId };
            var itemTypeList = new List<ItemType> { itemTypeOne, itemTypeTwo, itemTypeThree };
            _expectedNumberOfItemsToReturn = itemTypeList.Count;

            var mockCriteria = new Mock<ICriteria>();            
            mockCriteria.Setup(x => x.List<ItemType>()).Returns(itemTypeList);
            _session.Setup(x => x.CreateCriteria(typeof (ItemType))).Returns(mockCriteria.Object);
        }

        protected override void Because_of()
        {
            _result = _itemTypeRepository.GetAll;
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