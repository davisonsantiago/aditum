using Restaurant.API.Models;
using Restaurant.API.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Repository.Interface
{
    public interface IRestaurantRepository
    {
        RestaurantModel Create(RestaurantModel restaurant);
        List<RestaurantModel> Read(RestaurantFilter filter);
        RestaurantModel Read(int id);
        List<RestaurantModel> Read();
        bool Update(RestaurantModel restaurant);
        bool Delete(int id);
    }
}
