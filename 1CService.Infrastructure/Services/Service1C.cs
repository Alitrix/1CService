using _1CService.Application.DTO;
using _1CService.Application.Enums;
using _1CService.Application.Interfaces.Services;
using _1CService.Infrastructure.Services;
using _1CService.Utilities;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace _1CService.Persistence.Services
{
    public class Service1C : IService1C
    {
        private HttpClient? m_Client;
        private bool disposedValue;
        private readonly IAppUserService _appUserService;
        private readonly ILogger<Service1C> _logger;
        private Settings m_Settings;

        public Service1C(IAppUserService appUserService, ILogger<Service1C> logger)/////////////
        {
            _appUserService = appUserService;
            _logger = logger;
        }
        public async Task<HttpClient> InitContext(TypeContext1CService serviceType)
        {
            m_Settings = await _appUserService.GetHTTPService1CSettings();
            _logger.LogInformation($"Init context :{serviceType}, settings :{m_Settings}");
            if (m_Client == null)
            {
                m_Client = new HttpClient();
                m_Client.BaseAddress = new Uri("http://" + m_Settings.ServiceAddress + "/" + m_Settings.ServiceBaseName + "/hs/");
            }

            m_Client?.DefaultRequestHeaders.Clear();
            m_Client?.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(m_Settings.User1C + ":" + m_Settings.Password1C)));

            m_Client?.DefaultRequestHeaders.Accept.Clear();

            switch (serviceType)
            {
                case TypeContext1CService.Text:
                    m_Client?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));
                    break;
                case TypeContext1CService.Json:
                    m_Client?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    break;
            };
            return m_Client;
        }
        public async Task<T> PostAsync<T>(HttpClient client, string nameFunc, HttpContent param)
        {
            try
            {
                Type t = typeof(T);
                _logger.LogInformation($"Income Post async Func :{nameFunc}, param :{param}");
                HttpResponseMessage responsePost = await client.PostAsync(m_Settings.ServiceSection + "/" + nameFunc, param);
                if (!responsePost.IsSuccessStatusCode)
                    return default;
                string strResponse = await responsePost.Content.ReadAsStringAsync().ConfigureAwait(false);
                return strResponse.ToObj<T>();
            }
            catch (Exception ex)
            {
                //OnErrorMessage?.Invoke(this, ex.Message);
                return await Task.FromResult<T>(default);
            }
        }
        public async Task<T> GetAsync<T>(HttpClient client, string nameFunc)
        {
            try
            {
                _logger.LogInformation($"Income Get async Func :{nameFunc}");
                HttpResponseMessage responseGet = await client.GetAsync(m_Settings.ServiceSection + "/" + nameFunc).ConfigureAwait(false);
                if (!responseGet.IsSuccessStatusCode)
                    return default;
                string strResponse = await responseGet.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<T>(strResponse);
            }
            catch (Exception ex)
            {
                //OnErrorMessage?.Invoke(this, ex.Message);
                return await Task.FromResult<T>(default);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;
            if (disposing)
                m_Client?.Dispose();
            disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
