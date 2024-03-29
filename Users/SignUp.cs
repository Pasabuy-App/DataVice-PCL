﻿using DataVice.Users.Struct;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace DataVice.Users
{
    public class SignUp
    {
        #region Fields
        /// <summary>
        /// Instance of SignUp Class.
        /// </summary>
        private static SignUp instance;
        public static SignUp Instance
        {
            get
            {
                if (instance == null)
                    instance = new SignUp();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public SignUp()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Submit(string username, string email, string fullname, string lastname, string gender, string bday, string country, string province, string city, string brgy, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("un", username);
            dict.Add("em", email);
            dict.Add("fn", fullname);
            dict.Add("ln", lastname);
            dict.Add("gd", gender);
            dict.Add("bd", bday);
            dict.Add("co", country);
            dict.Add("pv", province);
            dict.Add("ct", city);
            dict.Add("bg", brgy);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(Server.host + "/datavice/v1/user/signup", content);
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