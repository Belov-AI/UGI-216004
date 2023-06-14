using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade
{
    public class EShop
    {
        List<IEShopOrderInstance> orders;
        IEShopOrderCreator creator;
        int nextID;
        // @@@ DIP - Dependency Inversion Principle -
        // правило, согласно которому классы должны зависеть от абстракций
        // (такими абстракциями зачастую выступают интерфейсы), а не напрямую
        // от других классов. Всем классам желательно обращаться к другим исключительно
        // по заданным интерфейсам для избежания ненужных модификаций кода, когда
        // перед нами встаёт задача изменить реализацию того или иного компонента системы
        // В данном конкретном примере метод AddOrder изначально полагался на класс OrderCreator,
        // - на конкретную реализацию фабрики Order'ов, которая производила их из строки с параметрами.
        // Если бы у нас появилась нужда заменить OrderCreator на другой класс, который бы работал,
        // например, с http-запросами, нам бы пришлось переписывать использующие его методы здесь, в этом классе,
        // что против правил чистого кода. Код должен быть открыт к расширению (прописыванию новых реализаций,
        // аналогичных OrderCreator), но закрыт для модификаций 
        public EShop(IEShopOrderCreator creator)
        {
            this.orders = new List<IEShopOrderInstance>();
            this.creator = creator;
            nextID = 1;
        }

        public void AddOrder(string record)
        {
            orders.Add(creator.Create(nextID++, record));
        }

        public void RemoveOrder(int id)
        {
            orders.RemoveAt(--id); // @@@ учитываем, что нумерация идёт с единицы
            nextID--;
        }


        public void ExecuteOrders(string category, string name, int totalQuantity)
        {
            // totalQuantity — общее количество данного товара, поступившее от поставщиков, 
            // т. е. которое можно выдать по заказам.
            // Определить метод, который находит заказы на данный товар
            // и, если в заказе количество не больше, чем оставшееся общее колличество,
            // то удаляет заказ из списка. Если количество в заказе больше, 
            // чем оставшееся общее колличество (заказ будет выполнен частично),
            // то уменьшает количество товаров в заказе, оставляя заказ в списке

            orders = new List<IEShopOrderInstance>(orders
                .Where(el =>
                {
                    var good = el.Goods;
                    if (totalQuantity <= 0 ||
                        !((good.Category == category) && (good.Name == name)))
                    {
                        return true;
                    }

                    
                    var oldQuantity = totalQuantity;
                    totalQuantity -= el.Quantity;
                    if (totalQuantity > 0)
                        return false;

                    el.Quantity -= oldQuantity;
                    return true;
                }));
        }

        public int Count(string category, string name)
        {
            // Метод должен возвращать общее количество данного товара,
            // заказанного во всех имеющихся заказах
            
            return orders
                .Where(el => (el.Goods.Category == category) && (el.Goods.Name == name))
                .Sum(el => el.Quantity);
        }

        public int CountOrders()
        {
            // Мтеод должен возвращать общще количество заказов

            return orders.Count();
        }
    }
}
