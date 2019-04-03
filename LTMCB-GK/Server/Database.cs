using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    class Record
    {
        public string username;
        public string password;
        public double money;

        public Record(string usr, string pwd, double m)
        {
            username = usr;
            password = pwd;
            money = m;
        }
        public string ToString()
        {
            return username + " " 
                + password + " " 
                + money.ToString() + "\n";
        }

        public static Record Parse(String str)
        {
            string[] tmp = str.Split(' ');
            if (tmp.Length != 3)
                return null;
            double ti;
            bool b = Double.TryParse(tmp[2], out ti);
            if (!b)
                return null;
            Record tRec = new Record(tmp[0], tmp[1], ti);
            return tRec;
        }
    };

  
    class Database
    {
        string dbPath = "";
        public Database(string path)
        {
            dbPath = path;
        }
        public int addRecord(Record record)
        {
            if (find(record.username) != null)
                return -1;
            string s = record.ToString();
            FileStream f = File.Open(
                dbPath, 
                FileMode.Append, 
                FileAccess.Write
                );
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] tmp = asen.GetBytes(s);
            f.Write(tmp, 0, tmp.Length);
            f.Close();
            return 1;
        }

        public Record find(string username)
        {
            string[] text = null;
            try
            {
                text = File.ReadAllLines(dbPath);
            }
            catch
            {
                return null;
            }
           
            for(int i = 0; i < text.Length; i++)
            {
                Record tRec = Record.Parse(text[i]);
                if (tRec.username == username)
                    return tRec;
            }            
            return null;
        }

        public int update(string usr, int newmoney)
        {
            string[] text = null;
            try
            {
                text = File.ReadAllLines(dbPath);
            }
            catch
            {
                return -1;
            }
            for (int i = 0; i < text.Length; i++)
            {
                Record tRec = Record.Parse(text[i]);
                if (tRec.username == usr)
                {
                    tRec.money = newmoney;
                    text[i] = tRec.ToString();
                    File.WriteAllLines(dbPath,
                        text);
                    return 1;
                }
            }
            return -1;
        }
    }

    
}
