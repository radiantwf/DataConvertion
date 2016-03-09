using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Data;
using DataConvertion.DataType;

namespace DataConvertion {
    public abstract class InputFile {
        public BusinessTypeType businessType { set; get; }

        public abstract DataTable GetRecords(int row);

        private InputFileTemplate CustomTemplate { set; get; }
        private static void getTemplates() {

        }
    }

    public class InputXlsFile : InputFile {
        public override DataTable GetRecords(int row) {
            throw new MulticastNotSupportedException();
        }
    }
}
