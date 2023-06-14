using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade
{
    public interface IEShopOrderCreator
    {
        IEShopOrderInstance Create(int ID, string record);
    }

    public interface IEShopOrderInstance
    {
        int ID { get; }
        IEShopComodity Goods { get; }
        int Quantity { get; set; }

    }

    public interface IEShopComodity
    {
        string Category { get; }
        string Name { get; }
        int Price { get; set; }
    }

}
