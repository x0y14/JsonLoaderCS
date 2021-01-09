# JsonLoaderCS  
C#製のJson to Dictionary<string, dynamic>の変換器です。  
firebaseみたいにパスに似た表記でデータを取得することもできる。  
this program is buggy!

```
using JsonLoader;
...

// set json which type is string.
string json_str = "{ \"title\": \"test\", \"items\": [ 9999, \"hello\", { \"data\": \"hoge\" } ] }";

var jdata = new JsonLoader.Loader(json_str);
var map = jdata.Load();// -> we can use jdata.

// we have two way that use it
// 1:
Console.WriteLine(map["title"]);// -> "test"
// 2:
Console.WriteLine(jdata.Get("title"));// -> "test"


// use second way, we can get data. like this
Console.WriteLine(jdata.Get("items.0"));// -> 9999
Console.WriteLine(jdata.Get("items.1"));// -> "hello"
Console.WriteLine(jdata.Get("items.2/data"));// -> "hoge"

```
