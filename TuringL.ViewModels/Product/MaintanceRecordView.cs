using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.ViewModels
{
    public class MaintanceRecordView
    {
        public string Id { get; set; }

        public string KeyWorld { get; set; }

        public string Description { get; set; }

        public int FaultType { get; set; }

        public string Solution { get; set; }

        public string MiantanceUser { get; set; }

        public DateTime MaintanceStartTime { get; set; }

        public DateTime MiantanceOverTime { get; set; }

        public int MState { get; set; }

        //public int RState { get; set; }
    }
}
