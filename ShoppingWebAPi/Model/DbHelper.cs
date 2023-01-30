using Microsoft.EntityFrameworkCore;
using ShoppingWebAPi.EFCore;

namespace ShoppingWebAPi.Model
{
    public class DbHelper
    {
        private readonly DataContext _context;
        public DbHelper(DataContext context)
        {
                _context= context;  
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>(); 
            var listOfProdcuts =await _context.Products.ToListAsync();
            listOfProdcuts.ForEach(row => products.Add(new ProductModel()
            {
                Id = row.Id,
                Name = row.Name,
                Brand = row.Brand,
                Price = row.Price,
                Size = row.Size,
            }));
                    
            return products;
        }
        public ProductModel GetProductById(int id)
        {
            ProductModel response = new ProductModel();
            var row = _context.Products.Where(d => d.Id.Equals(id)).FirstOrDefault();
            return new ProductModel()
            {
                Brand = row.Brand,
                Id = row.Id,
                Name = row.Name,
                Price = row.Price,
                Size = row.Size
            };
        }
        public void SaveOrder(OrderModel orderModel)
        {
            Order dbTableResult = new Order();
            if (orderModel.Id>0)
            {
                dbTableResult = _context.Orders.Where(d => d.Id.Equals(orderModel.Id)).FirstOrDefault();
                if (dbTableResult != null)
                {
                    dbTableResult.Name = orderModel.Name;
                    dbTableResult.Address = orderModel.Address;
                    dbTableResult.Phone= orderModel.Phone;
                }
            }
            else
            {
                //POST
                dbTableResult.Phone = orderModel.Phone;
                dbTableResult.Address = orderModel.Address;
                dbTableResult.Name = orderModel.Name;
                dbTableResult.Product = _context.Products.Where(f => f.Id.Equals(orderModel.Product_id)).FirstOrDefault();
                _context.Orders.Add(dbTableResult);
            }
            _context.SaveChanges();
        }
        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
