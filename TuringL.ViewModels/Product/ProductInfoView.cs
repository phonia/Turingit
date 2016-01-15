using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.ViewModels
{
    public class ProductInfoView
    {
        public String Id { get; set; }
        public string Name { get; set; }
        public string TypeVersion { get; set; }
        public String Note { get; set; }
        public List<ProductAddtionalInfoView> ProductAddtionalInfos { get; set; }
        public List<MaintanceRecordView> MaintanceRecords { get; set; }
        public List<InstallInfoView> InstallInfos { get; set; }
    }
}
