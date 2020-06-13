using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcessaCity.Business.Models;

namespace AcessaCity.Business.Interfaces.Service
{
    public interface ICityHallService : IDisposable
    {
        Task Add(CityHall category);
        Task Update(CityHall category);
        Task Remove(CityHall id);
        Task<IList<CityHall>> GetAll();
        Task Inactive(Guid cityHallId);   
    }
}