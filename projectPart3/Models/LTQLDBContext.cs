namespace projectPart3.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LTQLDBContext : DbContext
    {
        public LTQLDBContext()
            : base("name=LTQLDBContext")
        {
        }

        public virtual DbSet<KhachHang> khachhangs { get; set; }
        public virtual DbSet<NhaCungCap> nhacungcaps { get; set; }
        public virtual DbSet<ChiTietHoaDon> chitiethoadons { get; set; }
        public virtual DbSet<HoaDon> hoadons { get; set; }
        public virtual DbSet<DanhMuc> danhmucs { get; set; }
        public virtual DbSet<Gia> gias { get; set; }
        public virtual DbSet<SanPham> sanphams { get; set; }

        internal object ToPagedList(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual DbSet<KHACH> khachs { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KhachHang>()
                .Property(e => e.ma_khach_hang);
        }
    }
}
