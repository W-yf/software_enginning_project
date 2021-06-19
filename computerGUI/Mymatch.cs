using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using find;

namespace Mymatch
{
    class Match
    {
        static MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");

        //字符串数字转换为汉字
        static public string OneBitNumberToChinese(string num)
        {
            int n_num = num.Length;
            string numStr_1 = "0123456789";
            string numStr_2 = "123456789";
            string chineseStr_1 = "零一二三四五六七八九";
            string chineseStr_2 = "一二三四五六七八九";
            string result = "";
            if (n_num < 2)
            {
                int numIndex_1 = numStr_1.IndexOf(num[0]);
                if (numIndex_1 > -1)
                {
                    result = chineseStr_1.Substring(numIndex_1, 1);
                }
            }
            if (n_num == 2)
            {
                int numIndex_2 = numStr_2.IndexOf(num[1]);
                if (numIndex_2 > -1)
                {
                    result = "十" + chineseStr_2.Substring(numIndex_2, 1);
                }
                if (num[1] == '0')
                {
                    result = "十";
                }
            }
            return result;
        }
        //core
        static public bool J_udge_core(string c_1, string c_2, string c_3, string J)
        {
            if (J.Contains(c_1) && J.Contains(c_2) && J.Contains(c_3))
            {
/*                Console.WriteLine(J);*/
                return true;
            }
            else if (J.Contains(c_1) && J.Contains(c_2))
            {
                if ((!J.Contains("i3")) && (!J.Contains("i5")) && (!J.Contains("i7")) && (!J.Contains("i9")))
                {
                    Console.WriteLine(J);

                    return true;
                }
            }
            else
                return false;

            return false;

        }
        //amd
        static public bool J_udge_amd(string c_1, string c_2, string J)
        {
            if (J.Contains(c_1) && J.Contains(c_2))
            {
/*                Console.WriteLine(J);*/
                return true;
            }

            return false;

        }
        static public bool match(string c_s, string m_b)
        {

            /*            IMongoDatabase db = client.GetDatabase("data");
                        //建立数据库
                        var collection_1 = db.GetCollection<BsonDocument>("cpu");
                        var collection_2 = db.GetCollection<BsonDocument>("mb");
                        //创建查询器
                        FilterDefinitionBuilder<BsonDocument> builderFilter = Builders<BsonDocument>.Filter;
                        //
                        FilterDefinition<BsonDocument> filter_1 = builderFilter.Eq("CPU_name", c_s);
                        FilterDefinition<BsonDocument> filter_2 = builderFilter.Eq("MB_name", m_b);
                        List<BsonDocument> result_1 = collection_1.Find<BsonDocument>(filter_1).ToList();
                        List<BsonDocument> result_2 = collection_2.Find<BsonDocument>(filter_2).ToList();*/

            string db = "newdata";
            string collection_cpu = "CPU";
            string collection_mainboard = "主板";

            List<string> cpuName = new List<string>() { c_s };
            List<string> Typecpu = new List<string>() { "名称" };
            List<string> mainboardName = new List<string>() { m_b };
            List<string> TypeMainboard = new List<string>() { "名称" };

            List<BsonDocument> result_cpu = find.Myfind.ambiguousFind(Typecpu, cpuName, db, collection_cpu);
            List<BsonDocument> result_mb = find.Myfind.ambiguousFind(TypeMainboard, mainboardName, db, collection_mainboard);

            List<string> cpu_s = new List<string>();
            List<string> mb_co = new List<string>();

            foreach (var i in result_cpu)
            {
                cpu_s.Add(i["CPU系列"].ToString());
            }
            foreach (var i in result_mb)
            {
                mb_co.Add(i["CPU类型"].ToString());
            }


            string I = "", J = "";
            foreach (var temp_i in cpu_s)
            {
                I = temp_i;
            }

            foreach (var temp_j in mb_co)
            {
                J = temp_j;
            }

            string c1_temp = "", c2_temp = "", c3_temp = "";
            int ind_ex, ind_ex2;
            Console.WriteLine(I);
            c1_temp = I.Substring(0, 2);
            bool j_ud;
            switch (c1_temp)
            {
                case "酷睿":
                    ind_ex = I.IndexOf("代");
                    ind_ex2 = I.IndexOf("i");
                    c1_temp = I.Substring(5, ind_ex - 5);
                    c1_temp = OneBitNumberToChinese(c1_temp) + "代";
                    c2_temp = "Core";
                    c3_temp = I.Substring(ind_ex2, 2);
/*                    Console.WriteLine(c1_temp);
                    Console.WriteLine(c2_temp);
                    Console.WriteLine(c3_temp);*/
                    j_ud = J_udge_core(c1_temp, c2_temp, c3_temp, J);
                    Console.WriteLine(j_ud);
                    return j_ud;
                case "AM":
                    ind_ex = I.LastIndexOf(" ");
                    c1_temp = I.Substring(10, ind_ex - 10);
                    c1_temp = OneBitNumberToChinese(c1_temp) + "代";
                    c2_temp = "AMD Ryzen";
                    j_ud = J_udge_amd(c1_temp, c2_temp, J);
/*                    Console.WriteLine(c1_temp);
                    Console.WriteLine(j_ud);*/
                    return j_ud;
                case "赛扬":
                    c1_temp = "Celeron";
                    j_ud = J.Contains(c1_temp);
/*                    Console.WriteLine(j_ud);*/
                    return j_ud;
                case "奔腾":
                    c1_temp = "Pentium";
                    j_ud = J.Contains(c1_temp);
/*                    Console.WriteLine(j_ud);*/
                    return j_ud;
            }
            return false;
        }
    }
}
