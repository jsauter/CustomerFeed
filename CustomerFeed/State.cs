using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed
{
    public class State
    {
        private Cache cache;

        public Cache Cache()
        {
            if(cache == null)
            {
                cache = new Cache();
            }

            return cache;            
        }

        public void Logout()
        {
            cache.RemoveUser();
            cache = null;
        }
    }
}
