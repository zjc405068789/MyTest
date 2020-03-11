using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cms.Common.Util
{
    public class CryptoHelper
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DESDecrypt(string text, string sKey = "Cms")
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                int len = text.Length / 2;
                var inputByteArray = new byte[len];
                int x;
                for (x = 0; x < len; x++)
                {
                    int i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                var hashPasswordForStoringInConfigFile = MD5Hash32(sKey);
                if (hashPasswordForStoringInConfigFile != null)
                    des.Key = Encoding.ASCII.GetBytes(hashPasswordForStoringInConfigFile.Substring(0, 8));
                var passwordForStoringInConfigFile = MD5Hash32(sKey);
                if (passwordForStoringInConfigFile != null)
                    des.IV = Encoding.ASCII.GetBytes(passwordForStoringInConfigFile.Substring(0, 8));
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());

            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DESEncrypt(string text, string sKey = "Cms")
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(text);
                var hashPasswordForStoringInConfigFile = MD5Hash32(sKey);
                if (hashPasswordForStoringInConfigFile != null)
                    des.Key = Encoding.ASCII.GetBytes(hashPasswordForStoringInConfigFile.Substring(0, 8));
                var passwordForStoringInConfigFile = MD5Hash32(sKey);
                if (passwordForStoringInConfigFile != null)
                    des.IV = Encoding.ASCII.GetBytes(passwordForStoringInConfigFile.Substring(0, 8));
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        public static string MD5Hash32(string str)
        {
            string buf = "";
            MD5 md5 = MD5.Create();

            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            for (int i = 0; i < s.Length; i++)
            {
                buf = buf + s[i].ToString("X");
            }
            return buf;
        }

        #region  MD5加密

        /// <summary>
        /// 标准MD5加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <param name="addKey">附加字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string MD5_Encrypt(string source, string addKey, Encoding encoding)
        {
            if (addKey.Length > 0)
            {
                source = source + addKey;
            }

            MD5 MD5 = new MD5CryptoServiceProvider();
            byte[] datSource = encoding.GetBytes(source);
            byte[] newSource = MD5.ComputeHash(datSource);
            string byte2String = null;
            for (int i = 0; i < newSource.Length; i++)
            {
                string thisByte = newSource[i].ToString("x");
                if (thisByte.Length == 1) thisByte = "0" + thisByte;
                byte2String += thisByte;
            }
            return byte2String;
        }

        /// <summary>
        /// 标准MD5加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string MD5_Encrypt(string source, string encoding)
        {
            return MD5_Encrypt(source, string.Empty, Encoding.GetEncoding(encoding));
        }

        /// <summary>
        /// 标准MD5加密
        /// </summary>
        /// <param name="source">被加密的字符串</param>
        /// <returns></returns>
        public static string MD5_Encrypt(string source)
        {
            return MD5_Encrypt(source, string.Empty, Encoding.Default);
        }

        #endregion
    }
}
