using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;
using System.Linq;

namespace DataVice_PCL.Users
{
    class Reset
    {
        #region Fields
        /// <summary>
        /// Instance for Change Password Class.
        /// </summary>
        private Reset instance;
        public Reset Instance
        {
            get
            {
                if (instance == null)
                    instance = new Reset();
                    return instance;
            }
        }

        /// <summary>
        /// Web service for comunication for our Backend.
        /// </summary>

        HttpClient client;

        #endregion

        #region Consructor
        public Reset()
        {
            client = new HttpClient();
        }
        public async void Submit(string activation_key, string password, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("AK", activation_key);
                dict.Add("PW", password);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/user/reset", content);
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
