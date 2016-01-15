using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.ViewModels
{
    public class InstallInfoView
    {
        public String Id { get; set; }

        public string Principal { get; set; }

        public string MaintancePeriod { get; set; }

        public string Site { get; set; }

        public string OverTime { get; set; }

        public string InstallMethod { get; set; }

        public string StartTime { get; set; }

        public int IState { get; set; }

        public string CNumber { get; set; }

        public string CheckTime { get; set; }

        public string ProductId { get; set; }
    }
}
