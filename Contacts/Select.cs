using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Contacts
{
    public class Select
    {
        #region Fields
        /// <summary>
        /// Instance of Select Contact By ID Class.
        /// </summary>
        private static Select instance;
        public static Select Instance
        {
            get
            {
                if (instance == null)
                    instance = new Select();
                return instance;
            }
        }
        #endregion
        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Select()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void ByID(string wp_id, string session_key, string cid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("cid", cid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/v1/contact/select", content);
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
