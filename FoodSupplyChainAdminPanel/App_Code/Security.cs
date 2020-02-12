using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FoodSupplyChainAdminPanel.App_Code
{
    public static class FSCSecurity
    {
        #region Get IP Address
        public static string GetIPAddress()
        {
            string VisitorsIPAddr = string.Empty;
            try
            {
                VisitorsIPAddr = getIPAddress();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VisitorsIPAddr;
        }
        public static string getIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;

            string szRemoteAddr = context.Request.ServerVariables["REMOTE_ADDR"];
            string szXForwardedFor = context.Request.ServerVariables["X_FORWARDED_FOR"];
            string szIP = "";

            if (szXForwardedFor == null)
            {
                szIP = szRemoteAddr;
            }
            else
            {
                szIP = szXForwardedFor;

                if (szIP.IndexOf(",") > 0)
                {
                    string[] arIPs = szIP.Split(',');

                    foreach (string item in arIPs)
                    {
                        if (!IsPrivateIpAddress(item))
                        {
                            return item;
                        }
                    }
                }
            }
            return szIP;
        }
        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }
        #endregion

        #region Get Host Name
        public static string GetHostName(string IP)
        {
            string HostName = string.Empty;
            try
            {
                IPAddress myIP = IPAddress.Parse(IP);
                IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
                List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                if (compName != null && compName.Count >= 1)
                    HostName = compName.First();
                else
                    HostName = string.Empty;
            }
            catch (Exception exc)
            {
                HostName = string.Empty;
            }
            return HostName;
        }

        #endregion

        #region Get User Identity (Windows or Form Authentication)
        public static string GetUserIdentity()
        {
            string sUserID = HttpContext.Current.User.Identity.Name;

            if (sUserID.Contains("\\"))
            {
                return sUserID.Substring(sUserID.IndexOf("\\") + 1);
            }
            else
                return sUserID;
        }
        #endregion

        #region Encrypt Decrypt
        public static string EncryptPassword(string password)
        {
            Crypt objCrypt = new Crypt();
            return objCrypt.Encrypt(password);
        }
        public static string DecryptPassword(string password)
        {
            Crypt objCrypt = new Crypt();
            return objCrypt.Decrypt(password);
        }
        const string DESKey = "AQWSEDRF";
        const string DESIV = "HGFEDCBA";

        public static string DESDecrypt(string stringToDecrypt)//Decrypt the content
        {

            byte[] key;
            byte[] IV;

            byte[] inputByteArray;
            try
            {

                key = Convert2ByteArray(DESKey);

                IV = Convert2ByteArray(DESIV);

                int len = stringToDecrypt.Length; inputByteArray = Convert.FromBase64String(stringToDecrypt);


                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8; return encoding.GetString(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        public static string DESEncrypt(string stringToEncrypt)// Encrypt the content
        {

            byte[] key;
            byte[] IV;

            byte[] inputByteArray;
            try
            {

                key = Convert2ByteArray(DESKey);

                IV = Convert2ByteArray(DESIV);

                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream(); CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        static byte[] Convert2ByteArray(string strInput)
        {

            int intCounter; char[] arrChar;
            arrChar = strInput.ToCharArray();

            byte[] arrByte = new byte[arrChar.Length];

            for (intCounter = 0; intCounter <= arrByte.Length - 1; intCounter++)
                arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);

            return arrByte;
        }
        #endregion
    }
}

#region CrptoGraphy
public class Crypt
{

    string myKey;
    TripleDESCryptoServiceProvider cryptDES3 = new TripleDESCryptoServiceProvider();
    MD5CryptoServiceProvider cryptMD5Hash = new MD5CryptoServiceProvider();

    public Crypt()
    {
        myKey = "password";
    }

    public string Decrypt(string myString)
    {
        cryptDES3.Key = cryptMD5Hash.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(myKey));
        cryptDES3.Mode = CipherMode.ECB;
        ICryptoTransform desdencrypt = cryptDES3.CreateDecryptor();
        byte[] buff = Convert.FromBase64String(myString);
        return ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
    }

    public string Encrypt(string myString)
    {
        cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey));
        cryptDES3.Mode = CipherMode.ECB;
        ICryptoTransform desdencrypt = cryptDES3.CreateEncryptor();
        var MyASCIIEncoding = new ASCIIEncoding();
        byte[] buff = ASCIIEncoding.ASCII.GetBytes(myString);
        return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
    }

}
#endregion