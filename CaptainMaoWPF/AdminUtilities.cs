using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using CaptainMaoWPF.Model;

namespace CaptainMaoWPF
{
    internal class AdminUtilities
    {
        private string _userName;
        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _userPassword;

        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _userLastName;
        public string UserLastName
        {
            get { return _userLastName; }
            set { _userLastName = value; }
        }

        private string _userFirstName;
        public string UserFirstName
        {
            get { return _userFirstName; }
            set { _userFirstName = value; }
        }

        private string _userMobile;
        public string UserMobile
        {
            get { return _userMobile; }
            set { _userMobile = value; }
        }

        private string _userEMail;
        public string UserEmail
        {
            get { return _userEMail; }
            set { _userEMail = value; }
        }

        Model.MaoEntities db = new Model.MaoEntities();

        ////建立登錄使用者的方法。
        //public static RegisterErrorTypes CreateUser(string userName, string password, string password2, string email, string firstname, string lastname, string mobile, Image profilePic)
        //{
        //    //確認資訊填寫完整程度。
        //    if (userName == "") { return RegisterErrorTypes.LackOfUsername; }
        //    if (email == "") { return RegisterErrorTypes.LackOfEmail; }
        //    if (password == "") { return RegisterErrorTypes.LackOfPassword; }
        //    if (password != password2) { return RegisterErrorTypes.InconsistentPassword; }

        //    

        //    //確認DataEntity中是否已存在相同帳號。
        //    foreach (Model.AspNetUser u in db.)
        //    {
        //        if (u.UserName == userName)
        //        { return RegisterErrorTypes.DuplicatedUsername; }
        //    }

        //    //用RNGCryptoServiceProvider產生隨機salt，加到cmd的參數中
        //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //    byte[] salt = new byte[32];
        //    rng.GetBytes(salt);

        //    //將password加上salt後進行加密
        //    byte[] bytesPassword = Encoding.Unicode.GetBytes(password + Encoding.Unicode.GetString(salt));
        //    SHA256Managed Algorism = new SHA256Managed();
        //    byte[] hashedPassword = Algorism.ComputeHash(bytesPassword);

        //    //將Image轉為byte[]
        //    MemoryStream ms = new MemoryStream();
        //    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        //    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)profilePic.Source));
        //    encoder.Save(ms);
        //    byte[] imgData = ms.ToArray();
        //    ms.Close();

        //    //經多重檢查無誤後，將帳號資訊登錄於DataEntity中。
        //    Model.NormalUser m = new Model.NormalUser
        //    {
        //        Username= userName,
        //        Password=hashedPassword,
        //        email=email,
        //        salt =salt,
        //        RegistrationDate =DateTime.Now,
        //        Firstname = firstname,
        //        Lastname = lastname,
        //        mobile = mobile,
        //        Photo = imgData
        //    };

        //    db.NormalUsers.Add(m);
        //    db.SaveChanges();

        //    return RegisterErrorTypes.SuccessfulRegistration;
        //}

        public enum RegisterErrorTypes
        {
            SuccessfulRegistration = 0,
            LackOfUsername = 1,
            LackOfPassword = 2,
            DuplicatedUsername = 3,
            InconsistentPassword = 4,
            DuplicateEmail = 5,
            LackOfEmail = 6
        }

        //建立驗證使用者帳號密碼的方法。
        public static LogonErrorTypes ValidateUser(string userName, string password)
        {
            if (userName == "") { return LogonErrorTypes.LackOfUsername; }
            if (password == "") { return LogonErrorTypes.LackOfPassword; }

            Model.MaoEntities db = new Model.MaoEntities();
            
            //確認DataEntity中是否有帳號密碼相符的資訊。
            foreach (AspNetUser u in db.AspNetUsers)
            {
                if (u.UserName == userName)
                {
                    var aspUser = db.AspNetUsers.Where(x=>x.UserName==userName).First();

                    var aspUserRoles = aspUser.AspNetRoles.ToList();
                    string roles = "";
                    foreach (var r in aspUserRoles)
                    {
                        roles += r.Name;
                    }

                    if (Crypto.VerifyHashedPassword(u.PasswordHash, password))
                    {
                        if (roles.Contains("Admin"))
                        {
                            return LogonErrorTypes.ValidUser;
                        }
                        else
                        {
                            return LogonErrorTypes.Unauthorized;
                        }
                    }
                    else
                    {
                        return LogonErrorTypes.InvalidUser;
                    }
                }
            }
            return LogonErrorTypes.UserNotExist;
        }

        public enum LogonErrorTypes
        {
            ValidUser = 0,
            LackOfUsername = 1,
            LackOfPassword = 2,
            InvalidUser = 3,
            UserNotExist = 4,
            Unauthorized = 5
        }

        //public static RegisterErrorTypes UpdateUser(string email, string firstname, string lastname, string mobile, Image profilePic)
        //{
        //    //確認資訊填寫完整程度。
        //    if (email == "") { return RegisterErrorTypes.LackOfEmail; }

        //    Model.WPF_ProjectDBEntities1 db = new Model.WPF_ProjectDBEntities1();

        //    //確認DataEntity中是否已存在相同email。
        //    foreach (Model.NormalUser u in db.NormalUsers)
        //    {
        //        if ((MainWindow.CurrentUser != u.Username) && (u.email == email))
        //        { return RegisterErrorTypes.DuplicateEmail; }
        //    }

        //    //將Image轉為byte[]
        //    MemoryStream ms = new MemoryStream();
        //    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        //    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)profilePic.Source));
        //    encoder.Save(ms);
        //    byte[] imgData = ms.ToArray();
        //    ms.Close();

        //    //經多重檢查無誤後，將帳號資訊登錄於DataEntity中。
        //    var q = from u in db.NormalUsers
        //            where u.Username == MainWindow.CurrentUser
        //            select u;

        //    foreach (var user in q)
        //    {
        //        user.email = email;
        //        user.Firstname = firstname;
        //        user.Lastname = lastname;
        //        user.mobile = mobile;
        //        user.Photo = imgData;
        //    }
        //    db.SaveChanges();

        //    return RegisterErrorTypes.SuccessfulRegistration;
        //}

        

        ////測試: 產生假的Normal User資料
        //public static Model.NormalUser GenerateData()
        //{
        //    Random r = new Random();
        //    string[] lastNameList = {"趙","錢","孫","李","陳","林","黃","吳","張","簡","廖","梁","蕭","花","江",
        //                                     "袁","蔡","周","王","許","譚","鄧","洪","何","郭","馬","崔","馮","余","闕",
        //                                     "汪","方","莊","范","蘇","詹","章","葉","劉","毛","邱","胡","溫","謝","卓"};
        //    string[] firstNameList = {"小明","小英","家偉","國華","罔市","罔腰","招弟","阿牛","小芳","曉萱",
        //                                      "金寶","阿狗","大鼻","建智","美美","小紅","家豪","怡君","小智","俊雄",
        //                                      "阿良","兵兵","冰冰","曉琪","丁丁","美枝","來福","惠鈺","小乖","小威"};

        //    string fLastname = lastNameList[r.Next(lastNameList.Length)];
        //    string fFastname = firstNameList[r.Next(firstNameList.Length)];
        //    DateTime fRegistrationDate = DateTime.Now.AddDays(-r.Next(50000));
        //    string fMobile = $"09{r.Next(100000000):d8}";

        //    string fUserName = "";
        //    int stringLen = r.Next(3, 8);
        //    string alphabetic = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        //    for (int i = 0; i < stringLen; i++)
        //    {
        //        int charNO = r.Next(52);
        //        fUserName += alphabetic[charNO];
        //    }

        //    string fEmail = "XXXX@";
        //    for (int i = 0; i < 12; i++)
        //    {
        //        int charNO = r.Next(52);
        //        fEmail += alphabetic[charNO];
        //    }

        //    Model.NormalUser fakeUser = new Model.NormalUser
        //    {
        //        mobile = fMobile,
        //        RegistrationDate =fRegistrationDate,
        //        Firstname = fFastname,
        //        Lastname = fLastname,
        //        Username = fUserName,
        //        email = fEmail
        //    };
        //    return fakeUser;
        //}


    }

    internal static class Crypto
    {
        private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2SubkeyLength = 256/8; // 256 bits
        private const int SaltSize = 128/8; // 128 bits

        /* =======================
         * HASHED PASSWORD FORMATS
         * =======================
         * 
         * Version 0:
         * PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations.
         * (See also: SDL crypto guidelines v5.1, Part III)
         * Format: { 0x00, salt, subkey }
         */

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            // Produce a version 0 (see comment above) text hash.
            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        // hashedPassword must be of the format of HashWithPassword (salt + Hash(salt+input)
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Verify a version 0 (see comment above) text hash.

            if (hashedPasswordBytes.Length != (1 + SaltSize + PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                // Wrong length or version header.
                return false;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            var storedSubkey = new byte[PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
