using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DataVice_PCL.Users.Struct;
using System.Net.Http;

namespace DataVice_PCL.Feeds
{
    public class Profile
    {
        #region Fields
        /// <summary>
        /// Instance for Profile Class.
        /// </summary>
        private static Profile intance;
        public static Profile Instance
        {
            get
            {
                if (intance == null)
                    intance = new Profile();
                return intance;
            }
        }
        #endregion

        #region Constrcutor
        /// <summary>
        /// Web service for communication for our Backend
        /// </summary>
        HttpClient client;
        public Profile()
        {
            client = new HttpClient();
        }
        #endregion

        #region Method
        public async void GetData(string wp_id, string session_key, Action<bool, string> callback)
        {
            string getRequest = "?";
            getRequest += "wpid" + wp_id;
            getRequest += "&snky" + session_key;

            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/feeds/profile" + getRequest);
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
