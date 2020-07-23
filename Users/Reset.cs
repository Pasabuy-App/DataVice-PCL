using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Users
{
    public class Reset
    {
        #region Fields
        /// <summary>
        /// Instance for Change Password Class.
        /// </summary>
        private static Reset instance;
        public static Reset Instance
        {
            get
            {
                if (instance == null)
                    instance = new Reset();
                    return instance;
            }
        }
        #endregion
        #region Consructor
        /// <summary>
        /// Web service for comunication for our Backend.
        /// </summary>

        HttpClient client;

        public Reset()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void Submit(string activation_key, string password, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("ak", activation_key);
                dict.Add("pw", password);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/user/reset", content);
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
