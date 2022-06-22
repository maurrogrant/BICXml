using System.ComponentModel;

namespace BICXml.Model
{
    class BICModel : INotifyPropertyChanged
    {
        private string bic_num;
        public string BIC_num
        {
            get
            {
                return bic_num;
            }
            set
            {
                bic_num = value;
                OnPropertyChanged("BIC_num");
            }
        }

        private string ogranizationName;
        public string OgranizationName
        {
            get
            {
                return ogranizationName;
            }
            set
            {
                ogranizationName = value;
                OnPropertyChanged("OgranizationName");
            }
        }

        private string adress;
        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = value;
                OnPropertyChanged("Adress");
            }
        }

        public BICModel()
        {
            BIC_num = string.Empty;
            OgranizationName = string.Empty;
            Adress = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
