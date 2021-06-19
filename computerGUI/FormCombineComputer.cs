using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using find;
using Mymatch;

namespace computerGUI
{
    public partial class CombineComputer : Form
    {
        static List<string> ListValue = new List<string>() {
                "[CPU]",
                "[硬盘]",
                "[机箱]",
                "[主板]",
                "[显卡]",
                "[光驱]",
                "[内存]",
                "[显示器]",
                "[键盘]",
                "[鼠标]" };
        static List<int> ListPrice = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public CombineComputer()
        {
            InitializeComponent();
            //
            //CPU初始化下拉框：
            //
            List<string> newListCPU = new List<string>() { "Intel", "AMD" };
            this.cbbCPU.Items.AddRange(newListCPU.ToArray());
            //CPU型号



            //
            //硬盘下拉框
            //
            List<string> newListHardisk = new List<string>() { "希捷", "东芝", "西部数据", "HGST" };
            this.cbbHardiskBrand.Items.AddRange(newListHardisk.ToArray());

            //
            //初始化电源下拉框
            //
            
            //
            //初始化主板下拉框
            //
            List<string> newListMainboard = new List<string>() { "华硕","技嘉","七彩虹","映泰","昂达","铭瑄","华擎","艾尔莎","影驰","梅捷","微星" };
            this.cbbMainBoardBrand.Items.AddRange(newListMainboard.ToArray());

            //
            //初始化显卡下拉框
            //
            List<string> newListGraphicscardBrand = new List<string>() { "华硕", "丽台", "NVIDIA", "七彩虹", "磐镭", "铭瑄", "索泰", "万丽", "影驰", "技嘉", "微星","翔升","梅捷","XFX","EVGA","撼讯" };
            this.cbbGraphicsCardBrand.Items.AddRange(newListGraphicscardBrand.ToArray());

            //
            //初始化机箱下拉框
            //
            List<string> newListCrateBrand = new List<string>();
            List<BsonDocument> resCrateBrand = find.Myfind.unconditionFind("newdata", "机箱");
            foreach (var r in resCrateBrand)
            {
                string s = r["名称"].ToString().Substring(0, 2);
                if (!(newListCrateBrand.Exists(t => t == s)))
                {
                    newListCrateBrand.Add(s);
                }
            }
            this.cbbCrateBrand.Items.AddRange(newListCrateBrand.ToArray());

            //
            //初始化内存下拉框
            //

            List<string> newListMemoryBrand = new List<string>();
            List<BsonDocument> resMemoryBrand  = find.Myfind.unconditionFind("newdata", "内存");
            foreach (var r in resMemoryBrand)
            {
                string s = r["名称"].ToString().Substring(0, 2);
                if (!(newListMemoryBrand.Exists(t => t == s)))
                {
                    newListMemoryBrand.Add(s);
                }
            }
            this.cbbMemoryBrand.Items.AddRange(newListMemoryBrand.ToArray());

            //
            //初始化显示器下拉框
            //
            List<string> newListDisplayerBrand = new List<string>();
            List<BsonDocument> resDisplayerBrand = find.Myfind.unconditionFind("newdata", "显示器");
            foreach (var r in resDisplayerBrand)
            {
                string s = r["名称"].ToString().Substring(0, 2);
                if (!(newListDisplayerBrand.Exists(t => t == s)))
                {
                    newListDisplayerBrand.Add(s);
                }
            }
            this.cbbDIsplayerBrand.Items.AddRange(newListDisplayerBrand.ToArray());

            //
            //初始化光驱下拉框
            //
            List<string> newListCDROMBrand = new List<string>();
            List<BsonDocument> resCDROMBrand = find.Myfind.unconditionFind("newdata", "光驱");
            foreach (var r in resCDROMBrand)
            {
                string s = r["名称"].ToString().Substring(0, 2);
                if (!(newListCDROMBrand.Exists(t => t == s)))
                {
                    newListCDROMBrand.Add(s);
                }
            }
            this.cbbCDROMBrand.Items.AddRange(newListCDROMBrand.ToArray());

            //
            //初始化键盘下拉框
            //
            List<string> newListKeyboardBrand = new List<string>();
            List<BsonDocument> resKeyboardBrand = find.Myfind.unconditionFind("newdata", "键盘");
            foreach (var r in resKeyboardBrand)
            {
                string s = r["名称"].ToString().Substring(0, r["名称"].ToString().Length - 2);
                if ((s[0] >= 'A' && s[0] <= 'Z') || (s[0] >= 'a' && s[0] <= 'z'))
                {
                    string English = "";
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i] != ' ')
                        {
                            English += s[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (!(newListKeyboardBrand.Exists(t => t == English)))
                    {
                        newListKeyboardBrand.Add(English);
                    }
                }
                else
                {
                    if (!(newListKeyboardBrand.Exists(t => t == s.Substring(0, 2))))
                    {
                        newListKeyboardBrand.Add(s.Substring(0, 2));
                    }
                }

            }
            newListKeyboardBrand.Sort();
            this.cbbKeyboardBrand.Items.AddRange(newListKeyboardBrand.ToArray());

            //
            //初始化鼠标下拉框
            //
            List<string> newListMouseBrand = new List<string>();
            List<BsonDocument> resMouseBrand = find.Myfind.unconditionFind("newdata", "鼠标");
            foreach (var r in resMouseBrand)
            {
                string s = r["名称"].ToString().Substring(0, r["名称"].ToString().Length-2);
                if((s[0] >= 'A' && s[0] <= 'Z') || (s[0] >= 'a' && s[0] <= 'z'))
                {
                    string English = "";
                    for(int i = 0; i < s.Length; i++)
                    {
                        if(s[i] != ' ')
                        {
                            English += s[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (!(newListMouseBrand.Exists(t => t == English)))
                    {
                        newListMouseBrand.Add(English);
                    }
                }
                else
                {
                    if (!(newListMouseBrand.Exists(t => t == s.Substring(0,2))))
                    {
                        newListMouseBrand.Add(s.Substring(0,2));
                    }
                }

            }
            newListMouseBrand.Sort();
            this.cbbMouseBrand.Items.AddRange(newListMouseBrand.ToArray());

            //
            //初始化图片
            //
            pbGraphiccardSort.SizeMode = PictureBoxSizeMode.Zoom;
            pbGraphiccardSort.Visible = false;

        }

        static int sign = 1;

        private void btnGranphiccardSort_Click(object sender, EventArgs e)
        {
            if(sign == 1)
            {
                pbGraphiccardSort.Visible = true;
                labGraphiccardSort.Text = "显卡天梯图";
                sign *= -1;
            }
            else
            {
                pbGraphiccardSort.Visible = false;
                labGraphiccardSort.Text = "";
                sign *= -1;
            }
            string a = "0.0分";
            string b = "0.1分";
            Console.WriteLine(string.Compare(a, b));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cbbCPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CPU = this.cbbCPU.SelectedItem.ToString();
            string collections = "CPU";
            string database = "newdata";
            List<string> typeName = new List<string>() { "名称" };
            List<string> condition = new List<string>() { ".*" + CPU + ".*" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach(var r in result)
            {
                newList.Add(r["名称"].ToString());
            }
            // 清楚之前cbbCPUtype复选框中的所有数据
            this.cbbCPUtype.Items.Clear();
            this.cbbCPUtype.Items.AddRange(newList.ToArray());
            this.labMatchInformation.Text = "";
        }

        private void cbbCPUtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CPU = this.cbbCPUtype.SelectedItem.ToString();
            string collections = "CPU";
            string database = "newdata";
            List<string> condition_cpu = new List<string>() { CPU };
            List<string> typeName_cpu = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.conditionFind(typeName_cpu, condition_cpu, database, collections);
            this.labCPUprice.Text = "CPU价格：";
            this.labCPUdomaintfrequency.Text = "CPU主频：";
            foreach(var r in result)
            {
                this.labCPUdomaintfrequency.Text += r["CPU主频"].ToString();
                if(r["价格"].ToString() != "格面议")
                {
                    this.labCPUprice.Text = this.labCPUprice.Text + "￥" + r["价格"].ToString();
                }
                else
                {
                    this.labCPUprice.Text = this.labCPUprice.Text + "价" + r["价格"].ToString();
                }
                if(r["价格"].ToString() != "格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[0] = number;
                }
                else
                {
                    ListPrice[0] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();

            if(this.cbbMainboardType.Text.Trim().Length != 0)
            {
                collections = "mb";
                string MB_name = this.cbbMainboardType.SelectedItem.ToString();
                if (Mymatch.Match.match(CPU, MB_name))
                {
                    this.labMatchInformation.ForeColor = Color.Green;
                    this.labMatchInformation.Text = "CPU与主板可以匹配";
                }
                else
                {
                    this.labMatchInformation.ForeColor = Color.Red;
                    this.labMatchInformation.Text = "CPU与主板不能匹配";
                }

            }
            else
            {
                this.labMatchInformation.Text = "";
            }
        }

        private void cbbHardiskBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HardiskName = this.cbbHardiskBrand.SelectedItem.ToString();
            string database = "newdata";
            string collection = "硬盘";
            List<string> typeName = new List<string>() { "名称" };
            List<string> condition = new List<string>() { HardiskName };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collection);

            List<string> newList = new List<string>();
            foreach(var r in result)
            {
                if(!newList.Exists(t => t == r["硬盘类型"].ToString()))
                {
                    newList.Add(r["硬盘类型"].ToString());
                }
            }
            this.cbbHardisk.Items.Clear();
            this.cbbHardisk.Items.AddRange(newList.ToArray());
            this.cbbHardiskCapacity.Items.Clear();
            this.cbbHardiskType.Items.Clear();
        }

        private void cbbHardisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HardiskName = this.cbbHardiskBrand.SelectedItem.ToString();
            string Hardisk = this.cbbHardisk.SelectedItem.ToString();
            string database = "newdata";
            string collection = "硬盘";
            List<string> typeName = new List<string>() { "名称", "硬盘类型" };
            List<string> condition = new List<string>() { HardiskName, Hardisk };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collection);

            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                if (!newList.Exists(t => t == r["硬盘容量"].ToString()))
                {
                    newList.Add(r["硬盘容量"].ToString());
                }
            }
            this.cbbHardiskCapacity.Items.Clear();
            this.cbbHardiskCapacity.Items.AddRange(newList.ToArray());
            this.cbbHardiskType.Items.Clear();
        }

        private void cbbHardiskCapacity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HardiskName = this.cbbHardiskBrand.SelectedItem.ToString();
            string HardiskCapacity = this.cbbHardiskCapacity.SelectedItem.ToString();
            string Hardisk = this.cbbHardisk.SelectedItem.ToString();
            string collections = "硬盘";
            string database = "newdata";
            List<string> condition = new List<string>() { HardiskName, HardiskCapacity, Hardisk };
            List<string> typeName = new List<string>() { "名称", "硬盘容量", "硬盘类型" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                string s = r["名称"].ToString();
                
                if (!newList.Exists(t => t == s))
                {
                    newList.Add(s);
                }
            }
            this.cbbHardiskType.Items.Clear();
            this.cbbHardiskType.Items.AddRange(newList.ToArray());
        }

        private void cbbHardiskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HardiskName = this.cbbHardiskType.SelectedItem.ToString();
            string collections = "hd";
            string database = "newdata";
            List<string> condition = new List<string>() { HardiskName };
            List<string> typeName = new List<string>() { "HD_name" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labHardiskPrice.Text = "硬盘价格：";
            foreach (var r in result)
            {
                this.labHardiskPrice.Text += r["HD_price"].ToString();
                if (r["HD_price"].ToString() != "价格面议")
                {
                    string s = r["HD_price"].ToString().Substring(1);
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[1] = number;
                }
                else
                {
                    ListPrice[1] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();
        }
            
        private void cbbMainBoardBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MainboardBrand = this.cbbMainBoardBrand.SelectedItem.ToString();
            string collections = "主板";
            string database = "newdata";
            List<string> condition = new List<string>() { MainboardBrand };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                string s = r["名称"].ToString();

                if (!newList.Exists(t => t == s))
                {
                    newList.Add(s);
                }
            }
            this.cbbMainboardType.Items.Clear();
            this.cbbMainboardType.Items.AddRange(newList.ToArray());
            this.labMatchInformation.Text = "";
        }

        private void cbbMainboardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MainboardType = this.cbbMainboardType.SelectedItem.ToString();
            string collections = "主板";
            string database = "newdata";
            List<string> condition = new List<string>() { MainboardType };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labMainboardCompability.Text = "主板参数：";
            this.labMainboardPrice.Text = "主板价格：";
            foreach (var r in result)
            {
                this.labMainboardCompability.Text += r["存储接口"].ToString();
                this.labMainboardPrice.Text += "￥" + r["价格"].ToString();
                if (r["价格"].ToString() != "价格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[2] = number;
                }
                else
                {
                    ListPrice[2] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();

            if (this.cbbCPUtype.Text.Trim().Length != 0)
            {
                collections = "主板";
                string CPU = this.cbbCPUtype.SelectedItem.ToString();
                if (Mymatch.Match.match(CPU, MainboardType))
                {
                    this.labMatchInformation.ForeColor = Color.Green;
                    this.labMatchInformation.Text = "CPU与主板可以匹配";
                }
                else
                {
                    this.labMatchInformation.ForeColor = Color.Red;
                    this.labMatchInformation.Text = "CPU与主板不能匹配";
                }
            }
            else
            {
                this.labMatchInformation.Text = "";
            }
        }
        
        private void cbbGraphicsCardBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GraphicscradBrand = this.cbbGraphicsCardBrand.SelectedItem.ToString();
            Console.WriteLine(GraphicscradBrand);
            string collections = "显卡";
            string database = "newdata";
            List<string> condition = new List<string>() { GraphicscradBrand };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                string s = r["名称"].ToString();

                if (!newList.Exists(t => t == s))
                {
                    newList.Add(s);
                }
            }
            this.cbbGraphicsCardType.Items.Clear();
            this.cbbGraphicsCardType.Items.AddRange(newList.ToArray());
            this.labRadar.Text = "";
        }

        private void cbbGraphicsCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GraphicscardType = this.cbbGraphicsCardType.SelectedItem.ToString();
            string collections = "显卡";
            string database = "newdata";
            List<string> condition = new List<string>() { GraphicscardType };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labGraphicscardMemory.Text = "显卡显存：";
            this.labGraphicscardPrice.Text = "显卡价格：";
            foreach (var r in result)
            {
                if(r["显存容量"].ToString() == "")
                {
                    this.labGraphicscardMemory.Text += "显存未知";
                }
                else
                {
                    this.labGraphicscardMemory.Text += r["显存容量"].ToString();
                }
                this.labGraphicscardPrice.Text += r["价格"].ToString();
                if (r["价格"].ToString() != "格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[3] = number;
                }
                else
                {
                    ListPrice[3] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();
            this.labRadar.Text = "显卡雷达图";
            string path = "E:\\学习\\软件工程\\软件工程课程设计2021\\computerGUI\\显卡图\\picture\\" + GraphicscardType + ".png";
            pbRadar.ImageLocation = path;
            pbRadar.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void cbbCrateBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GraphicscradBrand = this.cbbCrateBrand.SelectedItem.ToString();
            string collections = "机箱";
            string database = "newdata";
            List<string> condition = new List<string>() { GraphicscradBrand };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                string s = r["名称"].ToString();

                if (!newList.Exists(t => t == s))
                {
                    newList.Add(s);
                }
            }
            this.cbbCrateType.Items.Clear();
            this.cbbCrateType.Items.AddRange(newList.ToArray());
        }

        private void cbbCrateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CrateType = this.cbbCrateType.SelectedItem.ToString();
            string collections = "机箱";
            string database = "newdata";
            List<string> condition = new List<string>() { CrateType };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labCrateStructure.Text = "机箱结构：";
            this.labCratePrice.Text = "机箱价格：";
            foreach (var r in result)
            {
                if (r["适用主板"].ToString() == "")
                {
                    this.labCrateStructure.Text += "结构未知";
                }
                else
                {
                    this.labCrateStructure.Text += r["适用主板"].ToString();
                }
                this.labCratePrice.Text += r["价格"].ToString();
                if (r["价格"].ToString() != "价格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[4] = number;
                }
                else
                {
                    ListPrice[4] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();
        }

        private void cbbMemoryBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MemoryBrand = this.cbbMemoryBrand.SelectedItem.ToString();
            string collections = "内存";
            string database = "newdata";
            List<string> condition = new List<string>() { MemoryBrand };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                string s = r["名称"].ToString();

                if (!newList.Exists(t => t == s))
                {
                    newList.Add(s);
                }
            }
            this.cbbMemoryType.Items.Clear();
            this.cbbMemoryType.Items.AddRange(newList.ToArray());
        }

        private void cbbMemoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MemoryType = this.cbbMemoryType.SelectedItem.ToString();
            string collections = "内存";
            string database = "newdata";
            List<string> condition = new List<string>() { MemoryType };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labMemoryCapability.Text = "内存大小：";
            this.labMemoryPrice.Text = "内存价格：";
            foreach (var r in result)
            {
                if (r["内存容量"].ToString() == "")
                {
                    this.labMemoryCapability.Text += "内存大小未知";
                }
                else
                {
                    this.labMemoryCapability.Text += r["内存容量"].ToString();
                }
                this.labMemoryPrice.Text += r["价格"].ToString();
                if (r["价格"].ToString() != "价格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[5] = number;
                }
                else
                {
                    ListPrice[5] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();
        }

        private void cbbDIsplayerBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DisplayerBrand = this.cbbDIsplayerBrand.SelectedItem.ToString();
            string collections = "显示器";
            string database = "newdata";
            List<string> condition = new List<string>() { DisplayerBrand };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                if (!newList.Exists(t => t == r["最佳分辨率"].ToString()))
                {
                    newList.Add(r["最佳分辨率"].ToString());
                }
            }
            this.cbbDisplayerResolution.Items.Clear();
            this.cbbDisplayerResolution.Items.AddRange(newList.ToArray());
            this.cbbDIsplayerType.Items.Clear();
        }

        private void cbbDisplayerResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DisplayerBrand = this.cbbDIsplayerBrand.SelectedItem.ToString();
            string DisplayerResolution = this.cbbDisplayerResolution.SelectedItem.ToString();
            string collections = "显示器";
            string database = "newdata";
            List<string> condition = new List<string>() { DisplayerBrand, DisplayerResolution };
            List<string> typeName = new List<string>() { "名称", "最佳分辨率" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                string s = r["名称"].ToString();
                int len = s.Length;
                if (!newList.Exists(t => t == s.Substring(0, len - 2)))
                {
                    newList.Add(s.Substring(0, len - 2));
                }
            }
            this.cbbDIsplayerType.Items.Clear();
            this.cbbDIsplayerType.Items.AddRange(newList.ToArray());
        }

        private void cbbDIsplayerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DisplayerType = this.cbbDIsplayerType.SelectedItem.ToString();
            string collections = "显示器";
            string database = "newdata";
            List<string> condition = new List<string>() { DisplayerType };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labDisplayerSize.Text = "显示器尺寸：";
            this.labDisplyerPrice.Text = "显示器价格：";
            foreach (var r in result)
            {
                if (r["屏幕尺寸"].ToString() == "")
                {
                    this.labDisplayerSize.Text += "显示器尺寸未知";
                }
                else
                {
                    this.labDisplayerSize.Text += r["屏幕尺寸"].ToString();
                }
                this.labDisplyerPrice.Text += r["价格"].ToString();
                if (r["价格"].ToString() != "价格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[6] = number;
                }
                else
                {
                    ListPrice[6] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();
        }

        private void cbbCDROMBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CDROMBrand = this.cbbCDROMBrand.SelectedItem.ToString();
            string collections = "光驱";
            string database = "newdata";
            List<string> condition = new List<string>() { CDROMBrand };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                if (!newList.Exists(t => t == r["名称"].ToString()))
                {
                    newList.Add(r["名称"].ToString());
                }
            }
            this.cbbCDROMType.Items.Clear();
            this.cbbCDROMType.Items.AddRange(newList.ToArray());
        }

        private void cbbCDROMType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CDROMType = this.cbbCDROMType.SelectedItem.ToString();
            string collections = "光驱";
            string database = "newdata";
            List<string> condition = new List<string>() { CDROMType };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labCDROM_type.Text = "光驱类型：";
            this.labCDROMPrice.Text = "光驱价格：";
            foreach (var r in result)
            {
                if (r["光驱类型"].ToString() == "")
                {
                    this.labCDROM_type.Text += "光驱类型未知";
                }
                else
                {
                    this.labCDROM_type.Text += r["光驱类型"].ToString();
                }
                this.labCDROMPrice.Text += r["价格"].ToString();
                if (r["价格"].ToString() != "价格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[7] = number;
                }
                else
                {
                    ListPrice[7] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();
        }

        private void cbbKeyboardBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string KeyboardBrand = this.cbbKeyboardBrand.SelectedItem.ToString();
            string collections = "键盘";
            string database = "newdata";
            List<string> condition = new List<string>() { KeyboardBrand+".*" };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                if (!newList.Exists(t => t == r["名称"].ToString()))
                {
                    newList.Add(r["名称"].ToString());
                }
            }
            this.cbbKeyboardType.Items.Clear();
            this.cbbKeyboardType.Items.AddRange(newList.ToArray());
        }

        private void cbbKeyboardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string KeyboardType = this.cbbKeyboardType.SelectedItem.ToString();
            string collections = "键盘";
            string database = "newdata";
            List<string> condition = new List<string>() { KeyboardType };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labKeyboardAxis.Text = "键盘类型：";
            this.labKeyboardPrice.Text = "键盘价格：";
            foreach (var r in result)
            {
                if (r["产品定位"].ToString() == "")
                {
                    this.labKeyboardAxis.Text += "键盘键轴未知";
                }
                else
                {
                    this.labKeyboardAxis.Text += r["产品定位"].ToString();
                }
                this.labKeyboardPrice.Text += r["价格"].ToString();
                if (r["价格"].ToString() != "价格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[8] = number;
                }
                else
                {
                    ListPrice[8] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();
        }

        private void cbbMouseBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MouseBrand = this.cbbMouseBrand.SelectedItem.ToString();
            string collections = "鼠标";
            string database = "newdata";
            List<string> condition = new List<string>() { MouseBrand };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            List<string> newList = new List<string>();
            foreach (var r in result)
            {
                if (!newList.Exists(t => t == r["名称"].ToString()))
                {
                    newList.Add(r["名称"].ToString());
                }
            }
            this.cbbMouseType.Items.Clear();
            this.cbbMouseType.Items.AddRange(newList.ToArray());
        }

        private void cbbMouseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MouseType = this.cbbMouseType.SelectedItem.ToString();
            string collections = "鼠标";
            string database = "newdata";
            List<string> condition = new List<string>() { MouseType };
            List<string> typeName = new List<string>() { "名称" };
            List<BsonDocument> result = find.Myfind.ambiguousFind(typeName, condition, database, collections);
            this.labMouseDpi.Text = "鼠标dpi：";
            this.labMousePrice.Text = "鼠标价格：";
            foreach (var r in result)
            {
                if (r["最高分辨率"].ToString() == "")
                {
                    this.labMouseDpi.Text += "鼠标dpi未知";
                }
                else
                {
                    this.labMouseDpi.Text += r["最高分辨率"].ToString();
                }
                this.labMousePrice.Text += r["价格"].ToString();
                if (r["价格"].ToString() != "价格面议")
                {
                    string s = r["价格"].ToString();
                    int number = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        number = number * 10 + (s[i] - '0');
                    }
                    ListPrice[9] = number;
                }
                else
                {
                    ListPrice[9] = 0;
                }
            }
            this.labTotalPrice.Text = "装机总价：￥" + ListPrice.Sum().ToString();
        }

        private void btnCombineComputer_Click(object sender, EventArgs e)
        {
            string maxprice = this.txbMaxPrice.Text.ToString();
            string minprice = this.txbMinPrice.Text.ToString();

            this.labInformation.Text = "正在匹配最佳计算机配置...";
            List<string> name = new List<string>() { "CPU", "光驱", "机箱", "键盘", "内存", "鼠标", "显卡", "显示器", "硬盘", "主板" };
            var control = this.gbRecommend.Controls;
            for (int i = 1; i <= 10; i++)
            {
                Label com = this.Controls.Find("lab" + i, true)[0] as Label;
                com.Text = name[i - 1] + ":";
            }
            List<string> combine = genatic.mygene.getcombine(0, 0);

            for (int i = 0; i < 10; i++)
            {
                Label com = this.Controls.Find("lab" + (i + 1), true)[0] as Label;
                com.Text += combine[i];
            }
            this.labInformation.Text = "";
        }

    }
}
