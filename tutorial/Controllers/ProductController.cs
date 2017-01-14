using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace tutorial.Controllers
{
    public class ProductController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();

        // GET api/<controller>
        public IQueryable<Product> Get()
        {
            return db.Products;
        }

        // GET api/<controller>/5
        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        // POST api/<controller>
        public void Post([FromBody] dynamic value)
        {
            string iProductName = value;

            if (value != null && (value as string).Length > 40)
            {
                iProductName = iProductName.Substring(0, 40);
            }

            var NewProduct = db.Products.Add(new Product()
            {
                CategoryID = 1,
                SupplierID = 1,
                ProductName = iProductName,
                QuantityPerUnit = "99 boxes",
                UnitPrice = 100,
                UnitsInStock = 10,
                UnitsOnOrder = 0,
                ReorderLevel = 5,
                Discontinued = false
            });

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}