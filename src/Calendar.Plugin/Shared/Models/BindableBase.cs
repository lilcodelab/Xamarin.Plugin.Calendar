using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.Plugin.Calendar.Models
{
    internal abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, PropertyChangedEventArgs> _propertyChangedArgs = new Dictionary<string, PropertyChangedEventArgs>();
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();
        private readonly Dictionary<string, List<PropertyChangedEventArgs>> _propertyDependencies = new Dictionary<string, List<PropertyChangedEventArgs>>();

        protected TData GetProperty<TData>(TData defaultValue = default, [CallerMemberName] string propertyName = "")
        {
            if (!_properties.ContainsKey(propertyName))
                AddProperty(propertyName, defaultValue);

            return (TData)_properties[propertyName];
        }

        protected void SetProperty<TData>(TData value, [CallerMemberName] string propertyName = "")
        {
            if (!_properties.ContainsKey(propertyName))
                AddProperty(propertyName, value);
            else if (value.Equals(_properties[propertyName]))
                return;

            _properties[propertyName] = value;
            PropertyChanged?.Invoke(this, _propertyChangedArgs[propertyName]);

            if (_propertyDependencies.TryGetValue(propertyName, out List<PropertyChangedEventArgs> alsoNotifyFor))
            {
                foreach (var dependentPropertyArgs in alsoNotifyFor)
                    PropertyChanged?.Invoke(this, dependentPropertyArgs);
            }
        }

        protected void SetupPropertyDependencies(string propertyName, params string[] dependentProperties)
        {
            var dependentPropertyArgs = new List<PropertyChangedEventArgs>();

            foreach (var dependentProperty in dependentProperties)
            {
                if (!_propertyChangedArgs.ContainsKey(dependentProperty))
                    _propertyChangedArgs.Add(dependentProperty, new PropertyChangedEventArgs(dependentProperty));

                dependentPropertyArgs.Add(_propertyChangedArgs[dependentProperty]);
            }

            _propertyDependencies.Add(propertyName, dependentPropertyArgs);
        }

        private void AddProperty(string propertyName, object defaultValue)
        {
            if (!_propertyChangedArgs.ContainsKey(propertyName))
                _propertyChangedArgs.Add(propertyName, new PropertyChangedEventArgs(propertyName));

            _properties.Add(propertyName, defaultValue);
        }
    }
}
