using FluentNHibernate.Mapping;
using Nhibernate_Day5_Console.Models;

namespace CastleWindsor_Day4_Console.Data.MapObject
{
    public class BangDiemMap : ClassMap<BangDiem>
    {
        public BangDiemMap()
        {
            Id(x => x.Stt, "STT");
            Map(x => x.DiemThanhPhan, "DiemThanhPhan");
            Map(x => x.DiemQuaTrinh, "DiemQuaTrinh");
            References(x => x.MonHoc,"MaMonHoc");
            References(x => x.SinhVien,"MaSinhVien");
            Table("BangDiem");
        }
    }
}
