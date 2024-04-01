﻿using System.Collections.ObjectModel;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class ProductsViewModel:ViewModelBase
    {
        private IProductDataProvider _productDataProvider;

        public ProductsViewModel(IProductDataProvider productDataProvider)
        {
            _productDataProvider = productDataProvider; 
        }

        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        public override async Task LoadAsync()
        {
            if(Products.Any())
            {
                return;
            }
            var products = await _productDataProvider.GetAllAsync();
            if(products is not null)
            {
                foreach(var product in products)
                {
                    Products.Add(product);
                }
            }
        }
    }
}
