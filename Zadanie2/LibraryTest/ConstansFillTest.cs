using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    [TestClass]
    public class ConstansFillTest
    {
        [TestMethod]
        public void DI()
        {
            DataRepository dataRepository = new DataRepository();
            dataRepository.FillData = new ConstansFill();

            Assert.AreEqual("Jan", dataRepository.GetClient("1").FirstName);
            Assert.AreEqual("Kowalski", dataRepository.GetClient("1").LastName);
            Assert.AreEqual("Krzyzacy", dataRepository.GetCatalog("1").Title);
            Assert.AreEqual("Henryk Sienkiewicz", dataRepository.GetCatalog("1").Author);
            Assert.AreEqual(10, dataRepository.GetInventory("1").Amount);
        }
    }
}
