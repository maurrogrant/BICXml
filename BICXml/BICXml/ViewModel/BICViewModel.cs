using BICXml.Helper;
using BICXml.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace BICXml.ViewModel
{
    class BICViewModel : BaseViewModel
    {
        private string xmlPath = Directory.GetCurrentDirectory().Remove(Directory.GetCurrentDirectory().IndexOf("bin")) + "Xml\\20220620_ED807_full.xml";
        public List<BICModel> TempData;
        private object lockObject = new object();

        #region Commands

        public string XMLPath
        {
            get
            {
                return xmlPath;
            }
            set
            {
                xmlPath = value;
                OnPropertyChanged("XMLPath");
            }
        }

        private ICommand xmlProcessCommand;
        public ICommand XMLProcessCommand
        {
            get
            {
                if (xmlProcessCommand == null)
                {
                    xmlProcessCommand = new RelayCommand(ProcessXML);
                }
                return xmlProcessCommand;
            }
        }

        private string searchQuery;

        public string SearchQuery
        {
            get
            {
                return searchQuery;
            }
            set
            {
                searchQuery = value;
                OnPropertyChanged("SearchQuery");
            }
        }

        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(QuerySearch);
                }
                return searchCommand;
            }
        }

        private ICommand clearcommand;
        public ICommand Clearcommand
        {
            get
            {
                if (clearcommand == null)
                {
                    clearcommand = new RelayCommand(ClearSearchQuery);
                }
                return clearcommand;
            }
        }

        #endregion

        #region Properties

        private BIC_Collection<BICModel> bicListCollection;

        public BIC_Collection<BICModel> BICListCollection
        {
            get
            {
                if (bicListCollection == null)
                {
                    bicListCollection = new BIC_Collection<BICModel>();
                }
                return bicListCollection;
            }
            set
            {
                bicListCollection = value;
                OnPropertyChanged("BICList");
            }
        }

        private ObservableCollection<BICModel> querySearchResults;
        public ObservableCollection<BICModel> QuarySearchResults
        {
            get
            {
                if (querySearchResults == null)
                {
                    querySearchResults = new ObservableCollection<BICModel>();
                }
                return querySearchResults;
            }
            set
            {
                querySearchResults = value;
            }

        }

        #endregion

        public bool ValidateSchema(string xmlpath)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                xml.Load(xmlpath);
                xml.Schemas.Add(null, Directory.GetCurrentDirectory().Remove(Directory.GetCurrentDirectory().IndexOf("bin")) + "Xml\\cbr_ed807_v2022.2.1.xsd");
            }
            catch (FileNotFoundException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return false;
            }

            try
            {
                xml.Validate(null);
            }
            catch (XmlSchemaValidationException)
            {
                return false;
            }
            return true;
        }

        public void ProcessXML(object param)
        {
            string path = param as string;

            if (!ValidateSchema(path))
            {
                System.Windows.MessageBox.Show("Ошибка. Валидация XML файла не пройдена");
                return;
            }

            lock (lockObject)
            {
                XDocument xmlDoc = null;
                try
                {
                    FileStream xmlStream = new FileStream(xmlPath, FileMode.Open);
                    xmlDoc = XDocument.Load(xmlStream);
                }
                catch (FileNotFoundException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }

                if (xmlDoc != null)
                {
                    BICListCollection.IsUpdatePaused = true;

                    foreach (XElement el in xmlDoc.Root.Elements())
                    {
                        string NameP = null;
                        string Adress = null;

                        foreach (XAttribute attr in el.Attributes())

                            foreach (XElement element in el.Elements())
                            {
                                foreach (XAttribute et in element.Attributes())
                                {
                                    switch (et.Name.ToString())
                                    {
                                        case "NameP":
                                            NameP = et.Value;
                                            break;
                                        case string a when new List<string> { "Ind", "Tnp", "Nnp", "Adr" }.Contains(a):
                                            Adress += et.Value + " ";
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                        BICListCollection.Add(
                            new BICModel
                            {
                                BIC_num = el.Attribute("BIC").Value,
                                OgranizationName = NameP,
                                Adress = Adress
                            }
                                );
                    }

                    BICListCollection.IsUpdatePaused = false;
                }
            }
        }

        public void QuerySearch(object param)
        {
            string searchQuery = param as string;

            if (searchQuery == null)
            {
                System.Windows.MessageBox.Show("Некорректные данные для поиска!");
                return;
            }

            if (TempData != null && TempData.Count != 0)
            {
                BICListCollection.Clear();
                for (int i = 0; i < TempData.Count; i++)
                    BICListCollection.Add(TempData[i]);
            }

            QuarySearchResults = new ObservableCollection<BICModel>(BICListCollection
                  .Where(x => x.BIC_num.Contains(searchQuery) ||
                              x.OgranizationName.ToLower().Contains(searchQuery.ToLower()) ||
                              x.Adress != null && x.Adress.ToLower().Contains(searchQuery.ToLower())));

            TempData = BICListCollection.ToList();

            BICListCollection.Clear();

            for (int i = 0; i < QuarySearchResults.Count; i++)
                BICListCollection.Add(QuarySearchResults[i]);
        }

        public void ClearSearchQuery(object param)
        {
            BICListCollection.Clear();

            SearchQuery = null;

            for (int i = 0; i < TempData.Count; i++)
                BICListCollection.Add(TempData[i]);

            TempData.Clear();
        }
    }
}
