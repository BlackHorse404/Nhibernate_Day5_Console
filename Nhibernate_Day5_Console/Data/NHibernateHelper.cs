using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Nhibernate_Day5_Console.Models;
using System;
using Nhibernate_Day5_Console.Utilities;
using System.Threading;


namespace Nhibernate_Day5_Console.Data
{
    public class NHibernateHelper
    {
        //fields
        private static string ServerName, DatabaseName;
        private static ISessionFactory _sessionFactory;

        //properties
        public static string GetServerName { get => ServerName; }
        public static string GetDBName { get => DatabaseName; }
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        //method Nhibernate Helper
        //input Server Name + DB Name
        private static void InputDBServer()
        {
            //nhập vào Server Name, Database Name để lấy thông tin kết nối
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[!] Để trống ServerName nếu muốn lấy tên Server mặc định tự nhận theo máy !");
            Console.ForegroundColor = ConsoleColor.Green; Console.Write("[>] "); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Nhập vào Server Name: ");
            ServerName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green; Console.Write("[>] "); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Nhập vào Database Name (DatabaseSinhVien): ");
            DatabaseName = Console.ReadLine();
            Console.ResetColor();
            //thông báo đang kết nối
            ExceptionNotice.WarningConnectingDB();
            //thông báo kết nối khi thành công 
            ExceptionNotice.SuccessConnectDatabase(); Thread.Sleep(2000);
        }
        //Init Session Connect by Class Map
        public static ISession InitializeSessionFactory()
        {
            InputDBServer();//input Server Name + Database Name
            //connect database by Nhibernate Fluent
            try
            {
                string connectionString = @"Data Source=" + ServerName + ";Initial Catalog=" + DatabaseName + ";Integrated Security=True";
                _sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<SinhVien>()
                    .AddFromAssemblyOf<MonHoc>()
                    .AddFromAssemblyOf<BangDiem>()
                )
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                .BuildSessionFactory();
                return SessionFactory.OpenSession();
            }
            catch (Exception e)//catch exception if error during run connect DB with Fluent Nhibernate
            {
                ExceptionNotice.ColorError();
                Console.WriteLine(e.Message);
                ExceptionNotice.InfoExit();
                Console.ReadLine();
                Environment.Exit(0);
                return null;
            }
        }
        //connect database again when connected before (know Servername + DB Name) support for update Database when have change
        public static ISession SessionFactoryOld()
        {
            //connect database by Nhibernate Fluent
            try
            {
                string connectionString = @"Data Source=" + ServerName + ";Initial Catalog=" + DatabaseName + ";Integrated Security=True";
                _sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                connectionString).ShowSql())
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<BangDiem>()
                )
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                    .Create(false, false)
                )
                .BuildSessionFactory();
                return SessionFactory.OpenSession();
            }
            catch (Exception e)
            {
                ExceptionNotice.ColorError();
                Console.WriteLine(e.Message);
                ExceptionNotice.InfoExit();
                Console.ReadLine();
                Environment.Exit(0);
                return null;
            }
        }
    }
}