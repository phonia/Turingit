namespace TuringL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InstallInfo")]
    public partial class InstallInfo
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Principal { get; set; }

        [StringLength(50)]
        public string MaintancePeriod { get; set; }

        public string Site { get; set; }

        public DateTime OverTime { get; set; }

        [StringLength(50)]
        public string InstallMethod { get; set; }

        public DateTime StartTime { get; set; }

        public int IState { get; set; }

        [StringLength(50)]
        public string CNumber { get; set; }

        public DateTime CheckTime { get; set; }

        [StringLength(50)]
        public string ProductId { get; set; }

        public int RState { get; set; }

        public virtual ProductInfo ProductInfo { get; set; }
    }
}
