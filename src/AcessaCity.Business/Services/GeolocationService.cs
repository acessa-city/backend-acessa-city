using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AcessaCity.Business.Dto.City.GeolocationAPI;
using AcessaCity.Business.Dtos.City;
using AcessaCity.Business.Interfaces.Service;
using Newtonsoft.Json;

namespace AcessaCity.Business.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IHttpClientFactory _clientFactory;
        const string GET_INFO_API = "http://open.mapquestapi.com/geocoding/v1/reverse?key=OtF9PTNB3McLyRVr2egT0mYbCf9HrqtW&location={0},{1}";
        const string GET_VIACEP_API = "https://viacep.com.br/ws/{0}/json/";

        public GeolocationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<VIACEPResult> GetVIACEPInfo(string cep)
        {
            var url = String.Format(GET_VIACEP_API, cep);
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _clientFactory.CreateClient();            
            var response = await client.GetAsync(url);

            VIACEPResult apiResult = null;

            if (response.IsSuccessStatusCode)
            {
                apiResult = JsonConvert.DeserializeObject<VIACEPResult>(await response.Content.ReadAsStringAsync());

                return apiResult;
            }

            return null;
        }

        public async Task<CityResultFromGeolocation> GetCityInfoFromLocation(double latitude, double longitude)
        {
            var url = String.Format(GET_INFO_API, latitude.ToString("G", CultureInfo.InvariantCulture), longitude.ToString("G", CultureInfo.InvariantCulture));

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _clientFactory.CreateClient();            
            var response = await client.GetAsync(url);

            MapQuestApiReverseResult apiReturn = null;

            if (response.IsSuccessStatusCode)
            {
                apiReturn = JsonConvert.DeserializeObject<MapQuestApiReverseResult>(await response.Content.ReadAsStringAsync());

                if (apiReturn.Results[0].Locations.Count() > 0)
                {                    
                    VIACEPResult viaCep = await this.GetVIACEPInfo(apiReturn.Results[0].Locations[0].PostalCode);
                    return new CityResultFromGeolocation()
                    {
                        Name = apiReturn.Results[0].Locations[0].AdminArea5,
                        StateName = apiReturn.Results[0].Locations[0].AdminArea3,
                        ZIPCode = apiReturn.Results[0].Locations[0].PostalCode,
                        Street = viaCep?.Logradouro,
                        Neighborhood = viaCep?.Bairro
                    };
                }
            }

            return null;
        }

        public void Dispose()
        {
            
        }
    }
}