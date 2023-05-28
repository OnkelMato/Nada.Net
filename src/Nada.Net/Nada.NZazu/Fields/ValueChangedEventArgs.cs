using System;

namespace Nada.NZazu.Fields
{
    internal class ValueChangedEventArgs<T> : System.EventArgs
    {
        public ValueChangedEventArgs(string storeKey, Guid ctrlGuid, T oldValue, T newValue)
        {
            StoreKey = storeKey;
            CtrlGuid = ctrlGuid;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string StoreKey { get; }
        public Guid CtrlGuid { get; }
        public T OldValue { get; }
        public T NewValue { get; }
    }
}