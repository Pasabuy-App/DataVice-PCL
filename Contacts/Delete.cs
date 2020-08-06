using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Contacts
{
    public class Delete
    {
        #region Fields
        /// <summary>
        /// Instance of Delete Contact Class.
        /// </summary>
        private static Delete instance;
        public static Delete Instance
        {
            get
            {
                if (instance == null)
                    instance = new Delete();
                return instance;
            }
        }
        #endregion
        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Delete()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Contact(string wp_id, string session_key, string contact_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("ctcid", contact_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/v1/contact/delete", content);
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
