using Core.Models;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : Services<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository) : base(unitOfWork, repository)
        {
        }

        public ICollection<Product> GetAllProductsList()
        {
            return _unitOfWork.Product.GetAllProductsList();
        }

        public Product GetProductBySeoUrl(string seoUrl)
        {
            return _unitOfWork.Product.GetProductBySeoUrl(seoUrl);
        }

        public ICollection<Product> RandomProductListTake5()
        {
            return _unitOfWork.Product.RandomProductListTake5();
        }

        public Product UpdateProductGetAllInclude(int id)
        {
            return _unitOfWork.Product.UpdateProductGetAllInclude(id);
        }
    }
}
