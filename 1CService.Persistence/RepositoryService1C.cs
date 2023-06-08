using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _1CService.Persistence
{
    public class RepositoryService1C : IRepositoryService1C
    {
        private readonly HttpClient m_Client;
        private bool disposedValue;
        private Settings m_Settings;

        public event EventHandler<string> OnErrorMessage = (_param1, _param2) => { };

        public RepositoryService1C(Settings settings)
        {
            m_Settings = settings;
            m_Client = new HttpClient();
            m_Client.BaseAddress = new Uri("http://" + m_Settings.ServiceAddress + "/" + m_Settings.ServiceBaseName + "/hs/");
            m_Client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(m_Settings.User1C + ":" + m_Settings.Password1C)));
        }
        public HttpClient InitTextContext()
        {
            m_Client.DefaultRequestHeaders.Accept.Clear();
            m_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));
            return m_Client;
        }
        public HttpClient InitJsonContext()
        {
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
        public async Task<byte[]> PostStreamAsync(HttpClient client, string nameFunc, HttpContent param)
        {
            try
            {
                HttpResponseMessage responsePost = await client.PostAsync(m_Settings.ServiceSection + "/" + nameFunc, param);
                if (!responsePost.IsSuccessStatusCode)
                    return Array.Empty<byte>();

                using (Stream file = await responsePost.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream).ConfigureAwait(false);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                EventHandler<string> onErrorMessage = OnErrorMessage;
                if (onErrorMessage != null)
                    onErrorMessage(this, ex.Message);
                return Array.Empty<byte>();
            }
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
                EventHandler<string> onErrorMessage = OnErrorMessage;
                if (onErrorMessage != null)
                    onErrorMessage(this, ex.Message);
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
                EventHandler<string> onErrorMessage = OnErrorMessage;
                if (onErrorMessage != null)
                    onErrorMessage(this, ex.Message);
                return default;
            }
        }
        public async Task<string> httpGetAsync(HttpClient client, string nameFunc)
        {
            try
            {
                HttpResponseMessage responsePost = await client.GetAsync(m_Settings.ServiceSection + "/" + nameFunc).ConfigureAwait(false);
                if (!responsePost.IsSuccessStatusCode)
                    return string.Empty;
                string async = await responsePost.Content.ReadAsStringAsync().ConfigureAwait(false);
                return async;
            }
            catch (Exception ex)
            {
                EventHandler<string> onErrorMessage = OnErrorMessage;
                if (onErrorMessage != null)
                    onErrorMessage(this, ex.Message);
                return string.Empty;
            }
        }
        public async Task<byte[]> GetStreamAsync(HttpClient client, string nameFunc)
        {
            try
            {
                using (Stream file = await client.GetStreamAsync(m_Settings.ServiceSection + "/" + nameFunc).ConfigureAwait(false))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream).ConfigureAwait(false);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                EventHandler<string> onErrorMessage = OnErrorMessage;
                if (onErrorMessage != null)
                    onErrorMessage(this, ex.Message);
                return null;
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
            GC.SuppressFinalize(this);
        }
    }
}
