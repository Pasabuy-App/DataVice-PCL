using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Location
{
    public class Provinces
    {
        #region Fields
        /// <summary>
        /// Instance of Provinces Class.
        /// </summary>
        private static Provinces instance;
        public static Provinces Instance
        {
            get
            {
                if (instance == null)
                    instance = new Provinces();
                return instance;
            }
        }
        #endregion
        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Provinces()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Location(string country_code, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("cc", country_code);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/v1/location/prv", content);
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
