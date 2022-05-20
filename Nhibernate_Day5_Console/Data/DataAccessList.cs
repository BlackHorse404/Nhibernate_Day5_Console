using System;
using System.Collections.Generic;
using Nhibernate_Day5_Console.Models;
using Nhibernate_Day5_Console.Utilities;
using Nhibernate_Day5_Console.Data.InterFace;
using System.Linq;

namespace Nhibernate_Day5_Console.Data
{
    public class DataAccessList : ISinhVienDAL
    {
        //thuộc tính 
        #region attribute
        private List<SinhVien> _lSinhVien = new List<SinhVien>();
        private List<MonHoc> _lMonHoc = new List<MonHoc>();
        private List<BangDiem> _lBangDiem = new List<BangDiem>();
        #endregion

        //constructor
        #region Constructor
        public DataAccessList()
        {
            ConnectDataBaseSinhVien();
        }
        #endregion

        //hàm kết nối tới Database
        #region Kết nối database + truy xuất dữ liệu
        //Hàm kết nối sqlDataBase
        public void ConnectDataBaseSinhVien()
        {
            using (var session = NHibernateHelper.InitializeSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var bds = session.Query<BangDiem>().ToList();
                    foreach (var bd in bds)
                    {
                        _lBangDiem.Add(bd);
                    }
                    var svs = session.Query<SinhVien>().ToList();
                    foreach (var sv in svs)
                    {
                        _lSinhVien.Add(sv);
                    }
                    var mhs = session.Query<MonHoc>().ToList();
                    foreach (var mh in mhs)
                    {
                        _lMonHoc.Add(mh);
                    }
                    transaction.Commit();
                }
            }
            Console.Clear();
        }
        //update core when change
        public void updateScoreLive(string maSinhVien, string maMH, double diemTPnew, double diemQTnew)
        {
            using (var session = NHibernateHelper.SessionFactoryOld())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var tempObjects = session.Query<BangDiem>().Where(x => (x.MonHoc.MaMonHoc.Equals(maMH) && x.SinhVien.MaSinhVien.Equals(maSinhVien))).SingleOrDefault();
                    if(tempObjects != null)
                    {
                        tempObjects.DiemThanhPhan = diemTPnew;
                        tempObjects.DiemQuaTrinh = diemQTnew;
                    }
                    session.SaveOrUpdate(tempObjects);
                    transaction.Commit();
                }
            }
        }
        #endregion

        //method select get All
        #region Method Select Object
        //method select all sinh vien
        public List<SinhVien> GetAllSinhVien() => _lSinhVien;
        //method select all Mon Hoc
        public List<MonHoc> GetAllMonHoc() => _lMonHoc;
        //method select all Bang Diem
        public List<BangDiem> GetAllBangDiem() => _lBangDiem;
        //method select all in one
        public void GetAllInOne(ref List<SinhVien> lsv, ref List<MonHoc> lmh, ref List<BangDiem> lbd)
        {
            lsv = _lSinhVien;
            lmh = _lMonHoc;
            lbd = _lBangDiem;
        }

        #endregion
    }
}
