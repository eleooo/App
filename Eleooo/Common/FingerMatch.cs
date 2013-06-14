using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.InteropServices;

namespace Eleooo.Common
{
    public class FingerMatch
    {
        [DllImport("ARTH_DLL.dll")]
        private static extern int Match2Fp(ref byte Src, ref byte Dst);

        private static byte[] HexStringToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static bool Match2Fp(string srcFinger, string destFinger)
        {
            if (string.IsNullOrEmpty(srcFinger) || string.IsNullOrEmpty(destFinger))
                return false;
            byte[] src = HexStringToByte(srcFinger);
            byte[] dest = HexStringToByte(destFinger);
            return Match2Fp(ref src[0], ref dest[0]) > 50;
        }
    }
}