using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DataVice_PCL.Users.Struct;
using System.Net.Http;

namespace DataVice_PCL.Activity_Log
{
    public class Activity_Log_Fetch_Feed
    {
        #region Fields
        /// <summary>
        /// Instance for Activity_Log_Fetch Class.
        /// </summary>
        private static Activity_Log_Fetch_Feed instance;
        public static Activity_Log_Fetch_Feed Instance
        {
            get
            {
                if (instance == null)
                    instance = new Activity_Log_Fetch_Feed();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communcation for our Backend.
        /// </summary>
        HttpClient client;
        public Activity_Log_Fetch_Feed()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void GetData(string wpid, string session_key, string last_id, Action<bool, string> callback)
        {
            string getRequest = "?";
            getRequest += "wpid" + wpid;
            getRequest += "&snky" + session_key;
            getRequest += "&lid" + last_id;

            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/activity/get_activity_feed");
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
