using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Model;

namespace DataVice
{
    public class Address
    {
        #region Fields
        /// <summary>
        /// Instance of Address Class with insert, update, delete, list, select type and id method.
        /// </summary>
        private static Address instance;
        public static Address Instance
        {
            get
            {
                if (instance == null)
                    instance = new Address();
                return instance;
            }
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Address()
        {
            client = new HttpClient();
        }
        #endregion

        #region Delete Method
        public async void Delete(string wp_id, string session_key, string id, string type,Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("id", id);
                dict.Add("type", type);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/address/delete", content);
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

        #region Insert Method
        public async void Insert(string wp_id, string session_key, string type, string co, string pv, string ct, string bg, string st, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("type", type);
                dict.Add("co", co);
                dict.Add("pv", pv);
                dict.Add("ct", ct);
                dict.Add("bg", bg);
                dict.Add("st", st);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/address/insert", content);
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

        #region List Method
        public async void List(string wp_id, string session_key, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/address/list/all", content);
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

        #region SelectByID Method
        public async void SelectByID(string wp_id, string session_key, string address_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("address_id", address_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/address/select", content);
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

        #region SelectByType Method
        public async void SelectByType(string wp_id, string session_key, string address_type, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("address_type", address_type);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/address/list/type", content);
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
        public async void Update(string wp_id, string session_key, string id, string co, string pv, string ct, string bg, string st, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("id", id);
                dict.Add("co", co);
                dict.Add("pv", pv);
                dict.Add("ct", ct);
                dict.Add("bg", bg);
                dict.Add("st", st);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/address/update", content);
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
