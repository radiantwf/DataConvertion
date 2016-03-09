using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConvertion.DataType {
    /// <summary>
    /// 业务类型
    /// </summary>
    public enum BusinessTypeType {
        /// <summary>
        /// 交易记录
        /// </summary>
        Transactions,
        /// <summary>
        /// 开户信息
        /// </summary>
        OpenningAccounts,
        /// <summary>
        /// 电子交易记录
        /// </summary>
        ElectronicTransactions,
        /// <summary>
        /// 银联交易记录
        /// </summary>
        ChinaUnionPayTransactions,
    };

    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileTypeType {
        /// <summary>
        /// Excel文件
        /// </summary>
        Xls,
        /// <summary>
        /// 文本文件
        /// </summary>
        Txt,
    };

    public class InputFileTemplate {
        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessTypeType BusinessType;
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileTypeType FileType;
        /// <summary>
        /// 所属银行
        /// </summary>
        public string Bank;
    }
}
