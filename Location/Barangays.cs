using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Location
{
    public class Barangays
    {
        #region Fields
        /// <summary>
        /// Instance of Barangays Class.
        /// </summary>
        private static Barangays instance;
        public static Barangays Instance
        {
            get
            {
                if (instance == null)
                    instance = new Barangays();
                return instance;
            }
        }
        #endregion
        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Barangays()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Location(string city_code, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("ctc", city_code);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/v1/location/brgy", content);
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
