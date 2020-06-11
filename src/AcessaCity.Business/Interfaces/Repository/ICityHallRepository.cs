using System;
using System.Threading.Tasks;
using AcessaCity.Business.Models;

namespace AcessaCity.Business.Interfaces.Repository
{
    public interface ICityHallRepository : IRepository<CityHall>
    {
        Task Inactive(Guid cityHallId);
    }
}