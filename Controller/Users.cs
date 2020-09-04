using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Model;

namespace DataVice
{
    public class Users
    {
        #region Fields 
        /// <summary>
        /// Instance of User Class with authentication, forgot password, reset password, signup, profile and verify method.
        /// </summary>
        private static Users instance;
        public static Users Instance
        {
            get
            {
                if (instance == null)
                    instance = new Users();
                return instance;
            }
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Users()
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

        #region Edit Profile Method
        public async void EditProfile(string wpid, string snky, string fn, string ln, string dpn, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wpid);
                dict.Add("snky", snky);
                dict.Add("fn", fn);
                dict.Add("ln", ln);
                dict.Add("dpn", dpn);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/edit", content);
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

        #region Change Password Method
        public async void ChangePassword(string wpid, string snky, string pas, string npas, string cpas, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wpid);
                dict.Add("snky", snky);
                dict.Add("pas", pas);
                dict.Add("npas", npas);
                dict.Add("cpas", cpas);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/password/edit", content);
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

        #region New Password Method
        public async void NewPassword(string ak, string un, string npas, string cpas, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("ak", ak);
                dict.Add("un", un);
                dict.Add("npas", npas);
                dict.Add("cpas", cpas);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/activate/verify", content);
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

        #region Activate Account Method
        public async void Activate(string ak, string un, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("ak", ak);
                dict.Add("un", un);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/activate", content);
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
