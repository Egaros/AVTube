# AVTube

AVTube is a simple library to search and download music and videos from various tubes like YouTube, Vimeo, Dailymotion, xHamster, Pornhub and many more.

# Usage

Sample code to search videos from YouTube and print out the video title. First parameter in YouTubeSearch.Query is the query string. Second one indicates the number of pages from YouTube who should be shown.

```cs
using AVTube;

var items = YouTubeSearch.Query("Usher", 2);

foreach (var item in items)
{
    Console.WriteLine(item.getTitle());
}
```

# Target plaforms

+ .NET Framework 4.5 and higher
+ MONO (Windows, MacOS, Linux)
+ Windows Phone 8
+ WinRT
+ Xamarin.Android
+ Xamarin.iOS
+ ASP.NET (Web)

# Dependencies

+ Newtonsoft.Json
+ taglib-sharp
+ ICSharpCode.SharpZipLib
