﻿using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfIGeneralServiceAndServiceDal:EfRepositoryDal<GeneralServiceAndService, DataContext>, IGeneralServiceAndServiceDal
    {
    }
}
