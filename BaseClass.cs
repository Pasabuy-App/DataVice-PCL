namespace DataVice
{
    public class DVHost
    {
        private static DVHost instance;
        public static DVHost Instance
        {
            get
            {
                if (instance == null)
                    instance = new DVHost();
                return instance;
            }
        }
   
        private bool isInitialized = false;
        private string baseUrl = "http://localhost";
        public string BaseDomain
        {
            get
            {
                return baseUrl + "/wp-json";
            }
        }

        public void Initialized(string url)
        {
            if (!isInitialized)
            {
                baseUrl = url;
                isInitialized = true;
            }
        }

    }
}
