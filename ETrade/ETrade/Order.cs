using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade
{
    public class Order : IEShopOrderInstance
    {
        public int ID { get; }
        public IEShopComodity Goods { get; }
        public int Quantity { get; set; }

        public Order(int id, IEShopComodity comodity, int quantity)
        {
            ID = id;
            Goods = comodity;
            Quantity = quantity;
        }

        public override string ToString()
        {
            //@@@ использовалось при разработке, написано просто так
            return $"{Goods}\t{Quantity}";
        }
    }
}
