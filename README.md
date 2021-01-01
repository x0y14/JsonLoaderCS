# JsonLoaderCS  
C#製のJson to Dictionary<string, dynamic>の変換器です。   

{ "items": [ { "title": "math book", "target_age": [ 12, 13, 14, 15 ] }, {...} ] }  

dict.Get("items.0/title") => "math book"　みたいなこともできる。
