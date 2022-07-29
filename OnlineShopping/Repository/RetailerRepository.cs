using OnlineShopping.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Repository
{
    public class RetailerRepository
    {
        online_shoppingContext db = new online_shoppingContext();

        public void AddRetailer(Retailer ret)
        {
            db.Retailers.Add(ret);
            db.SaveChanges();
        }

        public List<Retailer> GetRetailers()
        {
            List<Retailer> retailers = db.Retailers.ToList();

            return retailers;
        }

    }
}
