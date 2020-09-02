using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Model;

namespace DataVice
{
    public class User
    {
        #region Fields 
        /// <summary>
        /// Instance of User Class with authentication, forgot password, reset password, signup, profile and verify method.
        /// </summary>
        private static User instance;
        public static User Instance
        {
            get
            {
                if (instance == null)
                    instance = new User();
                return instance;
            }
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public User()
        {
            client = new HttpClient();
        }
        #endregion

        #region Auth Method
        public async void Auth(string username, string password, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("un", username);
                dict.Add("pw", password);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync( DVHost.Instance.BaseDomain + "/datavice/v1/user/auth", content);
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

        #region Forgot Method
        public async void Forgot(string username, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("un", username);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/forgot", content);
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

        #region Reset Method
        public async void Reset(string activation_key, string username, string password, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("ak", activation_key);
                dict.Add("un", username);
                dict.Add("pw", password);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync( DVHost.Instance.BaseDomain + "/datavice/v1/user/reset", content);
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

        #region SignUp Method
        public async void SignUp(string username, string email, string firstname, string lastname, string gender, string bday, 
                string country, string province, string city, string brgy, string street, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("un", username);
                dict.Add("em", email);
                dict.Add("fn", firstname);
                dict.Add("ln", lastname);
                dict.Add("gd", gender);
                dict.Add("bd", bday);
                dict.Add("co", country);
                dict.Add("pv", province);
                dict.Add("ct", city);
                dict.Add("bg", brgy);
                dict.Add("st", street);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/signup", content);
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

        #region UserData Method
        public async void Profile(string wp_id, string session_key, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync( DVHost.Instance.BaseDomain + "/datavice/v1/user/profile", content);
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

        #region Verify Method
        public async void Verify(string wpid, string snky, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wpid);
                dict.Add("snky", snky);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync( DVHost.Instance.BaseDomain + "/datavice/v1/user/verify", content);
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
