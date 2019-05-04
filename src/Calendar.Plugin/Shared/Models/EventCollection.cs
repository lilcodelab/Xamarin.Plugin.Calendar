﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xamarin.Plugin.Calendar.Models
{
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

        public new void Add(DateTime key, ICollection value)
        {
            base.Add(key.Date, value);
        }

        public new ICollection this[DateTime key]
        {
            get => base[key.Date];
            set => Add(key, value);
        }
    }
}
