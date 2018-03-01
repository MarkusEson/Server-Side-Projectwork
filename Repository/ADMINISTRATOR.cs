namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADMINISTRATOR")]
    public partial class ADMINISTRATOR
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMINISTRATOR()
        {
        }

        [Key]
        public int AdminId { get; set; }

        [StringLength(20)]
        public string Fname { get; set; }

        [StringLength(20)]
        public string Lname { get; set; }

        [StringLength(200)]
        public string AdminDescription { get; set; }

    }
}