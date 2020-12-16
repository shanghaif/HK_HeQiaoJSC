using com.sun.org.apache.xerces.@internal.impl.dv.util;
using java.security;
using java.security.interfaces;
using java.security.spec;
using javax.crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils
{
    public static class RSACryption
    {
        #region RSA 加密解密
        #region RSA 的密钥产生
        /// <summary>
        /// RSA产生密钥
        /// </summary>
        /// <param name="xmlKeys">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        public static void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                xmlKeys = rsa.ToXmlString(true);
                xmlPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA加密函数
        //############################################################################## 
        //RSA 方式加密 
        //KEY必须是XML的形式,返回的是字符串 
        //该加密方式有长度限制的！
        //############################################################################## 

        /// <summary>
        /// RSA的加密函数
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns></returns>
        public static string RSAEncrypt(string xmlPublicKey, string encryptString)
        {
            try
            {
                xmlPublicKey = @"<RSAKeyValue><Modulus>" + xmlPublicKey + "</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
                byte[] PlainTextBArray;
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                PlainTextBArray = (new UnicodeEncoding()).GetBytes(encryptString);
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA的加密函数 
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="EncryptString">待加密的字节数组</param>
        /// <returns></returns>
        public static string RSAEncrypt(string xmlPublicKey, byte[] EncryptString)
        {
            try
            {
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                CypherTextBArray = rsa.Encrypt(EncryptString, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA的解密函数        
        /// <summary>
        /// RSA的解密函数
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns></returns>
        public static string RSADecrypt(string xmlPrivateKey, string decryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                PlainTextBArray = Convert.FromBase64String(decryptString);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA的解密函数 
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="DecryptString">待解密的字节数组</param>
        /// <returns></returns>
        public static string RSADecrypt(string xmlPrivateKey, byte[] DecryptString)
        {
            try
            {
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                DypherTextBArray = rsa.Decrypt(DecryptString, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region java转c#Rsa加密
        public static string RsaEncryptJava(string publicKey,string serct)
        {
            var a = Base64.decode(publicKey);

            X509EncodedKeySpec keySpec = new X509EncodedKeySpec(a);
            KeyFactory keyFactory = KeyFactory.getInstance("RSA");
            RSAPublicKey publicKey1 = (RSAPublicKey)keyFactory.generatePublic(keySpec);


            Cipher cipher = Cipher.getInstance("RSA");
            cipher.init(Cipher.ENCRYPT_MODE, publicKey1);
            // 模长  
            int key_len = publicKey1.getModulus().bitLength() / 8;
            // 加密数据长度 <= 模长-11  
            string[] datas = splitString(serct, key_len - 11);
            string mi = "";
            //如果明文长度大于模长-11则要分组加密  
            foreach (string s in datas)
            {
                mi += bcd2Str(cipher.doFinal(System.Text.Encoding.UTF8.GetBytes(s)));
            }
            return mi;
        }
        public static string bcd2Str(byte[] bytes)
        {
            char[] temp = new char[bytes.Length * 2];
            char val;

            for (int i = 0; i < bytes.Length; i++)
            {
                val = (char)(((bytes[i] & 0xf0) >> 4) & 0x0f);
                temp[i * 2] = (char)(val > 9 ? val + 'A' - 10 : val + '0');

                val = (char)(bytes[i] & 0x0f);
                temp[i * 2 + 1] = (char)(val > 9 ? val + 'A' - 10 : val + '0');
            }
            return new string(temp);
        }
        public static string[] splitString(string str, int len)
        {
            int x = str.Length / len;
            int y = str.Length % len;
            int z = 0;
            if (y != 0)
            {
                z = 1;
            }
            string[] strings = new string[x + z];
            string str1 = "";
            for (int i = 0; i < x + z; i++)
            {
                if (i == x + z - 1 && y != 0)
                {
                    str1 = str.Substring(i * len, i * len + y);
                }
                else
                {
                    str1 = str.Substring(i * len, i * len + len);
                }
                strings[i] = str1;
            }
            return strings;
        }
        #endregion
    }
}
