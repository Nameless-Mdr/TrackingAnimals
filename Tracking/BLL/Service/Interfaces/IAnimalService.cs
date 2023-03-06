﻿using BLL.Service.Base;
using Domain.Entity.Animal;

namespace BLL.Service.Interfaces;

public interface IAnimalService : IBaseService<long, Animal>
{
    
}