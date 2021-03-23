using Rocket.API;
using System.Collections.Generic;

namespace OfflineAdmins
{
    public class Config : IRocketPluginConfiguration
    {
        public List<Admin> Admins = new List<Admin>();
        public void LoadDefaults()
        {
            
        }
    }
}
