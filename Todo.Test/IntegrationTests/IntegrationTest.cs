using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Todo.Api;
using Todo.Api.ViewModels;
using Xunit;

namespace Todo.Test.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient testhttpClient;
        // The same config class which would be injected into your server-side controllers
        protected readonly IConfiguration configService;
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            // fetch some useful objects from the injection service
            configService = (IConfiguration)appFactory.Services.GetService(typeof(IConfiguration));

            testhttpClient = appFactory.CreateClient();
            Authenticate();
        }


        protected void Authenticate()
        {
            testhttpClient.DefaultRequestHeaders.Add("Authorization", GenerateTodoToken());
        }


        public string GenerateTodoToken()
        {
          
            try
            {
                string result = PostWithResponse(testhttpClient, "Account/Login", new LoginViewModel() { Email = configService.GetSection("IntegrationTest").GetSection("email").Value, Password = configService.GetSection("IntegrationTest").GetSection("password").Value }).Result;
                var jsonObject = JObject.Parse(result);
                TokenViewModel tokenViewModel = jsonObject.ToObject<TokenViewModel>();
                return tokenViewModel.AuthToken.ToString();
            }
            catch
            {
                throw;
            }
        }

        protected static void AssertEquals(object upsertObject, object getObject)
        {
            try
            {
                //im doing this because almost in every ocation the type of getObject has more properties than the upsertObject
                IList<PropertyInfo> upsertProps = new List<PropertyInfo>(upsertObject.GetType().GetProperties());
                IList<PropertyInfo> getProps = new List<PropertyInfo>(getObject.GetType().GetProperties());
                foreach (PropertyInfo upsertProp in upsertProps)
                {

                    if (upsertProp.GetValue(upsertObject) == null)
                    {
                        if (getProps.FirstOrDefault(x => x.Name == upsertProp.Name) != null)
                        {
                            //== is because they are suppose to be null and the Equals method throw NullReferenceException
                            Assert.True(upsertProp.GetValue(upsertObject) == (getProps.First(x => x.Name == upsertProp.Name).GetValue(getObject)), "Different " + upsertProp.Name + " value");
                        }
                    }
                    else if ((upsertProp.PropertyType.IsGenericType) && upsertProp.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        IList<PropertyInfo> listProp = upsertProp.PropertyType.GetGenericArguments()[0].GetProperties();
                        IList<PropertyInfo> notNullProps = listProp.Where(x => ((IList)upsertProp.GetValue(upsertObject)).Cast<object>().ToList().Exists(y => x.GetValue(y) != null)).ToList();
                        JArray upJarray = new JArray();
                        JArray getJarray = new JArray();
                        foreach (var upsertObjectItem in (IList)upsertProp.GetValue(upsertObject))
                        {
                            JObject jObject = new JObject();
                            foreach (var prop in notNullProps)
                            {
                                jObject[prop.Name] = JToken.FromObject(prop.GetValue(upsertObjectItem));
                            }
                            upJarray.Add(jObject);
                        }
                        foreach (var getObjectItem in (IList)upsertProp.GetValue(upsertObject))
                        {
                            JObject jObject = new JObject();
                            foreach (var prop in notNullProps)
                            {
                                jObject[prop.Name] = JToken.FromObject(prop.GetValue(getObjectItem));
                            }
                            getJarray.Add(jObject);
                        }
                        Assert.True(JToken.DeepEquals(upJarray, getJarray));


                    }
                    else
                    {
                        if (getProps.FirstOrDefault(x => x.Name == upsertProp.Name) != null)
                        {
                            if (types.Contains(upsertProp.PropertyType))
                            {
                                try
                                {
                                    Assert.True(upsertProp.GetValue(upsertObject).Equals(getProps.First(x => x.Name == upsertProp.Name).GetValue(getObject)), "Different " + upsertProp.Name + " value");
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else
                            {
                                AssertEquals(upsertProp.GetValue(upsertObject), (getProps.First(x => x.Name == upsertProp.Name).GetValue(getObject)));
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //we could use the Type.IsPrimitive flag but the problem with that is that doesn't look the nullables fields like int?, i found easier this way
        static Type[] types = new Type[]
             {
                    typeof(int),typeof(int?),typeof(string),typeof(DateTime),typeof(DateTime?),typeof(float),typeof(decimal),typeof(float?),typeof(decimal?)
             };

        protected static async Task<string> PostWithResponse(HttpClient client, string api, object body)
        {
            try
            {
                string json = JsonConvert.SerializeObject(body);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/" + api, httpContent);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Could not post in " + api + ".");
                return (await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected static async Task<string> PutWithResponse(HttpClient client, string api, object body)
        {
            try
            {
                string json = JsonConvert.SerializeObject(body);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync("api/" + api, httpContent);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Could not post in " + api + ".");
                return (await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected static async Task Post(HttpClient client, string api, object body)
        {
            try
            {
                string json = JsonConvert.SerializeObject(body);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/" + api, httpContent);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Could not post in " + api + ".");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected static async Task<string> Get(HttpClient client, string api)
        {
            try
            {
                var response = await client.GetAsync("api/" + api);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Could not get in " + api + ".");
                return (await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected static async Task<string> Delete(HttpClient client, string api)
        {
            try
            {
                var response = await client.DeleteAsync("api/" + api);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Could not get in " + api + ".");
                return (await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}
