using System;
using System.Collections;
using System.Collections.Generic;

namespace Xamarin.Plugin.Calendar.Models
{
    /// <summary> 
    /// Calendar events collection, wraps <see cref="Dictionary{DateTime, ICollection}" />
    /// </summary>
    public class EventCollection : Dictionary<DateTime, ICollection>
    {
        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCollection"/> class
        /// that is empty, has the default initial capacity, and uses the default equality
        /// comparer for the key type.
        /// </summary>
        public EventCollection() : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCollection"/> class
        /// that is empty, has the specified initial capacity, and uses the default equality
        /// comparer for the key type.
        /// </summary>
        /// <param name="capacity">
        /// The initial number of elements that the <see cref="EventCollection"/> can contain.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public EventCollection(int capacity) : base(capacity)
        { }

        #endregion

        /// <summary>
        /// Removes a collection of values for specific date
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
