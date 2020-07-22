using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;


namespace DataVice_PCL.Users
{
    public class Auth
    {
        private static Auth instance;

        /// <summary>
        /// Instance of Authentication Class.
        /// </summary>
        public static Auth Instance
        {
            get
            {
                if (instance == null)
                    instance = new Auth();
                return instance;
            }
        }

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;

        #region Constructor
        public Auth()
        {
            client = new HttpClient();
        }
        #endregion

        #region Methods
        public async void Submit(string username, string password, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("UN", username);
                dict.Add("PW", password);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/user/auth", content);
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
