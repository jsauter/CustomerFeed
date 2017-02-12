using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed.Models.Comparers
{
    public class LogTimeStampDescendingComparer : IComparer<LogModel>
    {
        public int Compare(LogModel x, LogModel y)
        {
            if (x.LogTimeStamp == null)
            {
                if (y.LogTimeStamp == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y.LogTimeStamp == null)
                // ...and y is null, x is greater.
                {
                    return -1;
                }
                else
                {
                    if (x.LogTimeStamp > y.LogTimeStamp)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return -1;
                    }
                    else if (x.LogTimeStamp < y.LogTimeStamp)
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return 1;
                    }
                    
                    return 0;
                }
            }
        }
    }
}
