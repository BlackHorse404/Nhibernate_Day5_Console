using System;
using System.Text;
using Nhibernate_Day5_Console.Utilities;
using Nhibernate_Day5_Console.Data;
using Nhibernate_Day5_Console.BusinessLogic;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Nhibernate_Day5_Console.Data.InterFace;

namespace Nhibernate_Day5_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //format encoding 
            Console.OutputEncoding = Encoding.UTF8;
            //Process     
            //sử dụng thư viện Castle Windsor
            try
            {
                WindsorContainer container = new WindsorContainer();
                container.Register(Component.For<ISinhVienDAL>().ImplementedBy<DataAccessList>(),
                    Component.For<SVBusinessLogic>());
                Execute.RunMain(container);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
