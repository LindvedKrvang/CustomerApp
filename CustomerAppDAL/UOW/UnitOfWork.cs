using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL.Context;
using CustomerAppDAL.Repositories;

namespace CustomerAppDAL.UOW
{
    internal class UnitOfWork : IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; internal set; }
        public IOrderRepository OrderRepository { get; internal set; }
        public IAddressRepository AddressRepository { get; internal set; }

        private CustomerAppContext _context;

        public UnitOfWork()
        {
            _context = new CustomerAppContext();
            _context.Database.EnsureCreated();

            CustomerRepository = new CustomerRepository(_context);
            OrderRepository = new OrderRepository(_context);
            AddressRepository = new AddressRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            //The number of objects written to the underlying database.
            return _context.SaveChanges();
        }
    }
}
