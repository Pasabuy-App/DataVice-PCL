using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Model;

namespace DataVice
{
    public class Locations
    {
        #region Fields
        /// <summary>
        /// Instance of Location Class with barangay, city, country and province method.
        /// </summary>
        private static Locations instance;
        public static Locations Instance
        {
            get
            {
                if (instance == null)
                    instance = new Locations();
                return instance;
            }
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Locations()
        {
            client = new HttpClient();
        }
        #endregion

        #region Barangay Method
        public async void Barangay(string city_code, string master_key, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("city_code", city_code);
                dict.Add("mkey", master_key);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync( DVHost.Instance.BaseDomain + "/datavice/v1/location/brgy/active", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }

        }
        #endregion

        #region City Method
        public async void City(string province_code, string master_key, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("prov_code", province_code);
                dict.Add("mkey", master_key);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync( DVHost.Instance.BaseDomain  + "/datavice/v1/location/city/active", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }

        }
        #endregion

        #region Country Method
        public async void Country(string master_key, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("mkey", master_key);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync( DVHost.Instance.BaseDomain + "/datavice/v1/location/country/active", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }

        }
        #endregion

        #region Province Method
        public async void Province(string country_code, string master_key, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("country_code", country_code);
                dict.Add("mkey", master_key);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync( DVHost.Instance.BaseDomain + "/datavice/v1/location/province/active", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }

        }
        #endregion

        #region Timezone Method
        public async void Timezone(string country_code, string master_key, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("country_code", country_code);
                dict.Add("mkey", master_key);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/location/timezone", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }

        }
        #endregion
    }
}
