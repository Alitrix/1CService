using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace _1CService.Persistence.API
{
    public class Service1C : IService1C
    {
        private readonly HttpClient m_Client;
        private bool disposedValue;
        private readonly IAuthenticateRepositoryService _authenticateRepository;
        private Settings m_Settings;

        public event EventHandler<string> OnErrorMessage = (_param1, _param2) => { };

        public Service1C(IAuthenticateRepositoryService authenticateRepository)
        {
            _authenticateRepository = authenticateRepository;
            
            m_Client = new HttpClient();
            m_Client.BaseAddress = new Uri("http://" + m_Settings.ServiceAddress + "/" + m_Settings.ServiceBaseName + "/hs/");
            m_Client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(m_Settings.User1C + ":" + m_Settings.Password1C)));
        }
        public async Task<HttpClient> InitTextContext()
        {
            m_Settings = await _authenticateRepository.GetUserProfile(await _authenticateRepository.GetCurrentUser());
            m_Client.DefaultRequestHeaders.Accept.Clear();
            m_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));
            return m_Client;
        }
        public async Task<HttpClient> InitJsonContext()
        {
            m_Settings = await _authenticateRepository.GetUserProfile(await _authenticateRepository.GetCurrentUser());
            m_Client.DefaultRequestHeaders.Accept.Clear();
            m_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return m_Client;
        }
        public void SetSettings(Settings settings)
        {
            m_Settings = settings;
            m_Client.DefaultRequestHeaders.Clear();
            m_Client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(m_Settings.User1C + ":" + m_Settings.Password1C)));
        }
        public async Task<T> PostAsync<T>(HttpClient client, string nameFunc, HttpContent param)
        {
            try
            {
                HttpResponseMessage responsePost = await client.PostAsync(m_Settings.ServiceSection + "/" + nameFunc, param);
                if (!responsePost.IsSuccessStatusCode)
                    return default;
                string retJsonString = await responsePost.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<T>(retJsonString);
            }
            catch (Exception ex)
            {
                OnErrorMessage?.Invoke(this, ex.Message);
                return await Task.FromResult<T>(default);
            }
        }
        public async Task<T> GetAsync<T>(HttpClient client, string nameFunc)
        {
            try
            {
                HttpResponseMessage responsePost = await client.GetAsync(m_Settings.ServiceSection + "/" + nameFunc).ConfigureAwait(false);
                if (!responsePost.IsSuccessStatusCode)
                    return default;
                string str = await responsePost.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<T>(str);
            }
            catch (Exception ex)
            {
                OnErrorMessage?.Invoke(this, ex.Message);
                return default;
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
