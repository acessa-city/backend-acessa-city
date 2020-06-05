using System;

namespace AcessaCity.API.Dtos.CityHall
{
    public class CityHallUpdateDto
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public string CNPJ { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string ZIPCode { get; set; }
        public string Email { get; set; }        
    }
}