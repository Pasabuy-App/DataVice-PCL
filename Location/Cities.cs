using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Location
{
    public class Cities
    {
        #region Fields
        /// <summary>
        /// Instance of Cities Class.
        /// </summary>
        private static Cities instance;
        public static Cities Instance
        {
            get
            {
                if (instance == null)
                    instance = new Cities();
                return instance;
            }
        }
        #endregion
        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Cities()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Location(string province_code,  Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("pc", province_code);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/v1/location/cty", content);
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
