using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice_PCL.Users.Struct;

namespace DataVice_PCL.Users
{
    public class SignUp
    {
        #region Fields
        /// <summary>
        /// Instance of SignUp Class.
        /// </summary>
        private static SignUp instance;
        public static SignUp Instance
        {
            get
            {
                if (instance == null)
                    instance = new SignUp();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public SignUp()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Submit(string username, string email, string fullname, string lastname, string gender, string province, string city, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("un", username);
                dict.Add("em", email);
                dict.Add("fn", fullname);
                dict.Add("ln", lastname);
                dict.Add("gd", gender);
                dict.Add("pr", province);
                dict.Add("ct", city);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/user/signup", content);
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