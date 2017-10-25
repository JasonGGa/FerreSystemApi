using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreSystemWPF
{
    public class NewProduct : IEditableObject, INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private double? _purchPriceSol;
        public double? PurchPriceSol
        {
            get
            {
                return _purchPriceSol;
            }
            set
            {
                if (_purchPriceSol == value) return;
                _purchPriceSol = value;
                OnPropertyChanged("PurchPriceSol");
            }
        }

        private double? _purchPriceDol;
        public double? PurchPriceDol
        {
            get
            {
                return _purchPriceDol;
            }
            set
            {
                if (_purchPriceDol == value) return;
                _purchPriceDol = value;
                OnPropertyChanged("PurchPriceDol");
            }
        }

        private double? _wholesalePrice;
        public double? WholesalePrice
        {
            get
            {
                return _wholesalePrice;
            }
            set
            {
                if (_wholesalePrice == value) return;
                _wholesalePrice = value;
                OnPropertyChanged("WholesalePrice");
            }
        }

        private double? _retailPrice;
        public double? RetailPrice
        {
            get
            {
                return _retailPrice;
            }
            set
            {
                if (_retailPrice == value) return;
                _retailPrice = value;
                OnPropertyChanged("RetailPrice");
            }
        }

        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity == value) return;
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        
        #region IEditableObject

        private NewProduct backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as NewProduct;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.Name = backupCopy.Name;
            this.PurchPriceSol = backupCopy.PurchPriceSol;
            this.PurchPriceDol = backupCopy.PurchPriceDol;
            this.WholesalePrice = backupCopy.WholesalePrice;
            this.RetailPrice = backupCopy.RetailPrice;
            this.Quantity = backupCopy.Quantity;
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
