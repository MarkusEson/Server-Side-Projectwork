namespace Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Libdb : DbContext
    {
        public Libdb()
            : base("name=Libdb")
        {
        }

        public virtual DbSet<AUTHOR> AUTHOR { get; set; }
        public virtual DbSet<BOOK> BOOK { get; set; }
       
        public virtual DbSet<ADMINISTRATOR> ADMINISTRATOR { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AUTHOR>()
                .HasMany(e => e.BOOK)
                .WithMany(e => e.AUTHOR)
                .Map(m => m.ToTable("BOOK_AUTHOR").MapLeftKey("Aid").MapRightKey("ISBN"));
        }
    }
}
