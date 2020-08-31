using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(String Id)
        {
            Product product = context.find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(product);
            }


        }
        [HttpPost]
        public ActionResult Edit(Product product, String Id)
        {
            Product productToEdit = context.find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.commit();

                return RedirectToAction("Index");
            }

        }
        public ActionResult Delete(String Id)
        {
            Product productToDelete = context.find(Id);
            if (productToDelete== null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productToDelete);
            }


        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(String Id)
        {
            Product productToDelete = context.find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                context.delete(Id);
                context.commit();
                return RedirectToAction("Index");
            }


        }



    }
}