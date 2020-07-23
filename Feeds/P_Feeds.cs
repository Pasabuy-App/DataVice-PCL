using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DataVice_PCL.Users.Struct;
using System.Net.Http;

namespace DataVice_PCL.Feeds
{
    public class P_Feeds
    {
        #region Fields
        /// <summary>
        /// Instance for Additional Feeds Class.
        /// </summary>
        private static P_Feeds instance;
        public static P_Feeds Instance
        {
            get
            {
                if (instance == null)
                    instance = new P_Feeds();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication for our Backend.
        /// </summary>
        HttpClient client;
        public P_Feeds()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void GetData(string wp_id, string last_min_id, string session_key, Action<bool, string> callback)
        {
            string getRequest = "?";
            getRequest += "wpid" + wp_id;
            getRequest += "&lid" + last_min_id;
            getRequest += "&snky" + session_key;

            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/feeds/p_feeds" + getRequest);
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
