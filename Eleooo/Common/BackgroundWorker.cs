using System;
using System.Collections.Generic;
using System.Web;

namespace Eleooo.Common
{
    public static class BackgroundWorker
    {
        public static void DoWork<T>(T state, Action<T> working)
        {
            //string key, object value, CacheItemRemovedReason reason
            if (working == null)
                return;
            HttpContext.Current.Cache.Add(
                Guid.NewGuid( ).ToString( ),
                state,
                null,
                DateTime.Now.AddSeconds(1),
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.High,
                (key, value, reason) =>
                {
                    working(state);
                });
        }
        public static void DoWork<T>(T state, int nDelay, Action<T> working)
        {
            //string key, object value, CacheItemRemovedReason reason
            if (working == null)
                return;
            HttpRuntime.Cache.Add(
                Guid.NewGuid( ).ToString( ),
                state,
                null,
                DateTime.Now.AddSeconds(nDelay),
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.High,
                (key, value, reason) =>
                {
                    working(state);
                });
        }
    }
}