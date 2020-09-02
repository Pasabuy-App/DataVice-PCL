using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Model;

namespace DataVice
{
    public class Coordinates
    {
        #region Fields
        /// <summary>
        /// Instance of Coordinates Class with insert, update method.
        /// </summary>
        private static Coordinates instance;
        public static Coordinates Instance
        {
            get
            {
                if (instance == null)
                    instance = new Coordinates();
                return instance;
            }
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Coordinates()
        {
            client = new HttpClient();
        }
        #endregion

        #region Insert Method
        public async void Insert(string wp_id, string session_key, string lat, string lon, string addr, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("lat", lat);
                dict.Add("lon", lon);
                dict.Add("addr", addr);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/coordinates/insert", content);
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

        #region Update Method
        public async void Update(string wp_id, string session_key, string lat, string lon, string addr, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("lat", lat);
            dict.Add("lon", lon);
            dict.Add("addr", addr);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/coordinates/update", content);
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
