using RetailSync_Models.DbModels;

namespace RetailSync.Core
{
    public class ApplicationCache
    {
        private static ApplicationCache _instance;

        private ApplicationCache()
        {

        }
        
        public static ApplicationCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationCache();
                }
                return _instance;
            }
        }
        
        public UserModel AppUser { get; set; }

        public void SetAppUser(UserModel user)
        {
            AppUser = user;
        }

    }
}
