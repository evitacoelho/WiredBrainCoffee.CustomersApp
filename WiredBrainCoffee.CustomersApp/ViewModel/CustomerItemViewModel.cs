using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    //this is a class to handle individual customer items

    public class CustomerItemViewModel:ViewModelBase
    {
        private readonly Customer _model;

        //constructor uses the customer model
        public CustomerItemViewModel(Customer model)
        {
            _model = model;
        }

        //Id prop should not be set from the UI
        public int Id => _model.Id;
        public string? FirstName
        {
            get => _model.FirstName; 
            set
            {
                _model.FirstName = value;
                RaisePropertyChanged();
            }  
        }
        public string? LastName
        {
            get => _model.LastName;
            set
            {
                _model.LastName = value;
                RaisePropertyChanged();
            }
        }
        public bool IsDeveloper
        {
            get => _model.IsDeveloper;
            set
            {
                _model.IsDeveloper = value;
                RaisePropertyChanged();
            }
        }

    }
}
