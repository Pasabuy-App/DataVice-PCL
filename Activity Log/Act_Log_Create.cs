using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DataVice_PCL.Users.Struct;
using System.Net.Http;

namespace DataVice_PCL.Activity_Log
{
    public class Activity_Log
    {
        #region Fields
        /// <summary>
        /// Instance for Act_Log_Create Class.
        /// </summary>
        private static Activity_Log instance;
        public static Activity_Log Instance
        {
            get
            {
                if (instance == null)
                    instance = new Activity_Log();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communcation for our Backend.
        /// </summary>
        HttpClient client;
        public Activity_Log()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void Create(string wpid, string session_key, string stid, string icon, string title, string info, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wpid);
            dict.Add("snky", session_key);
            dict.Add("stid", stid);
            dict.Add("icon", icon);
            dict.Add("title", title);
            dict.Add("info", info);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/activity/dv_activity", content);
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
