using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace HaikanYixueyuanDog.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RSAUtils
    {
        /// <summary>
        /// 分段RSA加密
        /// </summary>
        /// <param name="content">要加密的内容</param>
        /// <param name="publicKey">此公钥为钱包公钥</param>
        /// <returns></returns>
        public static string RSAEncryptByPublicKey(string content, string publicKey)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(publicKey)) return string.Empty;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(content);
            content = Convert.ToBase64String(byteArray);
            rsa.FromXmlString(publicKey);
            byte[] OriginalData = Convert.FromBase64String(content);

            if (OriginalData == null || OriginalData.Length <= 0)
            {
                throw new NotSupportedException();
            }
            if (rsa == null)
            {
                throw new ArgumentNullException();
            }
            int bufferSize = (rsa.KeySize / 8) - 37;
            byte[] buffer = new byte[bufferSize];
            //分段加密
            using (MemoryStream input = new MemoryStream(OriginalData))
            using (MemoryStream ouput = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, bufferSize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] encrypt = rsa.Encrypt(temp, false);
                    ouput.Write(encrypt, 0, encrypt.Length);
                }
                return Convert.ToBase64String(ouput.ToArray());
            }
        }

        /// <summary>
        /// 分段RSA加密
        /// </summary>
        /// <param name="content">要加密的内容</param>
        /// <param name="privateKey">此公钥为钱包公钥</param>
        /// <returns></returns>
        public static string RSAEncryptByPrivateKey(string content, string privateKey)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(privateKey)) return string.Empty;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(content);
            content = Convert.ToBase64String(byteArray);
            rsa.FromXmlString(privateKey);
            byte[] OriginalData = Convert.FromBase64String(content);

            if (OriginalData == null || OriginalData.Length <= 0)
            {
                throw new NotSupportedException();
            }
            if (rsa == null)
            {
                throw new ArgumentNullException();
            }
            int bufferSize = (rsa.KeySize / 8) - 37;
            byte[] buffer = new byte[bufferSize];
            //分段加密
            using (MemoryStream input = new MemoryStream(OriginalData))
            using (MemoryStream ouput = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, bufferSize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] encrypt = rsa.Encrypt(temp, false);
                    ouput.Write(encrypt, 0, encrypt.Length);
                }
                return Convert.ToBase64String(ouput.ToArray());
            }
        }
        /// <summary>
        /// 分段RSA解密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSADecrypt(string privateKey, string content)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(privateKey)) return string.Empty;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            byte[] EncryptDada = Convert.FromBase64String(content);

            if (EncryptDada == null || EncryptDada.Length <= 0)
            {
                throw new NotSupportedException();
            }
            rsa.FromXmlString(privateKey);
            int keySize = rsa.KeySize / 8;
            byte[] buffer = new byte[keySize];

            using (MemoryStream input = new MemoryStream(EncryptDada))
            using (MemoryStream output = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, keySize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] decrypt = rsa.Decrypt(temp, false);
                    output.Write(decrypt, 0, decrypt.Length);
                }
                return Encoding.UTF8.GetString(output.ToArray());
            }
        }

    }
    public sealed class RSAFromPkcs1
    {
        /** 默认编码字符集 */
        private static string DEFAULT_CHARSET = "UTF-8";
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="content">需要签名的内容</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <param name="signType">签名方式(RSA:密钥长度为1024,RSA2:密钥长度为2048)</param>
        /// <returns></returns>
        public static string SignByPrivateKey(string content, string privateKey, string inputCharset, string signType = "RSA")
        {
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            Encoding encoding = Encoding.GetEncoding(inputCharset);
            byte[] contentBytes = encoding.GetBytes(content);
            byte[] signedBytes = null;
            using (RSACryptoServiceProvider rsaProvider = DecodePemPrivateKey(privateKey))
            {
                if (signType == "RSA2")
                {
                    signedBytes = rsaProvider.SignData(contentBytes, "SHA256");
                }
                else
                {
                    signedBytes = rsaProvider.SignData(contentBytes, "SHA1");
                }
            }
            return Convert.ToBase64String(signedBytes);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="content">需要验证的内容</param>
        /// <param name="signedString">签名结果</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <param name="signType">签名方式(RSA:密钥长度为1024,RSA2:密钥长度为2048)</param>
        /// <returns></returns>
        public static bool VerifyByPublicKey(string content, string signedString, string publicKey, string inputCharset, string signType = "RSA")
        {
            bool result = false;
            if (!string.IsNullOrEmpty(signedString))
            {
                signedString = signedString.Replace("%2B", "+");
                signedString = signedString.Replace(" ", "+");
            }
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            Encoding encoding = Encoding.GetEncoding(inputCharset);
            byte[] contentBytes = encoding.GetBytes(content);
            byte[] signedBytes = Convert.FromBase64String(signedString);
            using (RSACryptoServiceProvider rsaProvider = DecodePemPublicKey(publicKey))
            {
                rsaProvider.PersistKeyInCsp = false;
                if (signType == "RSA2")
                {
                    result = rsaProvider.VerifyData(contentBytes, "SHA256", signedBytes);
                }
                else
                {
                    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                    result = rsaProvider.VerifyData(contentBytes, sha1, signedBytes);
                }
            }
            return result;
        }

        /// <summary>
        /// RSA公钥加密
        /// </summary>
        /// <param name="content">需要签名的内容</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns></returns>
        public static string Encrypt(string content, string publicKey, string inputCharset)
        {
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            Encoding encoding = Encoding.GetEncoding(inputCharset);
            using (RSACryptoServiceProvider rsaProvider = DecodePemPublicKey(publicKey))
            {
                rsaProvider.PersistKeyInCsp = false;
                byte[] data = Encoding.GetEncoding(inputCharset).GetBytes(content);
                int maxBlockSize = rsaProvider.KeySize / 8 - 11; //加密块最大长度限制
                if (data.Length <= maxBlockSize)
                {
                    byte[] cipherbytes = rsaProvider.Encrypt(data, false);
                    return Convert.ToBase64String(cipherbytes);
                }
                MemoryStream plaiStream = new MemoryStream(data);
                MemoryStream crypStream = new MemoryStream();
                Byte[] buffer = new Byte[maxBlockSize];
                int blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
                while (blockSize > 0)
                {
                    Byte[] toEncrypt = new Byte[blockSize];
                    Array.Copy(buffer, 0, toEncrypt, 0, blockSize);
                    Byte[] cryptograph = rsaProvider.Encrypt(toEncrypt, false);
                    crypStream.Write(cryptograph, 0, cryptograph.Length);
                    blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
                }
                return Convert.ToBase64String(crypStream.ToArray(), Base64FormattingOptions.None);
            }

        }

        /// <summary>
        /// 用RSA私钥解密
        /// </summary>
        /// <param name="resData">待解密字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>解密结果</returns>
        public static string Decrypt(string resData, string privateKey, string inputCharset)
        {
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            RSACryptoServiceProvider rsaCsp = DecodePemPrivateKey(privateKey);
            byte[] data = Convert.FromBase64String(resData);
            int maxBlockSize = rsaCsp.KeySize / 8; //解密块最大长度限制
            if (data.Length <= maxBlockSize)
            {
                byte[] cipherbytes = rsaCsp.Decrypt(data, false);
                return Encoding.GetEncoding(inputCharset).GetString(cipherbytes);
            }
            MemoryStream crypStream = new MemoryStream(data);
            MemoryStream plaiStream = new MemoryStream();
            Byte[] buffer = new Byte[maxBlockSize];
            int blockSize = crypStream.Read(buffer, 0, maxBlockSize);
            while (blockSize > 0)
            {
                Byte[] toDecrypt = new Byte[blockSize];
                Array.Copy(buffer, 0, toDecrypt, 0, blockSize);
                Byte[] cryptograph = rsaCsp.Decrypt(toDecrypt, false);
                plaiStream.Write(cryptograph, 0, cryptograph.Length);
                blockSize = crypStream.Read(buffer, 0, maxBlockSize);
            }
            return Encoding.GetEncoding(inputCharset).GetString(plaiStream.ToArray());
        }
        /// <summary>
        /// Decodes the RSA private key.
        /// </summary>
        /// <param name="privkey">The privkey.</param>
        /// <returns>RSACryptoServiceProvider.</returns>
        private static RSACryptoServiceProvider DecodePemPrivateKey(string privateKey)
        {
            try
            {
                // ------- create RSACryptoServiceProvider instance and initialize with private key -----  
                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                rsaProvider.PersistKeyInCsp = false;
                var rsaParameters = ConvertFromPrivateKey(privateKey);
                rsaProvider.ImportParameters(rsaParameters);
                return rsaProvider;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Decodes the pem public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <returns>RSACryptoServiceProvider.</returns>
        private static RSACryptoServiceProvider DecodePemPublicKey(string publicKey)
        {
            try
            {
                // ------- create RSACryptoServiceProvider instance and initialize with private key -----  
                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                rsaProvider.PersistKeyInCsp = false;
                var rsaParameters = ConvertFromPublicKey(publicKey);
                rsaProvider.ImportParameters(rsaParameters);
                return rsaProvider;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #region 解析.net 生成的Pem
        /// <summary>
        /// 将Pkcs1pPem格式公钥(1024 or 2048)转换为RSAParameters
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        private static RSAParameters ConvertFromPublicKey(string publicKey)
        {

            if (string.IsNullOrEmpty(publicKey))
            {
                throw new ArgumentNullException("publicKey", "This arg cann't be empty.");
            }
            byte[] keyData = Convert.FromBase64String(publicKey);
            bool keySize1024 = (keyData.Length == 162);
            bool keySize2048 = (keyData.Length == 294);
            if (!(keySize1024 || keySize2048))
            {
                throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");
            }
            byte[] pemModulus = (keySize1024 ? new byte[128] : new byte[256]);
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, (keySize1024 ? 29 : 33), pemModulus, 0, (keySize1024 ? 128 : 256));
            Array.Copy(keyData, (keySize1024 ? 159 : 291), pemPublicExponent, 0, 3);
            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            return para;
        }

        /// <summary>
        /// 将Pkcs1pPem格式私钥(1024 or 2048)转换为RSAParameters
        /// </summary>
        /// <param name="privateKey">pem私钥内容</param>
        /// <returns>转换得到的RSAParamenters</returns>
        private static RSAParameters ConvertFromPrivateKey(string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
            {
                throw new ArgumentNullException("privateKey", "This arg cann't be empty.");
            }
            byte[] keyData = Convert.FromBase64String(privateKey);

            bool keySize1024 = (keyData.Length == 609 || keyData.Length == 610);
            bool keySize2048 = (keyData.Length == 1190 || keyData.Length == 1191 || keyData.Length == 1192);

            if (!(keySize1024 || keySize2048))
            {
                throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");
            }

            int index = (keySize1024 ? 11 : 12);
            byte[] pemModulus = (keySize1024 ? new byte[128] : new byte[256]);
            Array.Copy(keyData, index, pemModulus, 0, pemModulus.Length);

            index += pemModulus.Length;
            index += 2;
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, index, pemPublicExponent, 0, 3);

            index += 3;
            index += 4;
            if ((int)keyData[index] == 0)
            {
                index++;
            }
            byte[] pemPrivateExponent = (keySize1024 ? new byte[128] : new byte[256]);
            Array.Copy(keyData, index, pemPrivateExponent, 0, pemPrivateExponent.Length);

            index += pemPrivateExponent.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            byte[] pemPrime1 = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemPrime1, 0, pemPrime1.Length);

            index += pemPrime1.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            byte[] pemPrime2 = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemPrime2, 0, pemPrime2.Length);

            index += pemPrime2.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            byte[] pemExponent1 = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemExponent1, 0, pemExponent1.Length);

            index += pemExponent1.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            byte[] pemExponent2 = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemExponent2, 0, pemExponent2.Length);

            index += pemExponent2.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            byte[] pemCoefficient = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemCoefficient, 0, pemCoefficient.Length);

            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            para.D = pemPrivateExponent;
            para.P = pemPrime1;
            para.Q = pemPrime2;
            para.DP = pemExponent1;
            para.DQ = pemExponent2;
            para.InverseQ = pemCoefficient;
            return para;
        }
        #endregion
        /// <summary>
        /// Format Pkcs1 format private key
        /// Author:Zhiqiang Li
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Pkcs1PrivateKeyFormat(string privateKey)
        {
            if (privateKey.StartsWith("-----BEGIN RSA PRIVATE KEY-----"))
            {
                return privateKey;
            }

            List<string> res = new List<string>();
            res.Add("-----BEGIN RSA PRIVATE KEY-----");

            int pos = 0;

            while (pos < privateKey.Length)
            {
                var count = privateKey.Length - pos < 64 ? privateKey.Length - pos : 64;
                res.Add(privateKey.Substring(pos, count));
                pos += count;
            }

            res.Add("-----END RSA PRIVATE KEY-----");
            var resStr = string.Join("\r\n", res);
            return resStr;
        }

        /// <summary>
        /// Remove the Pkcs1 format private key format
        /// </summary>
        /// <param name="formatPrivateKey"></param>
        /// <returns></returns>
        public static string Pkcs1PrivateKeyFormatRemove(string formatPrivateKey)
        {
            if (!formatPrivateKey.StartsWith("-----BEGIN RSA PRIVATE KEY-----"))
            {
                return formatPrivateKey;
            }
            return formatPrivateKey.Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "")
                .Replace("\r\n", "");
        }
    }

    /// <summary>
    /// 类名：RSAFromPkcs8
    /// 功能：RSA解密、签名、验签
    /// 详细：该类对Java生成的密钥进行解密和签名以及验签专用类，不需要修改
    /// 版本：2.0
    /// 修改日期：2011-05-10
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// </summary>
    public sealed class RSAFromPkcs8
    {
        /** 默认编码字符集 */
        private static string DEFAULT_CHARSET = "UTF-8";
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="content">需要签名的内容</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <param name="signType">签名方式(RSA:密钥长度为1024,RSA2:密钥长度为2048)</param>
        /// <returns></returns>
        public static string SignByPrivateKey(string content, string privateKey, string inputCharset, string signType = "RSA")
        {
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            Encoding encoding = Encoding.GetEncoding(inputCharset);
            byte[] contentBytes = encoding.GetBytes(content);
            byte[] signedBytes = null;
            using (RSACryptoServiceProvider rsaProvider = DecodePemPrivateKey(privateKey))
            {
                if (signType == "RSA2")
                {
                    signedBytes = rsaProvider.SignData(contentBytes, "SHA256");
                }
                else
                {
                    signedBytes = rsaProvider.SignData(contentBytes, "SHA1");
                }
            }
            return Convert.ToBase64String(signedBytes);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="content">需要验证的内容</param>
        /// <param name="signedString">签名结果</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <param name="signType">签名方式(RSA:密钥长度为1024,RSA2:密钥长度为2048)</param>
        /// <returns></returns>
        public static bool VerifyByPublicKey(string content, string signedString, string publicKey, string inputCharset, string signType = "RSA")
        {
            bool result = false;
            if (!string.IsNullOrEmpty(signedString))
            {
                signedString = signedString.Replace("%2B", "+");
                signedString = signedString.Replace(" ", "+");
            }
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            Encoding encoding = Encoding.GetEncoding(inputCharset);
            byte[] contentBytes = encoding.GetBytes(content);
            byte[] signedBytes = Convert.FromBase64String(signedString);
            using (RSACryptoServiceProvider rsaProvider = DecodePemPublicKey(publicKey))
            {
                rsaProvider.PersistKeyInCsp = false;
                if (signType == "RSA2")
                {
                    result = rsaProvider.VerifyData(contentBytes, "SHA256", signedBytes);
                }
                else
                {
                    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                    result = rsaProvider.VerifyData(contentBytes, sha1, signedBytes);
                }
            }
            return result;
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="content">需要验证的内容</param>
        /// <param name="signedString">签名结果</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <param name="signType">签名方式(RSA:密钥长度为1024,RSA2:密钥长度为2048)</param>
        /// <returns></returns>
        public static bool VerifyByPublicKeyFormat(string content, string signedString, string publicKey, string inputCharset, string signType = "RSA")
        {
            bool result = false;
            if (!string.IsNullOrEmpty(signedString))
            {
                signedString = signedString.Replace("%2B", "+");
                signedString = signedString.Replace(" ", "+");
            }
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            Encoding encoding = Encoding.GetEncoding(inputCharset);
            byte[] contentBytes = encoding.GetBytes(content);
            byte[] signedBytes = Convert.FromBase64String(signedString);
            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
            {
                rsaProvider.PersistKeyInCsp = false;
                string formatPublicKey = PublicKeyFormat(publicKey);
                rsaProvider.LoadPublicKeyPem(formatPublicKey);
                if (signType == "RSA2")
                {
                    result = rsaProvider.VerifyData(contentBytes, "SHA256", signedBytes);
                }
                else
                {
                    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                    result = rsaProvider.VerifyData(contentBytes, sha1, signedBytes);
                }
            }
            return result;
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="content">需要验证的内容</param>
        /// <param name="signedString">签名结果</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <param name="signType">签名方式(RSA:密钥长度为1024,RSA2:密钥长度为2048)</param>
        /// <returns></returns>
        public static bool VerifyByPrivateKey(string content, string signedString, string privateKey, string inputCharset, string signType = "RSA")
        {
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            bool result = false;
            Encoding encoding = Encoding.GetEncoding(inputCharset);
            byte[] contentBytes = encoding.GetBytes(content);
            byte[] signedBytes = Convert.FromBase64String(signedString);
            using (RSACryptoServiceProvider rsaProvider = DecodePemPrivateKey(privateKey))
            {
                if (signType == "RSA2")
                {
                    result = rsaProvider.VerifyData(contentBytes, "SHA256", signedBytes);
                }
                else
                {
                    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                    result = rsaProvider.VerifyData(contentBytes, sha1, signedBytes);
                }
            }
            return result;
        }
        /// <summary>
        /// RSA公钥加密
        /// </summary>
        /// <param name="content">需要签名的内容</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns></returns>
        public static string Encrypt(string content, string publicKey, string inputCharset)
        {
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            Encoding encoding = Encoding.GetEncoding(inputCharset);
            using (RSACryptoServiceProvider rsaProvider = DecodePemPublicKey(publicKey))
            {
                rsaProvider.PersistKeyInCsp = false;
                byte[] data = Encoding.GetEncoding(inputCharset).GetBytes(content);
                int maxBlockSize = rsaProvider.KeySize / 8 - 11; //加密块最大长度限制
                if (data.Length <= maxBlockSize)
                {
                    byte[] cipherbytes = rsaProvider.Encrypt(data, false);
                    return Convert.ToBase64String(cipherbytes);
                }
                MemoryStream plaiStream = new MemoryStream(data);
                MemoryStream crypStream = new MemoryStream();
                Byte[] buffer = new Byte[maxBlockSize];
                int blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
                while (blockSize > 0)
                {
                    Byte[] toEncrypt = new Byte[blockSize];
                    Array.Copy(buffer, 0, toEncrypt, 0, blockSize);
                    Byte[] cryptograph = rsaProvider.Encrypt(toEncrypt, false);
                    crypStream.Write(cryptograph, 0, cryptograph.Length);
                    blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
                }
                return Convert.ToBase64String(crypStream.ToArray(), Base64FormattingOptions.None);
            }

        }

        /// <summary>
        /// 用RSA私钥解密
        /// </summary>
        /// <param name="resData">待解密字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>解密结果</returns>
        public static string Decrypt(string resData, string privateKey, string inputCharset)
        {
            if (string.IsNullOrEmpty(inputCharset))
            {
                inputCharset = DEFAULT_CHARSET;
            }
            RSACryptoServiceProvider rsaCsp = DecodePemPrivateKey(privateKey);
            byte[] data = Convert.FromBase64String(resData);
            int maxBlockSize = rsaCsp.KeySize / 8; //解密块最大长度限制
            if (data.Length <= maxBlockSize)
            {
                byte[] cipherbytes = rsaCsp.Decrypt(data, false);
                return Encoding.GetEncoding(inputCharset).GetString(cipherbytes);
            }
            MemoryStream crypStream = new MemoryStream(data);
            MemoryStream plaiStream = new MemoryStream();
            Byte[] buffer = new Byte[maxBlockSize];
            int blockSize = crypStream.Read(buffer, 0, maxBlockSize);
            while (blockSize > 0)
            {
                Byte[] toDecrypt = new Byte[blockSize];
                Array.Copy(buffer, 0, toDecrypt, 0, blockSize);
                Byte[] cryptograph = rsaCsp.Decrypt(toDecrypt, false);
                plaiStream.Write(cryptograph, 0, cryptograph.Length);
                blockSize = crypStream.Read(buffer, 0, maxBlockSize);
            }
            return Encoding.GetEncoding(inputCharset).GetString(plaiStream.ToArray());
        }

        /// <summary>
        /// 解析java生成的pkcs8pem文件公钥
        /// </summary>
        /// <param name="strPem"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider DecodePemPublicKey(string strPem)
        {
            byte[] pkcs8PublickKey;
            pkcs8PublickKey = Convert.FromBase64String(strPem);
            if (pkcs8PublickKey != null)
            {
                RSACryptoServiceProvider rsa = DecodeRSAPublicKey(pkcs8PublickKey);
                return rsa;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 解析java生成的pem文件私钥
        /// </summary>
        /// <param name="strPem"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider DecodePemPrivateKey(string strPem)
        {
            byte[] pkcs8PrivateKey;
            pkcs8PrivateKey = Convert.FromBase64String(strPem);
            if (pkcs8PrivateKey != null)
            {
                RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8PrivateKey);
                return rsa;
            }
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkcs8"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {
            try
            {
                pkcs8 = GetPemPrivateKey(pkcs8);
                RSACryptoServiceProvider rsa = DecodeRSAPrivateKey(pkcs8);
                return rsa;
            }
            catch (Exception ex)
            {
                //    throw new AopException("EncryptContent = woshihaoren,zheshiyigeceshi,wanerde", ex);
            }
            return null;

        }
        /// <summary>
        /// Gets the pem private key.
        /// </summary>
        /// <param name="pkcs8">The PKCS8.</param>
        /// <returns>System.Byte[].</returns>
        private static byte[] GetPemPrivateKey(byte[] pkcs8)
        {

            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            MemoryStream mem = new MemoryStream(pkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;


                bt = binr.ReadByte();
                if (bt != 0x02)
                    return null;

                twobytes = binr.ReadUInt16();

                if (twobytes != 0x0001)
                    return null;

                seq = binr.ReadBytes(15);		//read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))	//make sure Sequence for OID is correct
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x04)	//expect an Octet string 
                    return null;

                bt = binr.ReadByte();		//read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
                if (bt == 0x81)
                    binr.ReadByte();
                else
                    if (bt == 0x82)
                    binr.ReadUInt16();
                //------ at this stage, the remaining sequence should be the RSA private key

                byte[] privateKeyBytes = binr.ReadBytes((int)(lenstream - mem.Position));
                return privateKeyBytes;
            }

            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        /// <summary>Decodes the RSA public key.</summary>
        /// <param name="publickey">The publickey.</param>
        /// <returns>RSACryptoServiceProvider.</returns>
        private static RSACryptoServiceProvider DecodeRSAPublicKey(byte[] publickey)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"  
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------  
            MemoryStream mem = new MemoryStream(publickey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading  
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID  
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct  
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next  
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)  
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus  
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes  
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order  
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it  
                    binr.ReadByte();    //skip this null byte  
                    modsize -= 1;   //reduce modulus buffer size by 1  
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes  

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data  
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)  
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----  
                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                RSAParameters rsaParameters = new RSAParameters();
                rsaParameters.Modulus = modulus;
                rsaParameters.Exponent = exponent;
                rsaProvider.ImportParameters(rsaParameters);
                return rsaProvider;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }
        /// <summary>
        /// Decodes the RSA private key.
        /// </summary>
        /// <param name="privkey">The privkey.</param>
        /// <returns>RSACryptoServiceProvider.</returns>
        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // --------- Set up stream to decode the asn.1 encoded RSA private key ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);  //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();    //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------ all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                RSAParameters rsaParams = new RSAParameters();
                rsaParams.Modulus = MODULUS;
                rsaParams.Exponent = E;
                rsaParams.D = D;
                rsaParams.P = P;
                rsaParams.Q = Q;
                rsaParams.DP = DP;
                rsaParams.DQ = DQ;
                rsaParams.InverseQ = IQ;
                rsaProvider.ImportParameters(rsaParams);
                return rsaProvider;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                binr.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binr"></param>
        /// <returns></returns>
        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();	// data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }



            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
        /// <summary>
        /// 将pem格式公钥(1024 or 2048)转换为RSAParameters
        /// </summary>
        /// <param name="publicKey">pem公钥内容</param>
        /// <returns>转换得到的RSAParamenters</returns>
        public static RSAParameters ConvertFromPemPublicKey(string publicKey)
        {
            byte[] keyData = Convert.FromBase64String(publicKey);
            bool keySize1024 = (keyData.Length == 162);
            bool keySize2048 = (keyData.Length == 294);
            if (!(keySize1024 || keySize2048))
            {
                throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");
            }
            byte[] pemModulus = (keySize1024 ? new byte[128] : new byte[256]);
            var pemPublicExponent = new byte[3];
            Array.Copy(keyData, (keySize1024 ? 29 : 33), pemModulus, 0, (keySize1024 ? 128 : 256));
            Array.Copy(keyData, (keySize1024 ? 159 : 291), pemPublicExponent, 0, 3);
            var para = new RSAParameters { Modulus = pemModulus, Exponent = pemPublicExponent };
            byte[] sss = para.Modulus;
            return para;
        }

        /// <summary>
        /// Format Pkcs8 format private key
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Pkcs8PrivateKeyFormat(string privateKey)
        {
            if (privateKey.StartsWith("-----BEGIN PRIVATE KEY-----"))
            {
                return privateKey;
            }
            List<string> res = new List<string>();
            res.Add("-----BEGIN PRIVATE KEY-----");

            int pos = 0;

            while (pos < privateKey.Length)
            {
                var count = privateKey.Length - pos < 64 ? privateKey.Length - pos : 64;
                res.Add(privateKey.Substring(pos, count));
                pos += count;
            }

            res.Add("-----END PRIVATE KEY-----");
            var resStr = string.Join("\r\n", res);
            return resStr;
        }
        /// <summary>
        /// Remove the Pkcs8 format private key format
        /// </summary>
        /// <param name="formatPrivateKey"></param>
        /// <returns></returns>
        public static string Pkcs8PrivateKeyFormatRemove(string formatPrivateKey)
        {
            if (!formatPrivateKey.StartsWith("-----BEGIN PRIVATE KEY-----"))
            {
                return formatPrivateKey;
            }
            return formatPrivateKey.Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "")
                .Replace("\r\n", "");
        }

        /// <summary>
        /// Format public key
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string PublicKeyFormat(string publicKey)
        {
            if (publicKey.StartsWith("-----BEGIN PUBLIC KEY-----"))
            {
                return publicKey;
            }
            List<string> res = new List<string>();
            res.Add("-----BEGIN PUBLIC KEY-----");
            int pos = 0;

            while (pos < publicKey.Length)
            {
                var count = publicKey.Length - pos < 64 ? publicKey.Length - pos : 64;
                res.Add(publicKey.Substring(pos, count));
                pos += count;
            }
            res.Add("-----END PUBLIC KEY-----");
            var resStr = string.Join("\r\n", res);
            return resStr;
        }
        /// <summary>
        /// Public key format removed
        /// </summary>
        /// <param name="formatPublicKey"></param>
        /// <returns></returns>
        public static string PublicKeyFormatRemove(string formatPublicKey)
        {
            if (!formatPublicKey.StartsWith("-----BEGIN PUBLIC KEY-----"))
            {
                return formatPublicKey;
            }
            return formatPublicKey.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "")
                .Replace("\r\n", "");
        }
    }

    /// <summary>Extension method for initializing a RSACryptoServiceProvider from PEM data string.</summary>  
    internal static class RsaCryptoServiceProviderExtension
    {
        #region Methods

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a DER public key blob.</summary>  
        public static void LoadPublicKeyDer(this RSACryptoServiceProvider provider, byte[] derData)
        {
            byte[] rsaData = GetRsaFromDer(derData);
            byte[] publicKeyBlob = GetPublicKeyBlobFromRsa(rsaData);
            provider.ImportCspBlob(publicKeyBlob);
        }

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a DER private key blob.</summary>  
        public static void LoadPrivateKeyDer(this RSACryptoServiceProvider provider, byte[] derData)
        {
            byte[] privateKeyBlob = GetPrivateKeyDer(derData);
            provider.ImportCspBlob(privateKeyBlob);
        }

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a PEM public key string.</summary>  
        public static void LoadPublicKeyPem(this RSACryptoServiceProvider provider, string sPem)
        {
            byte[] derData = GetDerFromPem(sPem);
            LoadPublicKeyDer(provider, derData);
        }

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a PEM private key string.</summary>  
        public static void LoadPrivateKeyPem(this RSACryptoServiceProvider provider, string sPem)
        {
            byte[] derData = GetDerFromPem(sPem);
            provider.LoadPrivateKeyDer(derData);
        }

        /// <summary>Returns a public key blob from an RSA public key.</summary>  
        internal static byte[] GetPublicKeyBlobFromRsa(byte[] rsaData)
        {
            byte[] data = null;
            UInt32 dwCertPublicKeyBlobSize = 0;
            if (CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING,
                new IntPtr((int)CRYPT_OUTPUT_TYPES.RSA_CSP_PUBLICKEYBLOB), rsaData, (UInt32)rsaData.Length, CRYPT_DECODE_FLAGS.NONE,
                data, ref dwCertPublicKeyBlobSize))
            {
                data = new byte[dwCertPublicKeyBlobSize];
                if (!CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING,
                    new IntPtr((int)CRYPT_OUTPUT_TYPES.RSA_CSP_PUBLICKEYBLOB), rsaData, (UInt32)rsaData.Length, CRYPT_DECODE_FLAGS.NONE,
                    data, ref dwCertPublicKeyBlobSize))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return data;
        }

        /// <summary>Converts DER binary format to a CAPI CRYPT_PRIVATE_KEY_INFO structure.</summary>  
        internal static byte[] GetPrivateKeyDer(byte[] derData)
        {
            byte[] data;
            UInt32 dwRsaPrivateKeyBlobSize = 0;
            if (CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.PKCS_RSA_PRIVATE_KEY),
                derData, (UInt32)derData.Length, CRYPT_DECODE_FLAGS.NONE, null, ref dwRsaPrivateKeyBlobSize))
            {
                data = new byte[dwRsaPrivateKeyBlobSize];
                if (!CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.PKCS_RSA_PRIVATE_KEY),
                    derData, (UInt32)derData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwRsaPrivateKeyBlobSize))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return data;
        }

        /// <summary>Converts DER binary format to a CAPI CERT_PUBLIC_KEY_INFO structure containing an RSA key.</summary>  
        internal static byte[] GetRsaFromDer(byte[] derData)
        {
            byte[] data = null;
            byte[] publicKey = null;
            UInt32 dwCertPublicKeyInfoSize = 0;
            IntPtr pCertPublicKeyInfo = IntPtr.Zero;
            if (CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.X509_PUBLIC_KEY_INFO),
                derData, (UInt32)derData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwCertPublicKeyInfoSize))
            {
                data = new byte[dwCertPublicKeyInfoSize];
                if (CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.X509_PUBLIC_KEY_INFO),
                    derData, (UInt32)derData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwCertPublicKeyInfoSize))
                {
                    GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    try
                    {
                        var info = (CERT_PUBLIC_KEY_INFO)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CERT_PUBLIC_KEY_INFO));
                        publicKey = new byte[info.PublicKey.cbData];
                        Marshal.Copy(info.PublicKey.pbData, publicKey, 0, publicKey.Length);
                    }
                    finally
                    {
                        handle.Free();
                    }
                }
                else
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return publicKey;
        }

        /// <summary>Extracts the binary data from a PEM file.</summary>  
        internal static byte[] GetDerFromPem(string sPEM)
        {
            UInt32 dwSkip, dwFlags;
            UInt32 dwBinarySize = 0;

            if (!CryptStringToBinary(sPEM, (UInt32)sPEM.Length, CRYPT_STRING_FLAGS.CRYPT_STRING_BASE64HEADER, null, ref dwBinarySize, out dwSkip, out dwFlags))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            byte[] decodedData = new byte[dwBinarySize];
            if (!CryptStringToBinary(sPEM, (UInt32)sPEM.Length, CRYPT_STRING_FLAGS.CRYPT_STRING_BASE64HEADER, decodedData, ref dwBinarySize, out dwSkip, out dwFlags))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return decodedData;
        }

        #endregion Methods

        #region P/Invoke Constants

        /// <summary>Enumeration derived from Crypto API.</summary>  
        internal enum CRYPT_ACQUIRE_CONTEXT_FLAGS : uint
        {
            CRYPT_NEWKEYSET = 0x8,
            CRYPT_DELETEKEYSET = 0x10,
            CRYPT_MACHINE_KEYSET = 0x20,
            CRYPT_SILENT = 0x40,
            CRYPT_DEFAULT_CONTAINER_OPTIONAL = 0x80,
            CRYPT_VERIFYCONTEXT = 0xF0000000
        }

        /// <summary>Enumeration derived from Crypto API.</summary>  
        internal enum CRYPT_PROVIDER_TYPE : uint
        {
            PROV_RSA_FULL = 1
        }

        /// <summary>Enumeration derived from Crypto API.</summary>  
        internal enum CRYPT_DECODE_FLAGS : uint
        {
            NONE = 0,
            CRYPT_DECODE_ALLOC_FLAG = 0x8000
        }

        /// <summary>Enumeration derived from Crypto API.</summary>  
        internal enum CRYPT_ENCODING_FLAGS : uint
        {
            PKCS_7_ASN_ENCODING = 0x00010000,
            X509_ASN_ENCODING = 0x00000001,
        }

        /// <summary>Enumeration derived from Crypto API.</summary>  
        internal enum CRYPT_OUTPUT_TYPES : int
        {
            X509_PUBLIC_KEY_INFO = 8,
            RSA_CSP_PUBLICKEYBLOB = 19,
            PKCS_RSA_PRIVATE_KEY = 43,
            PKCS_PRIVATE_KEY_INFO = 44
        }

        /// <summary>Enumeration derived from Crypto API.</summary>  
        internal enum CRYPT_STRING_FLAGS : uint
        {
            CRYPT_STRING_BASE64HEADER = 0,
            CRYPT_STRING_BASE64 = 1,
            CRYPT_STRING_BINARY = 2,
            CRYPT_STRING_BASE64REQUESTHEADER = 3,
            CRYPT_STRING_HEX = 4,
            CRYPT_STRING_HEXASCII = 5,
            CRYPT_STRING_BASE64_ANY = 6,
            CRYPT_STRING_ANY = 7,
            CRYPT_STRING_HEX_ANY = 8,
            CRYPT_STRING_BASE64X509CRLHEADER = 9,
            CRYPT_STRING_HEXADDR = 10,
            CRYPT_STRING_HEXASCIIADDR = 11,
            CRYPT_STRING_HEXRAW = 12,
            CRYPT_STRING_NOCRLF = 0x40000000,
            CRYPT_STRING_NOCR = 0x80000000
        }

        #endregion P/Invoke Constants

        #region P/Invoke Structures

        /// <summary>Structure from Crypto API.</summary>  
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_OBJID_BLOB
        {
            internal UInt32 cbData;
            internal IntPtr pbData;
        }

        /// <summary>Structure from Crypto API.</summary>  
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_ALGORITHM_IDENTIFIER
        {
            internal IntPtr pszObjId;
            internal CRYPT_OBJID_BLOB Parameters;
        }

        /// <summary>Structure from Crypto API.</summary>  
        [StructLayout(LayoutKind.Sequential)]
        struct CRYPT_BIT_BLOB
        {
            internal UInt32 cbData;
            internal IntPtr pbData;
            internal UInt32 cUnusedBits;
        }

        /// <summary>Structure from Crypto API.</summary>  
        [StructLayout(LayoutKind.Sequential)]
        struct CERT_PUBLIC_KEY_INFO
        {
            internal CRYPT_ALGORITHM_IDENTIFIER Algorithm;
            internal CRYPT_BIT_BLOB PublicKey;
        }

        #endregion P/Invoke Structures

        #region P/Invoke Functions

        /// <summary>Function for Crypto API.</summary>  
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDestroyKey(IntPtr hKey);

        /// <summary>Function for Crypto API.</summary>  
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptImportKey(IntPtr hProv, byte[] pbKeyData, UInt32 dwDataLen, IntPtr hPubKey, UInt32 dwFlags, ref IntPtr hKey);

        /// <summary>Function for Crypto API.</summary>  
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptReleaseContext(IntPtr hProv, Int32 dwFlags);

        /// <summary>Function for Crypto API.</summary>  
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptAcquireContext(ref IntPtr hProv, string pszContainer, string pszProvider, CRYPT_PROVIDER_TYPE dwProvType, CRYPT_ACQUIRE_CONTEXT_FLAGS dwFlags);

        /// <summary>Function from Crypto API.</summary>  
        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptStringToBinary(string sPem, UInt32 sPemLength, CRYPT_STRING_FLAGS dwFlags, [Out] byte[] pbBinary, ref UInt32 pcbBinary, out UInt32 pdwSkip, out UInt32 pdwFlags);

        /// <summary>Function from Crypto API.</summary>  
        [DllImport("crypt32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDecodeObjectEx(CRYPT_ENCODING_FLAGS dwCertEncodingType, IntPtr lpszStructType, byte[] pbEncoded, UInt32 cbEncoded, CRYPT_DECODE_FLAGS dwFlags, IntPtr pDecodePara, ref byte[] pvStructInfo, ref UInt32 pcbStructInfo);

        /// <summary>Function from Crypto API.</summary>  
        [DllImport("crypt32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDecodeObject(CRYPT_ENCODING_FLAGS dwCertEncodingType, IntPtr lpszStructType, byte[] pbEncoded, UInt32 cbEncoded, CRYPT_DECODE_FLAGS flags, [In, Out] byte[] pvStructInfo, ref UInt32 cbStructInfo);

        #endregion P/Invoke Functions
    }
}

