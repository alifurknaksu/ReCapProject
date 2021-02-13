﻿using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IColorDal : IEntityRepository<Color>
    {
        void Delete(System.Drawing.Color color);
        void Add(System.Drawing.Color color);
    }
}
