using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade
{
    public class OrderCreator : IEShopOrderCreator
    {
        public IEShopOrderInstance Create(int id, string record)
        {
            // Реализовать метод так, чтобы из строки в формате
            // категория, название, цена, количество (в этом порядке), разделенные табуляцией

            // @@@ SRP - Single Responsibility Principle - 
            // мы выносим создание заказов из строки ради того, чтобы изначальный объект
            // (order) не нёс в себе методов, которые ему не нужны для исполнения своей обязанности,
            // которая у него одна - выступать представлением заказа с конкретными параметрами.
            // кроме того, если мы вдруг решим изменить текущую процедуру создания заказов на более реалистичную
            // (например, из прочитанных http-запросов), то для такого манёвра нам достаточно изменить текущий класс OrderCreator,
            // не трогая при этом Order, что будет разумнее в контексте ООП, ведь общие принципы работы объектов класса Order
            // не изменятся - они всё так же будут выступать представлением заказов. 
            var splits = record.Split('\t');
            var cat = splits[0];
            var name = splits[1];
            var price = int.Parse(splits[2]);
            
            var commod = new Comodity(cat, name, price);
            var quant = int.Parse(splits[splits.Length - 1]);
            var newOrder = new Order(id, commod, quant);
            return newOrder;
        }
    }
}
