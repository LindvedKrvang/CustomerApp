using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL.Context;
using CustomerAppDAL.Repositories;
using CustomerAppDAL.UOW;

namespace CustomerAppDAL
{
    public class DALFacade
    {
        public IUnitOfWork UnitOfWork => new UnitOfWork();
    }
}
