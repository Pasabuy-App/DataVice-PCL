using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DataVice_PCL.Users.Struct;
using System.Net.Http;

namespace DataVice_PCL.Activity_Log
{
    public class Activity_Log_ByID
    {
        #region Fields
        /// <summary>
        /// Instance for Act_Log_Fetch_ByID Class.
        /// </summary>
        private static Activity_Log_ByID instance;
        public static Activity_Log_ByID Instance
        {
            get
            {
                if (instance == null)
                    instance = new Activity_Log_ByID();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communcation for our Backend.
        /// </summary>
        HttpClient client;
        public Activity_Log_ByID()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void Create(string wpid, string session_key, string activity_id, Action<bool, string> callback)
        {
            var getRequest = "?";
            getRequest += "wpid" + wpid;
            getRequest += "&snky" + session_key;
            getRequest += "$atid"+ activity_id;


            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/activity/get_activity_byid" + getRequest);
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
