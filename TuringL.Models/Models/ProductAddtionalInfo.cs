namespace TuringL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductAddtionalInfo")]
    public partial class ProductAddtionalInfo
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int RType { get; set; }

        [StringLength(50)]
        public string Path { get; set; }

        [StringLength(50)]
        public string ProductId { get; set; }

        public int RState { get; set; }

        public virtual ProductInfo ProductInfo { get; set; }
    }
}
