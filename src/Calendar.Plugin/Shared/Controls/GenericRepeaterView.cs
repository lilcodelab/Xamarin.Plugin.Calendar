﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Controls
{
    internal class GenericRepeaterView : StackLayout
    {
        #region Bindable properties

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(ICollection), typeof(GenericRepeaterView), new List<object>(), BindingMode.TwoWay, propertyChanged: OnItemsSourceChanged);

        public ICollection ItemsSource
        {
            get => (ICollection)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(GenericRepeaterView), null, propertyChanged: OnItemTemplateChanged);

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly BindableProperty EmptyTemplateProperty =
            BindableProperty.Create(nameof(EmptyTemplate), typeof(DataTemplate), typeof(GenericRepeaterView), null, propertyChanged: OnItemTemplateChanged);

        public DataTemplate EmptyTemplate
        {
            get => (DataTemplate)GetValue(EmptyTemplateProperty);
            set => SetValue(EmptyTemplateProperty, value);
        }

        #endregion

        internal GenericRepeaterView()
        {
            Spacing = 0;
            VerticalOptions = LayoutOptions.StartAndExpand;
        }

        #region PropertyChanged

        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is GenericRepeaterView view)
                view.ResetItems();
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is GenericRepeaterView view)
            {
                if (oldValue is INotifyCollectionChanged oldObservable)
                    oldObservable.CollectionChanged -= view.CollectionChanged;

                view.ResetItems();

                if (newValue is INotifyCollectionChanged newObservable)
                    newObservable.CollectionChanged += view.CollectionChanged;
            }
        }

        #endregion

        private void ResetItems()
        {
            if (ItemTemplate == null)
                return;

            Children.Clear();

            if (ItemsSource != null || ItemsSource?.Count > 0)
            {
                foreach (var itemModel in ItemsSource)
                    Children.Add(GetItemView(itemModel));
            }
            else if (EmptyTemplate != null)
            {
                Children.Add(GetEmptyView());
            }
        }

        private View GetItemView(object itemModel)
        {
            var itemContent = ItemTemplate.CreateContent(itemModel);

            var view = itemContent as View ?? (itemContent as ViewCell)?.View;
            return view;
        }

        private View GetEmptyView()
        {
            var emptyContent = EmptyTemplate.CreateContent();

            var view = emptyContent as View ?? (emptyContent as ViewCell)?.View;
            return view;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var itemViews = Children.ToDictionary(x => x.BindingContext, x => x);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var index = e.NewStartingIndex;

                    foreach (var newItem in e.NewItems)
                        Children.Insert(index++, GetItemView(newItem));

                    break;

                case NotifyCollectionChangedAction.Move:
                    Children.RemoveAt(e.OldStartingIndex);
                    Children.Insert(e.NewStartingIndex, GetItemView(itemViews[e.OldItems[0]]));
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                        Children.Remove(itemViews[item]);

                    break;

                case NotifyCollectionChangedAction.Replace:
                    Children.RemoveAt(e.OldStartingIndex);
                    Children.Insert(e.NewStartingIndex, GetItemView(itemViews[e.NewItems[0]]));
                    break;

                case NotifyCollectionChangedAction.Reset:
                    ResetItems();
                    break;
            }
        }
    }
}
