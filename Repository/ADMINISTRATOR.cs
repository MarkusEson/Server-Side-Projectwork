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
        public string UserName { get; set; }

        [StringLength(128)]
        public string PassSalt { get; set; }

        [StringLength(128)]
        public string PassHash { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        public string AdminDesc { get; set; }

    }
}