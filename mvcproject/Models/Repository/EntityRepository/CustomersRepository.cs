using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcproject.Models.Repository
{
    public class CustomersRepository
    {
        private readonly ICustomersRepository _customersRepository = null;

        public CustomersRepository(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public bool Create(客戶資料 customer)
        {
            if (customer == null)
            {
                return false;
            } else
            {
                _customersRepository.Create(customer);
                return true;
            }
        }
        public bool Update(客戶資料 customer) {
            if (customer == null)
            {
                return false;
            } else
            {
                _customersRepository.Update(customer);
                return true;
            }
        }
        public bool Delete(客戶資料 customer) {
            if (customer == null)
            {
                return false;
            } else
            {
                _customersRepository.Delete(customer);
                return true;
            }
        }

        public 客戶資料 Get(int customerId)
        {
            if (customerId != 0)
                return _customersRepository.Get(customerId);
            return null;
        }

        public IEnumerable<客戶資料> GetAll()
        {
            return _customersRepository.GetAll();
        }
    }
}