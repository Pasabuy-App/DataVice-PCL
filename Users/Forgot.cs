using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Users
{
    class Forgot
    {
        #region Fields

        /// <summary>
        /// Instance for Forgot Password Class.
        /// </summary>
        private Forgot instance;
        public Forgot Instance
        {
            get
            {
                if (instance == null)
                    instance = new Forgot();
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// Web service for communication for our Backend.
        /// </summary>

        HttpClient client;

        #region Constructor
        public Forgot()
        {
            client = new HttpClient();
        }
        public async void Submit(string username, string password, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("UN", username);
                dict.Add("PW", password);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "datavice/api/v1/user/forgot", content);

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
