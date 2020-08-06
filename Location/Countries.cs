using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Users.Struct;

namespace DataVice.Location
{
    public class Countries
    {
        #region Fields
        /// <summary>
        /// Instance of Countries Class.
        /// </summary>
        private static Countries instance;
        public static Countries Instance
        {
            get
            {
                if (instance == null)
                    instance = new Countries();
                return instance;
            }
        }
        #endregion
        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Countries()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Location( Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(Server.host + "/datavice/v1/location/ctry", content);
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
