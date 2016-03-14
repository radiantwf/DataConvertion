using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DataConvertion.DataType {

    /// <summary>
    /// 输入文件模板列定义
    /// </summary>
    public class OutputFileTemplateColumn {
        /// <summary>
        /// 列号
        /// </summary>
        public int Column { get; set; }
        /// <summary>
        /// 列标题
        /// </summary>
        public string ColumnHeader { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 数据格式
        /// </summary>
        public string DataFormat { get; set; }
    }

    /// <summary>
    /// 输入文件模板
    /// </summary>
    public class OutputFileTemplate {
        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessTypeType BusinessType { private set; get; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileTypeType FileType { private set; get; }
        /// <summary>
        /// 分隔符（列）
        /// </summary>
        public string SeparatedValues { private set; get; }
        /// <summary>
        /// 标题所在行
        /// </summary>
        public int TitleRowNum { private set; get; }
        /// <summary>
        /// 数据开始行
        /// </summary>
        public int DataStratRowNum { private set; get; }
        /// <summary>
        /// 单条记录占用行数
        /// </summary>
        public int PerRecordRows { private set; get; }
        /// <summary>
        /// 记录间空行
        /// </summary>
        public int RecordsIntervalRows { private set; get; }
        /// <summary>
        /// 列信息
        /// </summary>
        public List<OutputFileTemplateColumn> Columns { private set; get; }
        /// <summary>
        /// Json字符串
        /// </summary>
        public string JsonString { private set; get; }

        /// <summary>
        /// 实例化模板对象
        /// </summary>
        /// <param name="templateFile"></param>
        private OutputFileTemplate(string templateFile) {
            try {
                this.JsonString = File.ReadAllText(templateFile);
                AnalyzeJsonString();
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// 分析模板JSon字符串
        /// </summary>
        private void AnalyzeJsonString() {
            JObject jo = JObject.Parse(JsonString);

            // 业务类型
            if (jo.Property("BusinessType") != null) {
                string businessTypeStr = jo.Property("BusinessType").Value.ToString();
                switch (businessTypeStr.ToUpper()) {
                    case "TRANSACTIONS":
                        BusinessType = BusinessTypeType.Transactions;
                        break;
                    case "OPENNINGACCOUNTS":
                        BusinessType = BusinessTypeType.OpenningAccounts;
                        break;
                    case "ELECTRONICTRANSACTIONS":
                        BusinessType = BusinessTypeType.ElectronicTransactions;
                        break;
                    case "CHINAUNIONPAYTRANSACTIONS":
                        BusinessType = BusinessTypeType.ChinaUnionPayTransactions;
                        break;
                    default:
                        BusinessType = BusinessTypeType.Unknown;
                        break;
                }
            }

            // 文件类型
            if (jo.Property("FileType") != null) {
                string fileTypeStr = jo.Property("FileType").Value.ToString();
                switch (fileTypeStr.ToUpper()) {
                    case "XLS":
                        FileType = FileTypeType.Xls;
                        break;
                    case "TXT":
                        FileType = FileTypeType.Txt;
                        break;
                    default:
                        FileType = FileTypeType.Unknown;
                        break;
                }
            }

            // 分隔符（列）
            if (jo.Property("SeparatedValues") != null) {
                this.SeparatedValues = jo.Property("SeparatedValues").Value.ToString();
            }

            // 标题所在行
            int titleRowNum = 0;
            if (jo.Property("TitleRowNum") != null) {
                int.TryParse(jo.Property("TitleRowNum").Value.ToString(), out titleRowNum);
            }
            this.TitleRowNum = titleRowNum;

            // 数据开始行
            int dataStratRowNum = 1;
            if (jo.Property("DataStratRowNum") != null) {
                int.TryParse(jo.Property("DataStratRowNum").Value.ToString(), out dataStratRowNum);
            }
            this.DataStratRowNum = dataStratRowNum;

            // 单条记录占用行数
            int perRecordRows = 1;
            if (jo.Property("PerRecordRows") != null) {
                int.TryParse(jo.Property("PerRecordRows").Value.ToString(), out perRecordRows);
            }
            this.PerRecordRows = perRecordRows;

            // 记录间空行
            int recordsIntervalRows = 0;
            if (jo.Property("RecordsIntervalRows") != null) {
                int.TryParse(jo.Property("RecordsIntervalRows").Value.ToString(), out recordsIntervalRows);
            }
            this.RecordsIntervalRows = recordsIntervalRows;

            // 列
            Columns = new List<OutputFileTemplateColumn>();
            if (jo.Property("Columns") != null) {
                jo.Property("Columns").Value.ToList().ForEach(row => {
                    OutputFileTemplateColumn column = new OutputFileTemplateColumn();
                    column.Column = row.Value<int>("Column");
                    column.ColumnHeader = row.Value<string>("ColumnHeader");
                    column.DataType = row.Value<string>("DataType");
                    column.DataFormat = row.Value<string>("DataFormat");
                    Columns.Add(column);
                });
            }

        }

        /// <summary>
        /// 模板列表
        /// </summary>
        public static List<OutputFileTemplate> OutputFileTemplateList {
            private set; get;
        }

        static OutputFileTemplate() {
            // 初始化模板列表
            OutputFileTemplateList = new List<OutputFileTemplate>();

            // 获取模板文件
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string outputTemplatesDirectory = Path.Combine(baseDirectory, "OutputTemplets");
            string[] templates = Directory.GetFiles(outputTemplatesDirectory, "*.json", SearchOption.AllDirectories);

            foreach (string templateFile in templates) {
                OutputFileTemplate template = new OutputFileTemplate(templateFile);
                if (template != null) {
                    OutputFileTemplateList.Add(template);
                }
            }
        }
    }
}
