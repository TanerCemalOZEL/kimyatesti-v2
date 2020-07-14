using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace kimyatesti.Identity
{
    public static class CryptoService
    {
        public static string Sifrele(this string strQueryStringParameter)
        {
            MD5CryptoServiceProvider hash_func = new MD5CryptoServiceProvider();
            byte[] key = hash_func.ComputeHash(Encoding.ASCII.GetBytes("printHelloWorld"));
            byte[] IV = new byte[8];
            SHA1CryptoServiceProvider sha_func = new SHA1CryptoServiceProvider();
            byte[] temp = sha_func.ComputeHash(Encoding.ASCII.GetBytes("printHelloWorld"));
            for (int i = 0; i < 8; i++)
                IV[i] = temp[i];
            byte[] toenc = System.Text.Encoding.UTF8.GetBytes(strQueryStringParameter);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.KeySize = 192;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(toenc, 0, toenc.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToBase64String(ms.ToArray())));
        }

        public static string SifreCoz(this string strQueryStringParameter)
        {
            MD5CryptoServiceProvider hash_func = new MD5CryptoServiceProvider();
            byte[] key = hash_func.ComputeHash(Encoding.ASCII.GetBytes("printHelloWorld"));
            byte[] IV = new byte[8];
            SHA1CryptoServiceProvider sha_func = new SHA1CryptoServiceProvider();
            byte[] temp = sha_func.ComputeHash(Encoding.ASCII.GetBytes("printHelloWorld"));
            for (int i = 0; i < 8; i++)
                IV[i] = temp[i];
            byte[] todec = Convert.FromBase64String(Encoding.UTF8.GetString(Convert.FromBase64String(strQueryStringParameter)));
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.KeySize = 192;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(todec, 0, todec.Length);
            cs.FlushFinalBlock();
            return System.Text.Encoding.UTF8.GetString(ms.ToArray());

        }
    }
}