using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MemoryPot.DAL
{
    class DatabaseFunctions
    {
        public static void UnhashPassword(string hashedPassword, string hashedPasswordFromDatabase)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPasswordFromDatabase);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(hashedPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
        }
        public static Boolean LoginDB(string dbName, string dbUserName, string dbPassword)
        {
            try
            {
                string startupPath = Environment.CurrentDirectory;
                string dataBasePath = startupPath + "\\..\\..\\DB\\" + dbName;
                SQLiteConnection dbConnString;
                dbConnString = new SQLiteConnection("Data Source =" + dataBasePath + ";Version=3;");
                dbConnString.Open();
                using (SQLiteCommand checkConnection = new SQLiteCommand("SELECT UserPasswordHash FROM Users where UserName = \"" + dbUserName + "\";", dbConnString))
                {
                    using (var reader = checkConnection.ExecuteReader())
                    {
                        var listOfUsers = new List<string>();
                        while (reader.Read())
                            listOfUsers.Add(reader.GetString(0));
                        foreach(var item in listOfUsers)
                        {
                            UnhashPassword(dbPassword, item);
                        }
                    }
                }
                dbConnString.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Nie określony błąd, skontaktuj się z administratorem.", "DB Info");
                return false;
            }
        }
    }
}
