using FluentNHibernate.Mapping;
using Nhibernate_Day5_Console.Models;

namespace Nhibernate_Day5_Console.Data.MapObject
{
    public class SinhVienMap : ClassMap<SinhVien>
    {
        public SinhVienMap()
        {
            Id(x => x.MaSinhVien,"MaSinhVien");
            Map(x => x.TenSinhVien, "TenSinhVien");
            Map(x => x.GioiTinh, "GioiTinh");
            Map(x => x.NgayThangNamSinh,"NgaySinh");
            Map(x => x.Lop,"Lop");
            Map(x => x.Khoa,"Khoa");
            Table("SinhVien");
        }
    }
}
