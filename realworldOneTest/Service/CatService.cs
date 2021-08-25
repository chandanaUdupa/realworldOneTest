using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace realworldOneTest.Service
{
     public interface ICatService
    {
        byte[] GetRandomCatImage(string apiUrl);
        string GetCatJsonData(string apiUrl);
    }

    public class CatService: ICatService
    {
        /// <summary>
        /// Get cat image (default and basic most API call)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public byte[] GetRandomCatImage(string apiUrl)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.Timeout = TimeSpan.FromSeconds(900);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetByteArrayAsync(apiUrl);
                    response.Wait();
                    return response.Result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetCatJsonData(string apiUrl)
        {
            string result = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.Timeout = TimeSpan.FromSeconds(900);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync(apiUrl);
                    response.Wait();
                    result = response.Result;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
