## Calendar Plugin for Xamarin.Forms
[![Build Status](https://dev.azure.com/lilcodelab/Xamarin.Plugin.Calendar/_apis/build/status/lilcodelab.Xamarin.Plugin.Calendar?branchName=master)](https://dev.azure.com/lilcodelab/Xamarin.Plugin.Calendar/_build/latest?definitionId=20&branchName=master) 
[![Nuget](https://img.shields.io/nuget/v/Xamarin.Plugin.Calendar.svg?label=nuget)](https://www.nuget.org/packages/Xamarin.Plugin.Calendar/)
[![Issues](https://img.shields.io/github/issues/lilcodelab/Xamarin.Plugin.Calendar.svg)](https://github.com/lilcodelab/Xamarin.Plugin.Calendar/issues)
[![Chat](https://img.shields.io/badge/Telegram-chat-blue.svg)](https://t.me/XamarinPluginCalendar)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://github.com/lilcodelab/Xamarin.Plugin.Calendar/blob/master/LICENSE)

Simple cross platform plugin for Calendar control featuring:
- Displaying events by binding EventCollection
- Localization support with System.Globalization.CultureInfo
- Customizable colors, day view sizes/label styles, custom Header/Footer template support
- UI reactive to EventCollection, Culture and other changes

We are open to any suggestions and feedback, and we got our community telegram group [here](https://t.me/XamarinPluginCalendar) :)   


## Simple Implementation
| Android | iPhone |
| ------- | ------ |
| ![Android Calendar Screenshot](https://github.com/lilcodelab/Xamarin.Plugin.Calendar/blob/master/art/android-simple.png) | ![iPhone Calendar Screenshot](https://github.com/lilcodelab/Xamarin.Plugin.Calendar/blob/master/art/iphone-simple.png) |

## Advanced implementation
| Android | iPhone |
| ------- | ------ |
| ![Android Custom Calendar Screenshot](https://github.com/lilcodelab/Xamarin.Plugin.Calendar/blob/master/art/android-advanced.png) | ![iPhone Custom Calendar Screenshot](https://github.com/lilcodelab/Xamarin.Plugin.Calendar/blob/master/art/iphone-advanced.png) |

## Range implementation
| Android | iPhone |
| ------- | ------ |
| ![Android Custom Calendar Screenshot](https://github.com/lilcodelab/Xamarin.Plugin.Calendar/blob/master/art/android-range.png) | ![iPhone Custom Calendar Screenshot](https://github.com/lilcodelab/Xamarin.Plugin.Calendar/blob/master/art/iphone-range.png) |


### Setup
* Available on NuGet 
  * https://www.nuget.org/packages/Xamarin.Plugin.Calendar/

#### Supported versions
| Platform | Version |
| -------- | ------- 
| Xamarin.Forms | 4.8+ |
| Xamarin.Android | 10.0+ |
| Xamarin.iOS | 9.0+ |

### Usage
To get started just install the package via Nuget into your shared and client projects.
You can take a look on the sample app to get started or continue reading.

Reference the following xmlns to your page:
```xml
xmlns:controls="clr-namespace:Xamarin.Plugin.Calendar.Controls;assembly=Xamarin.Plugin.Calendar"
```

Basic control usage:
```xml
<controls:Calendar
        Day="14"
        Month="5"
        Year="2019"
        VerticalOptions="FillAndExpand"
        HorizontalOptions="FillAndExpand">
```

Bindable properties:
* `Culture` _CultureInfo_ calender culture/language
* `Day` _int_ currently viewing day
* `Month` _int_ currently viewing month
* `Year` _int_ currently viewing year
* `Events` _EventCollection_ (from package) your events for calender
* Custom colors, fonts, sizes ...


__Remark: You can use `ShownDate` as an alternative to `Year`, `Month` and `Day`__
```xml
<controls:Calendar
        ShownDate="2019-05-14"
        VerticalOptions="FillAndExpand"
        HorizontalOptions="FillAndExpand">
```

#### Binding events:
In your XAML, add the data template for events, and bind the events collection, example:
```xml
<controls:Calendar
    Events="{Binding Events}">
    <controls:Calendar.EventTemplate>
        <DataTemplate>
            <StackLayout
                Padding="15,0,0,0">
                <Label
                    Text="{Binding Name}"
                    FontAttributes="Bold"
                    FontSize="Medium" />
                <Label
                    Text="{Binding Description}"
                    FontSize="Small"
                    LineBreakMode="WordWrap" />
            </StackLayout>
        </DataTemplate>
    </controls:Calendar.EventTemplate>
</controls:Calendar>
```

In your ViewModel reference the following namespace:
```csharp
using Xamarin.Plugin.Calendar.Models;
```

Add property for Events:
```csharp
public EventCollection Events { get; set; }
```

Initialize Events with your data:
```csharp
Events = new EventCollection
{
    [DateTime.Now] = new List<EventModel>
    {
        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" }
    },
    // 5 days from today
    [DateTime.Now.AddDays(5)] = new List<EventModel>
    {
        new EventModel { Name = "Cool event3", Description = "This is Cool event3's description!" },
        new EventModel { Name = "Cool event4", Description = "This is Cool event4's description!" }
    },
    // 3 days ago
    [DateTime.Now.AddDays(-3)] = new List<EventModel>
    {
        new EventModel { Name = "Cool event5", Description = "This is Cool event5's description!" }
    },
    // custom date
    [new DateTime(2020, 3, 16))] = new List<EventModel>
    {
        new EventModel { Name = "Cool event6", Description = "This is Cool event6's description!" }
    }
};
```

Initialize Events with your data and a different dot color per day:
```csharp
Events = new EventCollection
{
    //2 days ago
    [DateTime.Now.AddDays(-2)] = new DayEventCollection<EventModel>( Color.Purple, Color.Purple)
    {
        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" }
    },
    // 5 days ago
    [DateTime.Now.AddDays(-5)] = new DayEventCollection<EventModel>(Color.Blue, Color.Blue)
    {
        new EventModel { Name = "Cool event3", Description = "This is Cool event3's description!" },
        new EventModel { Name = "Cool event4", Description = "This is Cool event4's description!" }
    },
};
//4 days ago
Events.Add(DateTime.Now.AddDays(-4), new DayEventCollection<EventModel>(GenerateEvents(10, "Cool")) { EventIndicatorColor = Color.Green, EventIndicatorSelectedColor = Color.Green });
```

Where `EventModel` is just an example, it can be replaced by any data model you desire.

`EventsCollection` is just a wrapper over `Dictionary<DateTime, ICollection>` exposing custom `Add` method and `this[DateTime]` indexer which internally extracts the `.Date` component of `DateTime` values and uses it as a key in this dictionary.

`DayEventCollection` is just a wrapper over `List<T>` exposing custom properties `EventIndicatorColor` and `EventIndicatorSelectedColor` for assigning a custom color to the dot.

#### Set up culture

In your ViewModel add property for Culture:
```csharp
public CultureInfo Culture => new CultureInfo("hr-HR")
```

In XAML add Culture binding
```xml
<controls:Calendar
    Culture="{Binding Culture}">
</controls:Calendar>
```

#### Available color customization
Sample properties:
```xml
MonthLabelColor="Red"
YearLabelColor="Blue"
EventIndicatorColor="Red"
EventIndicatorSelectedColor="White"
DaysTitleColor="Orange"
DeselectedDayTextColor="Blue"
OtherMonthDayColor="Gray"
SelectedDayTextColor="Cyan"
SelectedDayBackgroundColor="DarkCyan"
SelectedDateColor="Red"
SelectedTodayTextColor="Green"
TodayOutlineColor="Blue"
TodayFillColor="Silver"
TodayTextColor="Yellow"
```

#### Available customization properties

##### Calendar Layout customizations
You can set the layout of the calendar with property `CalendarLayout`

- Available layouts are: 

    `OneWeek` - only one week is shown

    `TwoWeeks` - two weeks are shown

    `Month` - whole month is shown (default value)

```xml
CalendarLayout="Month"
```

You can also choose to display the shown week number instead of month name

```xml
CalendarLayout="Week"
WeekViewUnit="WeekNumber"
```

##### Event indicator customizations
You can customize how will look event indication with property `EventIndicatorType`

- Available indicator are: 
`BottomDot` - event indicator as dot bellow of date in calendar (default value)
`TopDot` - event indicator as dot on top of date in calendar
`Background` - event indicator as colored background in calendar
`BackgroundFull` // event indicator as larger size colored background in calendar

```xml
EventIndicatorType="Background"
```
##### Calendar swipe customizations
You can write your own customizations commands for swipe. 
```xml
SwipeLeftCommand="{Binding SwipeLeftCommand}"
SwipeRightCommand="{Binding SwipeRightCommand}"
SwipeUpCommand="{Binding SwipeUpCommand}"
```

You can also disable default swipe actions.
```xml
SwipeToChangeMonthEnabled="False"
SwipeUpToHideEnabled="False"
```

##### Selection type of calender

You can either use the `Calender` class implementation for single selection mode or `RangeSelectionCalendar` for range selection mode.

```xml
    <plugin:Calendar
        SelectedDate="{Binding SelectedDate}"/>
```
On the `RangeSelectionCalendar` you can use binding for start date `SelectedStartDate` and end date `SelectedEndDate` or get list of selected dates with `SelectedDates`.
```xml
    <plugin:RangeSelectionCalendar
        x:Name="rangedCalendar"
        SelectedDates="{Binding SelectedDates}"
        SelectedEndDate="{Binding SelectedEndDate}"
        SelectedStartDate="{Binding SelectedStartDate}">
```
__Remark: Don't use both `SelectedDates` and `SelectedStartDate`/`SelectedEndDate`__

##### Other customizations
Enable/Disable animation when calendar is loaded or refreshed
Sample properties:
```xml
AnimateCalendar="False"
```

#### Section templates
There are several templates that can be used to customize the calendar. You can find an example for each one in the AdvancedPage.xaml.
You can create your own custom control file or you can also write customization directly inside of Templates.

##### Calendar control sections
These sections provide customization over appearance of the controls of the calendar, like showing the selected month and year, month selection controls etc.

###### HeaderSectionTemplate
Customize the header section (top of the calendar control). Example from AdvancedPage.xaml
```xml
<plugin:Calendar.HeaderSectionTemplate>
    <controls:CalendarHeader />
</plugin:Calendar.HeaderSectionTemplate>
```

###### FooterSectionTemplate
Customize the footer section (under the calendar part, above the events list). Example from AdvancedPage.xaml
```xml
<plugin:Calendar.FooterSectionTemplate>
    <DataTemplate>
        <controls:CalendarFooter />
    </DataTemplate>
</plugin:Calendar.FooterSectionTemplate>
```

###### BottomSectionTemplate
Customize the bottom section (at the bottom of the calendar control, below the events list). Example from AdvancedPage.xaml
```xml
<plugin:Calendar.BottomSectionTemplate>
    <controls:CalendarBottom />
</plugin:Calendar.BottomSectionTemplate>
```

##### Event templates
These templates provide customization for the events list.

###### EventTemplate
Customize the appearance of the events section. Example from AdvancedPage.xaml
```xml
<plugin:Calendar.EventTemplate>
    <DataTemplate>
        <controls:CalenderEvent CalenderEventCommand="{Binding BindingContext.EventSelectedCommand, Source={x:Reference advancedCalendarPage}}" />
    </DataTemplate>
</plugin:Calendar.EventTemplate>
```

###### EmptyTemplate
Customize what to show in case the selected date has no events. Example from AdvancedPage.xaml
```xml
<plugin:Calendar.EmptyTemplate>
    <DataTemplate>
        <StackLayout>
            <Label Text="NO EVENTS FOR THE SELECTED DATE" HorizontalTextAlignment="Center" Margin="0,5,0,5" />
        </StackLayout>
    </DataTemplate>
</plugin:Calendar.EmptyTemplate>
```
