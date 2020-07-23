using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Feeds
{
    public class Home_Feeds
    {
        #region Fields
        /// <summary>
        /// Instance for Home_Feeds Class Library.
        /// </summary>
        private static Home_Feeds instance;
        public static Home_Feeds Instance
        {
            get
            {
                if (instance == null)
                    instance = new Home_Feeds();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication for our Backend.
        /// </summary>
        HttpClient client;
        public Home_Feeds()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void GetData(string last_id, string wp_id, string session_key, Action<bool, string> callback)
        {
            string getRequest = "?";
            getRequest += "lid" + last_id;
            getRequest += "&wpid" + wp_id;
            getRequest += "$snky" + session_key;

            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/feeds/home_add_feeds" + getRequest);
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
