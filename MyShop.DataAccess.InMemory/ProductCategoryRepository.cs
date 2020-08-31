using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();

            }

        }
        public void commit()
        {

            cache["productCategories"] = productCategories;
        }
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        public void update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public ProductCategory find(string Id)
        {
            ProductCategory product = productCategories.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("ProductCategory not found");
            }

        }
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void delete(String Id)
        {
            ProductCategory productCategoryTodelete = productCategories.Find(p => p.Id == Id);
            if (productCategoryTodelete != null)
            {
                productCategories.Remove(productCategoryTodelete);
            }
            else
            {
                throw new Exception("ProductCategory not found");
            }
        }
    }
}

