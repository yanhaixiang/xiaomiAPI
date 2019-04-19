using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace API
{
    public class Mi
    {
        public static string Jie(string iv,string sessionKey,string encryptedData) {
            byte[] encDatas = Convert.FromBase64String(encryptedData);
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Key = Convert.FromBase64String(sessionKey); // Encoding.UTF8.GetBytes(AesKey);
            rijndaelCipher.IV = Convert.FromBase64String(iv);// Encoding.UTF8.GetBytes(AesIV);
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encDatas, 0, encDatas.Length);
            string result1 = Encoding.Default.GetString(plainText);
            dynamic model = Newtonsoft.Json.Linq.JToken.Parse(result1) as dynamic;
            return model.phoneNumber;

        }
    }
}