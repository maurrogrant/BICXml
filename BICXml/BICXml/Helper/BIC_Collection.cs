using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BICXml.Helper
{
    public class BIC_Collection<T> : ObservableCollection<T>
    {
        private readonly Queue<NotifyCollectionChangedEventArgs> collectionChangeQueue;
        private bool isUpdatePaused;

        public BIC_Collection() : base()
        {
            collectionChangeQueue = new Queue<NotifyCollectionChangedEventArgs>();
        }

        public bool IsUpdatePaused
        {
            get
            {
                return isUpdatePaused;
            }
            set
            {
                isUpdatePaused = value;

                if (!value)
                {
                    while (collectionChangeQueue.Count > 0)
                    {
                        OnCollectionChanged(collectionChangeQueue.Dequeue());
                    }
                }
            }
        }
    }
}
