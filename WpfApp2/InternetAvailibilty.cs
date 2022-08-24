﻿using System.Runtime.InteropServices;

namespace WpfApp2
{
    public class InternetAvailability
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);
 
        public static bool IsInternetAvailable( )
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }
    }
}