namespace TuringL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaintanceRecord")]
    public partial class MaintanceRecord
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string KeyWorld { get; set; }

        public string Description { get; set; }

        public int FaultType { get; set; }

        public string Solution { get; set; }

        [Required]
        [StringLength(50)]
        public string MiantanceUser { get; set; }

        public DateTime MaintanceStartTime { get; set; }

        public DateTime MiantanceOverTime { get; set; }

        public int MState { get; set; }

        public int RState { get; set; }

        [StringLength(50)]
        public string ProductId { get; set; }

        [StringLength(50)]
        public string FirstPartier { get; set; }

        public virtual ProductInfo ProductInfo { get; set; }
    }
}
