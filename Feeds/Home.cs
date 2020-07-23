using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Feeds
{
    public class Home
    {
        #region Fields
        /// <summary>
        /// Instance for Home Class Library.
        /// </summary>
        private static Home instance;
        public static Home Instance
        {
            get
            {
                if (instance == null)
                    instance = new Home();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication for our Backend.
        /// </summary>
        HttpClient client;
        public Home()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void Home(string wp_id, string session_key, Action<bool, string> callback)
        {
            string getRequest = "?";
            getRequest += "wpid" + wp_id;
            getRequest += "$snky" + session_key;
            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/feeds/home" + getRequest);

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
