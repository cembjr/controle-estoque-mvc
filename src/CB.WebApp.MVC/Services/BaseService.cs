using CB.WebApp.MVC.Extensions;
using CB.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CB.WebApp.MVC.Services
{
    public class BaseService
    {
        protected readonly HttpClient httpClient;
        public BaseService(HttpClient httpClient, Uri base_url)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = base_url;
        }

        protected StringContent GetContent(object obj)
        {
            return new StringContent(
                JsonSerializer.Serialize(obj),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> Deserialize<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool IsResponseValido(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<ResponseResult<TRet>> Post<TModel, TRet>(TModel model, string COMPL_URL)
            where TRet : class, IResponse
            where TModel : class
        {
            var loginContent = GetContent(model);
            var result = new ResponseResult<TRet>();

            var response = await httpClient.PostAsync(COMPL_URL, loginContent);

            result.Sucesso = IsResponseValido(response);

            if (!result.Sucesso)
                result.ErrosResponse = await Deserialize<ErrosResponse>(response);
            else
                result.Result = await Deserialize<TRet>(response);
            
            return result;
        }

        public async Task<bool> Post<TModel>(TModel model, string COMPL_URL)
           where TModel : class
        {
            var content = GetContent(model);

            var response = await httpClient.PostAsync(COMPL_URL, content);

            return IsResponseValido(response);
        }

        public async Task<bool> Put<TModel>(TModel model, string COMPL_URL)
           where TModel : class
        {
            var content = GetContent(model);
            var response = await httpClient.PutAsync(COMPL_URL, content);
            return IsResponseValido(response);
        }

        public async Task<bool> Delete(string COMPL_URL)
        {
            var response = await httpClient.DeleteAsync(COMPL_URL);
            return IsResponseValido(response);
        }
        public async Task<ResponseResult<TRet>> GetAsync<TRet>(string COMPL_URL)
            where TRet: class
        {
            var result = new ResponseResult<TRet>();

            var response = await httpClient.GetAsync(COMPL_URL);

            result.Sucesso = IsResponseValido(response);

            if (!result.Sucesso)
                result.ErrosResponse = await Deserialize<ErrosResponse>(response);

            else
                result.Result = await Deserialize<TRet>(response);

            return result;
        }

        public async Task<ResponseResult<List<TRet>>> GetListAsync<TRet>(string COMPL_URL)
            where TRet : class
        {
            var result = new ResponseResult<List<TRet>>();

            var response = await httpClient.GetAsync(COMPL_URL);

            result.Sucesso = IsResponseValido(response);

            if (!result.Sucesso)
                result.ErrosResponse = await Deserialize<ErrosResponse>(response);

            else
                result.Result = await Deserialize<List<TRet>>(response);

            return result;
        }
    }
}
