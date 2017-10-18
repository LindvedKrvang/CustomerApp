using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAppBLL.BusinessObjects;
using CustomerAppBLL.Converters;
using CustomerAppDAL;

namespace CustomerAppBLL.Services
{
    internal class AddressService : IAddressService
    {
        private readonly AddressConverter _converter;
        private readonly DALFacade _facade;

        public AddressService(DALFacade facade)
        {
            _facade = facade;
            _converter = new AddressConverter();
        }

        public AddressBO Create(AddressBO address)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var addressCreated = uow.AddressRepository.Create(_converter.Convert(address));
                uow.Complete();
                return _converter.Convert(addressCreated);
            }
        }

        public List<AddressBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                return uow.AddressRepository.GetAll().Select(_converter.Convert).ToList();
            }
        }

        public AddressBO Get(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                return _converter.Convert(uow.AddressRepository.Get(id));
            }
        }

        public AddressBO Update(AddressBO address)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var addressToBeUpdated = uow.AddressRepository.Get(address.Id);
                addressToBeUpdated.City = address.City;
                addressToBeUpdated.Number = address.Number;
                addressToBeUpdated.Street = address.Street;

                uow.Complete();
                return _converter.Convert(addressToBeUpdated);
            }
        }

        public AddressBO Delete(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var addressToBeDeleted = uow.AddressRepository.Get(id);
                uow.AddressRepository.Delete(id);
                uow.Complete();
                return _converter.Convert(addressToBeDeleted);
            }
        }
    }
}
