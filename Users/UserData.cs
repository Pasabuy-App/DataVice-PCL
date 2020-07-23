using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;


namespace DataVice_PCL.Users
{
    public class UserData
    {
        #region Fields
        /// <summary>
        /// Instance for User Data Class.
        /// </summary>
        private static UserData instance;
        public static UserData Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserData();
                return instance;
            }
        }
        #endregion
        /// <summary>
        /// Web service for communication for our Backend.
        /// </summary>
        #region Construtor
        HttpClient client;

        public UserData()
        {
            client = new HttpClient();
        }
        public async void GetData(string wp_id, string session_key, Action<bool, string> callback)
        {
            string getRequest = "?";
                getRequest += "wpid=" + wp_id;
                getRequest += "&snky=" + session_key;

            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/user/data" + getRequest);


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
