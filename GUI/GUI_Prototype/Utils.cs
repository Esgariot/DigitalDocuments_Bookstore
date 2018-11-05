using System;
using System.Collections.Generic;
using System.IO;

namespace GUI_Prototype
{
    public class Utils
    {
        public static string loggedUser { get; set; }
        public static string loggedRole { get; set; }
        public static string rootPath = @"./";
        public static string usersDirName = @"users";
        public static string usersDirPath = System.IO.Path.Combine(rootPath, usersDirName);
        public static string excelDirName = @"excel";
        public static string excelFileName = @"doc.xlsx";
        public static string excelDirPath = System.IO.Path.Combine(rootPath, excelDirName);

        public static bool CAN_ACCEPT_ORDER = false;
        public static bool CAN_REJECT_ORDER = false;
        public static bool CAN_PREPARE_TEMPLATE = false;
        public static bool CAN_CONFIRM_TEMPLATE = false;
        public static bool CAN_ADD_PRODUCTS = false;
        public static bool CAN_CONFIRM_PRODUCTS = false;
        public static bool CAN_CONFIRM_ORDER = false;
        public static bool CAN_FINISH_ORDER = false;

        internal static List<string> getWorkersEmails()
        {
            List<string> res = new List<string>();
            if (Directory.Exists(usersDirPath))
            {
                string[] dirs = Directory.GetDirectories(usersDirName);
                foreach (string directory in dirs)
                {
                    string[] files = Directory.GetFiles(directory);
                    foreach (string file in files)
                    {
                        using (StreamReader reader = new StreamReader(file))
                        {
                            string email = reader.ReadLine();
                            string pass = reader.ReadLine();
                            string role = reader.ReadLine();

                            if (role.Equals(Role.DEPARTMENT_EMPLOYEE))
                                res.Add(email);
                        }
                    }
                }
            }
            return res;
        }

        public static bool CreateDirectory(string path, string directoryName)
        {
            string dirPath = System.IO.Path.Combine(path, directoryName);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                return true;
            }
            return false;
        }

        public static bool DirectoryExists(string path, string directoryName)
        {
            string dirPath = System.IO.Path.Combine(path, directoryName);

            if (Directory.Exists(dirPath))
                return true;

            return false;
        }

        public static bool ValidateEmail(string email)
        {
            if (email == null) return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidatePassword(string password)
        {
            const int MIN_LENGTH = 8;
            const int MAX_LENGTH = 15;

            if (password == null) return false;

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit;
            return isValid;
        }
    }
}