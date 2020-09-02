using System;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Model;
using System.IO;

namespace DataVice
{
    public class Upload
    {
        #region Fields
        /// <summary>
        /// Instance of Upload file Class.
        /// </summary>
        private static Upload instance;
        public static Upload Instance
        {
            get
            {
                if (instance == null)
                    instance = new Upload();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Upload()
        {
            client = new HttpClient();

        }
        #endregion
        #region Methods
        public async void Image(string wpid, string snky, string img, Action<bool, string> callback)
        {

            // we need to send a request with multipart/form-data
            var multiForm = new MultipartFormDataContent();

            // add API method parameters
            multiForm.Add(new StringContent(wpid), "wpid");
            multiForm.Add(new StringContent(snky), "snky");

            // add file and directly upload it
            FileStream fs = File.OpenRead(img);
            multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));

            var response = await client.PostAsync( DVHost.Instance.BaseDomain + "/datavice/v1/process/upload", multiForm);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                //callback(true, result);
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
