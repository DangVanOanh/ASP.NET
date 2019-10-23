using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.DAO
{
    public class CategoryDAO
    {
        OnlineShopDbContext dbContext = null;


        private static CategoryDAO instance;


        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return instance; }
            private set { instance = value; }
        }

        private CategoryDAO()
        {
            dbContext = new OnlineShopDbContext();
        }


        public List<Category> GetListAll()
        {
            return dbContext.Category.Where(m => m.Status == true).ToList();
        }

    }
}
