using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Xamarin.Plugin.Calendar.Models
{
    internal abstract class BindableBase<TData> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();
        private readonly Dictionary<string, PropertyChangedEventArgs> _propertyChangedArgs = new Dictionary<string, PropertyChangedEventArgs>();

        protected TProperty GetProperty<TProperty>(TProperty defaultValue = default, [CallerMemberName] string propertyName = "")
        {
            if (!_properties.ContainsKey(propertyName))
                return defaultValue;

            return (TProperty)_properties[propertyName];
        }


        protected BindableBase<TData> SetProperty<TProperty>(TProperty value, [CallerMemberName] string propertyName = "")
            => SetProperty<TProperty>(value, new NotifyOther(null), propertyName);
        protected BindableBase<TData> SetProperty<TProperty>(TProperty value, NotifyOther notifyOther, [CallerMemberName] string propertyName = "")
        {
            if (!_properties.TryGetValue(propertyName, out object storedValue))
                AddProperty(propertyName, value);
            else if (storedValue is object && storedValue.Equals(value))
                return this;

            _properties[propertyName] = value;
            PropertyChanged?.Invoke(this, _propertyChangedArgs[propertyName]);

            foreach (string otherPropertyName in notifyOther)
            {
                if (!_propertyChangedArgs.ContainsKey(otherPropertyName))
                    _propertyChangedArgs.Add(otherPropertyName, new PropertyChangedEventArgs(otherPropertyName));

                PropertyChanged?.Invoke(this, _propertyChangedArgs[otherPropertyName]);
            }

            return this;
        }

        internal static NotifyOther Notify(params string[] otherPropertyNames)
        {
            return new NotifyOther(otherPropertyNames?.ToList());
        }


        private void AddProperty(string propertyName, object defaultValue)
        {
            if (!_propertyChangedArgs.ContainsKey(propertyName))
                _propertyChangedArgs.Add(propertyName, new PropertyChangedEventArgs(propertyName));

            _properties.Add(propertyName, defaultValue);
        }
    }

    internal class NotifyOther : IEnumerable<string>
    {
        private readonly List<string> _propertyNames;

        public NotifyOther(List<string> otherPropertyNames)
        {
            _propertyNames = otherPropertyNames ?? new List<string>(0);
        }

        public IEnumerator<string> GetEnumerator() => _propertyNames.GetEnumerator() ;

        IEnumerator IEnumerable.GetEnumerator() => _propertyNames.GetEnumerator();
    }
}
