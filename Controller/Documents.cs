using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Model;
using System.IO;

namespace DataVice
{
    public class Documents
    {
        #region Fields 
        /// <summary>
        /// Instance of Documents Class with insert, list and approve method.
        /// </summary>
        private static Documents instance;
        public static Documents Instance
        {
            get
            {
                if (instance == null)
                    instance = new Documents();
                return instance;
            }
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Documents()
        {
            client = new HttpClient();
        }
        #endregion

        #region Insert Method
        public async void Insert(string wpid, string snky, string type, string doctype, string img, Action<bool, string> callback)
        {
            var multiForm = new MultipartFormDataContent();

            multiForm.Add(new StringContent(wpid), "wpid");
            multiForm.Add(new StringContent(snky), "snky");
            multiForm.Add(new StringContent(type), "type");
            if (doctype != "") { multiForm.Add(new StringContent(doctype), "doctype"); }

            FileStream fs = File.OpenRead(img);
            multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/documents/insert", multiForm);
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

        #region Approve Method
        public async void Approve(string wpid, string snky, string status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wpid);
                dict.Add("snky", snky);
                dict.Add("status", status);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/documents/approve", content);
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

        #region List Method
        public async void List(string wpid, string snky, string status, string docid, string user_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wpid);
                dict.Add("snky", snky);
                if (status != "") { dict.Add("status", status); }
                if (docid != "") { dict.Add("docid", docid); }
                if (user_id != "") { dict.Add("user_id", user_id); }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(DVHost.Instance.BaseDomain + "/datavice/v1/user/documents/list", content);
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
