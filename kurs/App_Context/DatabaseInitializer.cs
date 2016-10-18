using Autobase.DAO;
using Autobase.Models;
using Autobase.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Autobase.App_Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        private const int ROW_COUNT = 100;
        readonly CarDAO carDAO;
        readonly AccountDAO accountDAO;
        readonly OrderDAO orderDAO;
        readonly TripDAO tripDAO;

        public DatabaseInitializer(CarDAO carDAO, AccountDAO accountDAO, OrderDAO orderDAO, TripDAO tripDAO)
        {
            this.carDAO = carDAO;
            this.accountDAO = accountDAO;
            this.orderDAO = orderDAO;
        }

        protected override void Seed(ApplicationContext context)
        {
            
        }

        private void FillCarsTable()
        {
            Car car = null;
            for (int i = 0; i < ROW_COUNT; i++)
            {
                car = new Car();
                car.CarName = RandomUtil.GetInstance.GetRandomString;
                car.CarSpeed = 100 * RandomUtil.GetInstance.GetRandomDouble;
                car.CarCapacity = 100 * RandomUtil.GetInstance.GetRandomDouble;
                car.IsHealthy = RandomUtil.GetInstance.GetRandomBool;
                carDAO.Create(car);
            }
        }

        private void FillAccountsTable()
        {
            Account acc = null;
            for (int i = 0; i < ROW_COUNT; i++)
            {
                acc = new Account();
                acc.AccountName = RandomUtil.GetInstance.GetRandomString;
                acc.IsDispatcher = RandomUtil.GetInstance.GetRandomBool;
                acc.Password = RandomUtil.GetInstance.GetRandomString;
                //if (!acc.IsDispatcher)
                //{
                //    acc.Car = car;
                //}
                accountDAO.Create(acc);
            }
        }

        
    }
}