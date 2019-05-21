using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xamarin.Plugin.Calendar.Models
{
    /// <summary>
    /// Class for calendar events (extends Dictionary<DateTime, ICollection>)
    /// </summary>
    public class EventCollection : Dictionary<DateTime, ICollection>
    {
        #region ctor

        public EventCollection() : base()
        { }

        public EventCollection(int capacity) : base(capacity)
        { }

        public EventCollection(IEqualityComparer<DateTime> comparer) : base(comparer)
        { }

        public EventCollection(IDictionary<DateTime, ICollection> dictionary) : base(dictionary)
        { }

        public EventCollection(int capacity, IEqualityComparer<DateTime> comparer) : base(capacity, comparer)
        { }

        public EventCollection(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

        public EventCollection(IDictionary<DateTime, ICollection> dictionary, IEqualityComparer<DateTime> comparer) : base(dictionary, comparer)
        { }

        #endregion

        /// <summary>
        /// Removed a collection of values for specific date
        /// </summary>
        /// <param name="key">Event DateTime</param>
        /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if key is not found in the System.Collections.Generic.Dictionary`2.</returns>
        public new bool Remove(DateTime key)
        {
            var removed = base.Remove(key.Date);

            if (removed)
                CollectionChanged?.Invoke(this, new EventCollectionChangedArgs { Item = key.Date, Type = EventCollectionChangedType.Remove });

            return removed;
        }

        /// <summary>
        /// Add collection of values for specific date
        /// </summary>
        /// <param name="key">Event DateTime</param>
        /// <param name="value">Collection of events for date</param>
        public new void Add(DateTime key, ICollection value)
        {
            base.Add(key.Date, value);
            CollectionChanged?.Invoke(this, new EventCollectionChangedArgs { Item = key.Date, Type = EventCollectionChangedType.Add });
        }

        /// <summary>
        /// Gets/sets collection of values for specific date
        /// </summary>
        /// <param name="key">Event DateTime</param>
        /// <returns>Collection of events for date</returns>
        public new ICollection this[DateTime key]
        {
            get => base[key.Date];
            set
            {
                base[key.Date] = value;
                CollectionChanged?.Invoke(this, new EventCollectionChangedArgs { Item = key.Date, Type = EventCollectionChangedType.Set });
            }
        }

        internal event EventHandler<EventCollectionChangedArgs> CollectionChanged;

        internal class EventCollectionChangedArgs
        {
            public DateTime Item { get; set; }
            public EventCollectionChangedType Type { get; set; }
        }

        internal enum EventCollectionChangedType
        {
            Add,
            Set,
            Remove
        }
    }
}
