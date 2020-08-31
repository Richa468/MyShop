using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;
        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        // GET: ProductManager

        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }
        public ActionResult create()
        {
            ProductCategory productCategories = new ProductCategory();
            return View(productCategories);
        }
        [HttpPost]
        public ActionResult create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(String Id)
        {
            ProductCategory productCategories = context.find(Id);
            if (productCategories == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productCategories);
            }


        }
        [HttpPost]
        public ActionResult Edit(ProductCategory product, String Id)
        {
            ProductCategory productCategoryToEdit = context.find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategoryToEdit);
                }
                productCategoryToEdit.Category = product.Category;
                

                context.commit();

                return RedirectToAction("Index");
            }

        }
        public ActionResult Delete(String Id)
        {
            ProductCategory productCategoryToDelete = context.find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productCategoryToDelete);
            }


        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(String Id)
        {
            ProductCategory productCategoryToDelete = context.find(Id);
            if (productCategoryToDelete == null)
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