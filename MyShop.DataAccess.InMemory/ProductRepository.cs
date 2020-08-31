using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"]as List<Product>;
            if(products==null)
            {
                products = new List<Product>();

            }

        }
        public void commit()
        {

            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if(productToUpdate!=null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public Product find(string Id)
        {
            Product product = products.Find(p => p.Id ==Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
        public IQueryable<Product>Collection()
        {
            return products.AsQueryable();
        }

        public void delete(String Id)
        {
            Product productTodelete = products.Find(p => p.Id ==Id);
            if (productTodelete != null)
            {
                products.Remove(productTodelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
