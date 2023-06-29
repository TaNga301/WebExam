using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace WebExam.Commons
{
    public class Common
    {
        // Phương thức mã hóa chuỗi sử dụng thuật toán Rijndael
        // Parameters:
        //   str: Chuỗi cần mã hóa
        //   password: Mật khẩu được sử dụng để mã hóa
        // Returns:
        //   Chuỗi đã được mã hóa
        public static string Encrypt(string str, string password)
        {
            string encStr = null;
            RijndaelManaged rijndael = null;

            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }

            try
            {
                rijndael = new RijndaelManaged();
                rijndael.BlockSize = 128;
                rijndael.KeySize = 128;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                byte[] key, iv;
                GenerateKeyFromPassword(password, rijndael.KeySize, out key, rijndael.BlockSize, out iv);
                rijndael.Key = key;
                rijndael.IV = iv;

                byte[] strBytes = Encoding.UTF8.GetBytes(str);

                ICryptoTransform encryptor = rijndael.CreateEncryptor();

                byte[] encBytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);

                encryptor.Dispose();

                encStr = Convert.ToBase64String(encBytes);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
            finally
            {
                if (rijndael != null)
                {
                    
                    rijndael.Clear();
                }
            }

            return encStr;
        }

        // Phương thức giải mã chuỗi sử dụng thuật toán Rijndael
        // Parameters:
        //   str: Chuỗi cần giải mã
        //   password: Mật khẩu được sử dụng để giải mã
        // Returns:
        //   Chuỗi đã được giải mã
        public static string Decrypt(string str, string password)
        {
            string decStr = null;
            RijndaelManaged rijndael = null;

            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }

            try
            {
                rijndael = new RijndaelManaged();
                rijndael.BlockSize = 128;
                rijndael.KeySize = 128;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                byte[] key, iv;
                GenerateKeyFromPassword(password, rijndael.KeySize, out key, rijndael.BlockSize, out iv);
                rijndael.Key = key;
                rijndael.IV = iv;

                byte[] strBytes = Convert.FromBase64String(str);

                ICryptoTransform decryptor = rijndael.CreateDecryptor();
                byte[] decBytes = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);

                //Đóng kết nối
                decryptor.Dispose();

                decStr = Encoding.UTF8.GetString(decBytes);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return String.Empty;
            }
            finally
            {
                if (rijndael != null)
                {
                    rijndael.Clear();
                }
            }

            return decStr;
        }

        // Phương thức tạo khóa và vectơ khởi tạo từ mật khẩu
        // Parameters:
        //   password: Mật khẩu để tạo khóa và vectơ khởi tạo
        //   keySize: Kích thước khóa (bit)
        //   key: Đầu ra - mảng byte chứa khóa
        //   blockSize: Kích thước khối (bit)
        //   iv: Đầu ra - mảng byte chứa vectơ khởi tạo
        private static void GenerateKeyFromPassword(string password, int keySize, out byte[] key, int blockSize, out byte[] iv)
        {
            byte[] salt = Encoding.UTF8.GetBytes(Constants.ENCRYPT_SALT_BASE);
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, 1000);

            key = deriveBytes.GetBytes(keySize / 8);
            iv = deriveBytes.GetBytes(blockSize / 8);
        }
    }
}