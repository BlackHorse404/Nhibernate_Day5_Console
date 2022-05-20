using FluentNHibernate.Mapping;
using Nhibernate_Day5_Console.Models;

namespace CastleWindsor_Day4_Console.Data.MapObject
{
    public class MonHocMap : ClassMap<MonHoc>
    {
        //map attribute
        public MonHocMap()
        {
            Id(x => x.MaMonHoc,"MaMonHoc");
            Map(x => x.TenMonHoc,"TenMonHoc");
            Map(x => x.SoTiet,"SoTiet");
            Map(x => x.LoaiMon,"LoaiMon");
            Table("MonHoc");
        }
        

    }
}
