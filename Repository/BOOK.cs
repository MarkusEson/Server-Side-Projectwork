namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BOOK")]
    public partial class BOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOK()
        {
            AUTHOR = new HashSet<AUTHOR>();
        }

        [Key]
        [Required]
        [StringLength(15)]
        public string ISBN { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(10)]
        public string PublicationYear { get; set; }

        [StringLength(255)]
        public string publicationinfo { get; set; }

        [Range(0,9999)]
        public short? pages { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AUTHOR> AUTHOR { get; set; }
    }
}
