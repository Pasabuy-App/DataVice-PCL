﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Users.Struct;

namespace DataVice.Contacts
{
    public class Update
    {
        #region Fields
        /// <summary>
        /// Instance of Update Contact Class.
        /// </summary>
        private static Update instance;
        public static Update Instance
        {
            get
            {
                if (instance == null)
                    instance = new Update();
                return instance;
            }
        }
        #endregion
        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Update()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Contact(string wp_id, string session_key, string phone, string email, string contact_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("phone", phone);
                dict.Add("email", email);
                dict.Add("ctcid", contact_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(Server.host + "/datavice/v1/contact/update", content);
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
