using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Users
{
    public class Forgot
    {
        #region Fields
        /// <summary>
        /// Instance of Forgot Password Class.
        /// </summary>
        private static Forgot instance;
        public static Forgot Instance
        {
            get
            {
                if (instance == null)
                    instance = new Forgot();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Forgot()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Submit(string username, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("un", username);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/v1/user/forgot", content);
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
