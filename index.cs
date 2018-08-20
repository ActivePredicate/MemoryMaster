using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace MemoryMaster
{
    
    public class Database
    {
        public List<string> columns;
        public List<Dictionary<string, string>> records;
    }
    public class MemoryMaster
    {
        string MenuPath = "";
        public MemoryMaster(string dbMenupath)
        {
            MenuPath = dbMenupath;
            if(Directory.Exists(dbMenupath))
            {
                
            }
            else
            {
                Directory.CreateDirectory(dbMenupath);
                List<string> vs = new List<string>() { "null" };
                byte[] dat = SerializeObject(vs);
                File.WriteAllBytes(Rel2Abs("\\mcfg.dat"), dat);
                List<Dictionary<string, string>> records = new List<Dictionary<string, string>>();
                Dictionary<string, string> record = new Dictionary<string, string>();
                record.Add("null", "none data");
                records.Add(record);
                byte[] hdat = SerializeObject(records);
                File.WriteAllBytes(Rel2Abs("\\tags.dat"), hdat);
            }
        }
        public void CreateDB(List<string> vs)
        {
            Directory.CreateDirectory(MenuPath);
            byte[] dat = SerializeObject(vs);
            File.WriteAllBytes(Rel2Abs("\\mcfg.dat"), dat);
            List<Dictionary<string, string>> records = new List<Dictionary<string, string>>();
            byte[] hdat = SerializeObject(records);
            File.WriteAllBytes(Rel2Abs("\\tags.dat"), hdat);
        }
        public void CreateDB(string dbMenupath)
        {
            Directory.CreateDirectory(dbMenupath);
            List<string> vs = new List<string>() { "ID" };
            byte[] dat = SerializeObject(vs);
            File.WriteAllBytes(Rel2Abs("\\mcfg.dat"), dat);
            List<Dictionary<string, string>> records = new List<Dictionary<string, string>>();
            Dictionary<string, string> record = new Dictionary<string, string>();
            record.Add("ID", "0");
            records.Add(record);
            byte[] hdat = SerializeObject(records);
            File.WriteAllBytes(Rel2Abs("\\tags.dat"), hdat);
        }

        public void CreateOrReplaceColumns(List<string> vs)
        {
            byte[] dat = SerializeObject(vs);
            File.WriteAllBytes(Rel2Abs("\\mcfg.dat"), dat);
        }

        public void AddRecord(List<string> vs)
        {
            
            byte[] dat = File.ReadAllBytes(Rel2Abs("\\mcfg.dat"));
            List<string> cnm = (List<string>)DeserializeObject(dat);
            Dictionary<string, string> record = new Dictionary<string, string>();
            for(int i=0;i<vs.Count;i++)
            {
                record.Add(cnm[i], vs[i]);
            }
            byte[] qdat= File.ReadAllBytes(Rel2Abs("\\tags.dat"));
            List<Dictionary<string, string>> records =(List < Dictionary<string, string> >) DeserializeObject(qdat);
            records.Add(record);
            byte[] hdat = SerializeObject(records);
            File.WriteAllBytes(Rel2Abs("\\tags.dat"), hdat);
            DeleteRecord("null", "none data");
        }

        public Database GetAllRecords()
        {
            byte[] dat = File.ReadAllBytes(Rel2Abs("\\mcfg.dat"));
            List<string> cnm = (List<string>)DeserializeObject(dat);
            byte[] qdat = File.ReadAllBytes(Rel2Abs("\\tags.dat"));
            List<Dictionary<string, string>> arecords = (List<Dictionary<string, string>>)DeserializeObject(qdat);
            return new Database() { columns = cnm, records = arecords };
        }
        public Dictionary<string,string> GetRec(string key, string value)
        {
            byte[] dat = File.ReadAllBytes(Rel2Abs("\\mcfg.dat"));
            List<string> cnm = (List<string>)DeserializeObject(dat);
            byte[] qdat = File.ReadAllBytes(Rel2Abs("\\tags.dat"));
            List<Dictionary<string, string>> records = (List<Dictionary<string, string>>)DeserializeObject(qdat);
            foreach (var r1 in records)
            {
                foreach (var d in r1)
                {
                    if (d.Key == key)
                    {
                        if (d.Value == value)
                        {
                            return r1;
                        }
                    }
                }
            }
            return null;
        }
        public string ReturnValue(string key,string value,string askKey)
        {
            byte[] dat = File.ReadAllBytes(Rel2Abs("\\mcfg.dat"));
            List<string> cnm = (List<string>)DeserializeObject(dat);
            byte[] qdat = File.ReadAllBytes(Rel2Abs("\\tags.dat"));
            List<Dictionary<string, string>> records = (List<Dictionary<string, string>>)DeserializeObject(qdat);
            foreach(var r1 in records)
            {
                foreach(var d in r1)
                {
                    if(d.Key==key)
                    {
                        if (d.Value == value)
                        {
                            foreach (var dd in r1)
                            {
                                if(dd.Key==askKey)
                                {
                                    return dd.Value;
                                }
                            }
                        }
                    }
                }
            }
            return "No such a record in Database";
        }
        public bool Exist(string key, string value)
        {
            byte[] qdat = File.ReadAllBytes(Rel2Abs("\\tags.dat"));
            List<Dictionary<string, string>> records = (List<Dictionary<string, string>>)DeserializeObject(qdat);
            for (int i = 0; i < records.Count; i++)
            {
                Dictionary<string, string> doc = records[i];
                foreach (var item in doc)
                {
                    if (item.Key == key)
                    {
                        if (item.Value == value)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void UpdateRecord(string key, string value, Dictionary<string,string> BeUpdated)
        {
            byte[] qdat = File.ReadAllBytes(Rel2Abs("\\tags.dat"));
            List<Dictionary<string, string>> records = (List<Dictionary<string, string>>)DeserializeObject(qdat);
            foreach (var e in BeUpdated)
            {
                for (int i = 0; i < records.Count; i++)
                {
                    Dictionary<string, string> doc = records[i];
                    foreach (var item in doc)
                    {
                        if (item.Key == key)
                        {
                            if (item.Value == value)
                            {
                                doc[e.Key] = e.Value;
                                break;
                            }
                        }
                    }
                }
            }
            byte[] hdat = SerializeObject(records);
            File.WriteAllBytes(Rel2Abs("\\tags.dat"), hdat);
            
        }

        public void DeleteRecord(string key, string value)
        {
            byte[] qdat = File.ReadAllBytes(Rel2Abs("\\tags.dat"));
            List<Dictionary<string, string>> records = (List<Dictionary<string, string>>)DeserializeObject(qdat);
            Dictionary<string, string> needToRemove = new Dictionary<string, string>();
            for (int i = 0; i < records.Count; i++)
            {
                Dictionary<string, string> doc = records[i];
                bool ableToDel = false;
                foreach (var item in doc)
                {
                    if (item.Key == key)
                    {
                        if (item.Value == value)
                        {
                            ableToDel = true;
                            needToRemove = doc;
                        }
                    }
                }
                if(ableToDel)
                {
                    records.Remove(needToRemove);
                }
                
            }
            byte[] hdat = SerializeObject(records);
            File.WriteAllBytes(Rel2Abs("\\tags.dat"), hdat);
        }

        string Rel2Abs(string rel)
        {
            return MenuPath +rel;
        }
        public static byte[] SerializeObject(object obj)
        {
            if (obj == null)
                return null;
            //内存实例
            MemoryStream ms = new MemoryStream();
            //创建序列化的实例
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);//序列化对象，写入ms流中  
            byte[] bytes = ms.GetBuffer();
            return bytes;
        }
        public static object DeserializeObject(byte[] bytes)
        {
            object obj = null;
            if (bytes == null)
                return obj;
            //利用传来的byte[]创建一个内存流
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            obj = formatter.Deserialize(ms);//把内存流反序列成对象  
            ms.Close();
            return obj;
        }
        public static void RunWindow()
        {
            Application.EnableVisualStyles();
            Application.Run(new DataViewer());
        }
        public static void RunWindow(string dbpath)
        {
            Application.EnableVisualStyles();
            Application.Run(new DataViewer());
        }
    }
}
