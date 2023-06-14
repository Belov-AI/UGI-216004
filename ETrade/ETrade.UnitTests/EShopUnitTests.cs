using ETrade;
using System.Linq;

namespace ETradeUnitTests
{
    [TestFixture]
    public class EShopUnitTests
    {
        [Test]
        public void CountTest()
        {
            var shop = GetEShop();
            Assert.That(shop.Count("Телевизор", "Samsung UE55TU7097U"), Is.EqualTo(10));
            Assert.That(shop.Count("Телефон", "Apple iPhone 12"), Is.EqualTo(30));
            Assert.That(shop.Count("Ноутбук", "Apple MacBook Air"), Is.EqualTo(5));
            Assert.That(shop.Count("Пылесос", "Samsung"), Is.EqualTo(0));
        }

        [Test]
        public void CountOrdersTest()
        {
            var shop = GetEShop();
            Assert.That(shop.CountOrders(), Is.EqualTo(5));
        }

        [Test]
        public void AddOrderTest()
        {
            var shop = new EShop(new OrderCreator());
            Assert.That(shop.CountOrders(), Is.EqualTo(0));

            var comodity = new Comodity("Телефон", "Apple iPhone 12", 84990);
            shop.AddOrder("Телефон\tApple iPhone 12\t84990\t10");
            Assert.That(shop.CountOrders(), Is.EqualTo(1));
            Assert.That(shop.Count("Телефон", "Apple iPhone 12"), Is.EqualTo(10));

            shop.AddOrder("Телефон\tApple iPhone 12\t84990\t5");
            Assert.That(shop.CountOrders(), Is.EqualTo(2));
            Assert.That(shop.Count("Телефон", "Apple iPhone 12"), Is.EqualTo(15));

            comodity = new Comodity("Телевизор", "Samsung UE55TU7097U", 39990);
            shop.AddOrder("Телевизор\tSamsung UE55TU7097U\t39990\t20");
            Assert.That(shop.CountOrders(), Is.EqualTo(3));
            Assert.That(shop.Count("Телефон", "Apple iPhone 12"), Is.EqualTo(15));
            Assert.That(shop.Count("Телевизор", "Samsung UE55TU7097U"), Is.EqualTo(20));
        }

        [Test]
        public void RemoveOrderTest()
        {
            var shop = GetEShop();
            Assert.That(shop.CountOrders(), Is.EqualTo(5));
            Assert.That(shop.Count("Телефон", "Apple iPhone 12"), Is.EqualTo(30));
          
            shop.RemoveOrder(2);
            Assert.That(shop.CountOrders(), Is.EqualTo(4));
            Assert.That(shop.Count("Телефон", "Apple iPhone 12"), Is.EqualTo(10));
        }

        [Test]
        public void ExecuteOrdersTest()
        {
            var shop = GetEShop();
            Assert.That(shop.CountOrders(), Is.EqualTo(5));

            shop.ExecuteOrders("Телефон", "Apple iPhone 12", 22);
            Assert.That(shop.CountOrders(), Is.EqualTo(4));
            Assert.That(shop.Count("Телефон", "Apple iPhone 12"), Is.EqualTo(8));
        }

        public EShop GetEShop()
        {
            var shop = new EShop(new OrderCreator());
            var a = new Comodity("Телевизор", "Samsung UE55TU7097U", 39990);
            var b = new Comodity("Телефон", "Apple iPhone 12", 84990);
            var c = new Comodity("Ноутбук", "Apple MacBook Air", 74990);

            shop.AddOrder("Телевизор\tSamsung UE55TU7097U\t39990\t10");
            shop.AddOrder("Телефон\tApple iPhone 12\t84990\t20");
            shop.AddOrder("Ноутбук\tApple MacBook Air\t74990\t5");
            shop.AddOrder("Телефон\tApple iPhone 12\t84990\t5");
            shop.AddOrder("Телефон\tApple iPhone 12\t84990\t5");

            return shop;
        }
    }
}
