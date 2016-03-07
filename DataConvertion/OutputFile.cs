using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace DataConvertion.OutputFormat {
    public class OutputFormat {

        private Dictionary<int, string> title;
        private Dictionary<int, string> DateType;

        /// <summary>
        /// 输出文件类型
        /// </summary>
        public enum OutputFileType {
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

        public OutputFileType FileType { set; get; }

        private static void getOutputFilesFomart() {
        }
    }
}
