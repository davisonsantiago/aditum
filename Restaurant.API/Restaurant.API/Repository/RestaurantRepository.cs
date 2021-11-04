using Restaurant.API.Database;
using Restaurant.API.Models;
using Restaurant.API.Models.Filter;
using Restaurant.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.API.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApiContext _context;

        public RestaurantRepository(ApiContext context)
        {
            _context = context;
        }

        public RestaurantModel Create(RestaurantModel restaurant)
        {
            var result = _context.Restaurants.Add(restaurant);

            _context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(T => T.Id == id);

            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<RestaurantModel> Read()
        {
            return _context.Restaurants.ToList();
        }

        public RestaurantModel Read(int id)
        {
            return _context.Restaurants.FirstOrDefault(T => T.Id == id);
        }

        public List<RestaurantModel> Read(RestaurantFilter filter)
        {
            TimeSpan hour;

            var validTS = TimeSpan.TryParse(filter.Hour, out hour);

            var list = _context.Restaurants.ToList();

            if (validTS)
            {
                list = list.Where(T => T.Open <= hour && T.Close >= hour).ToList();
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                list = list.Where(T => T.Name.Contains(filter.Name)).ToList();
            }

            return list;
        }

        public bool Update(RestaurantModel restaurant)
        {
            var result = _context.Restaurants.FirstOrDefault(T => T.Id == restaurant.Id);

            if (result != null)
            {
                result.Name = restaurant.Name;
                result.Open = restaurant.Open;
                result.Close = restaurant.Close;

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
