using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibox.Models;
using Tibox.Repository;
using Tibox.UnitOfWork;

namespace Tibox.DataAccess.Tests
{
    [TestClass]
   public class OrderRepositoryTest
    {
        private readonly IUnitOfWork _unitOfWork;


        public OrderRepositoryTest()
        {
            _unitOfWork = new TiboxUnitOfWork();
        }
        [TestMethod]
        public void Get_All_Orders()
        {
            var result = _unitOfWork.Orders.GetAll();
            Assert.AreEqual(result.Count() > 0, true);
        }

        
        [TestMethod]
        public void Get_Order_By_Id()
        {

            var result = _unitOfWork.Orders.GetEntityById(1);
            Assert.AreEqual(result != null, true);
        }
        [TestMethod]
        public void Search_By_Names()
        {

            var Order = _unitOfWork.Orders.OrderByOrderNumber("543207");
            Assert.AreEqual(Order != null, true);

            Assert.AreEqual(Order.Id, 830); 
        }
        [TestMethod]
        public void Order_With_Orders()
        {

            var Order = _unitOfWork.Orders.OrderWithOrderItems(830);
            Assert.AreEqual(Order != null, true);

            Assert.AreEqual(Order.OrderItems.Any(), true); //almenos 1 ordenItem

            Assert.AreEqual(Order.Id, 830);
             
        }
    }
}
