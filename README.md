# ASP.NET Core 2.0 MVC Web アプリケーション

## 機能概要

1. `.NET Core Identity` によるログイン認証  
2. `Facebook、Google、Microsoft` による外部サービス認証  
3. 住所情報の登録・更新・削除  
4. `zipaddress.net` APIを利用した住所情報の取得  


## 環境構築手順
1. `appsettings.json` の各種設定値を使用可能な値に書き換える
   - `ConnectionStrings:DefaultConnection`に接続可能なSQLServerの接続文字列を設定する  
   - `FacebookAPIOptions:AppId、FacebookAPIOptions:AppSecret`を設定する 
   [FacebookAPIの利用方法](https://docs.microsoft.com/ja-jp/aspnet/core/security/authentication/social/facebook-logins?view=aspnetcore-2.0)
   - `GoogleAPIOptions:ClientId、GoogleAPIOptions:ClientSecret`を設定する 
   [GoogleAPIの利用方法](https://docs.microsoft.com/ja-jp/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-2.0)
   - `MicrosoftAPIOptions:ClientId、MicrosoftAPIOptions:ClientSecret`を設定する 
   [MicrosoftAPIの利用方法](https://docs.microsoft.com/ja-jp/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-2.0)  

2. `.NET Entity Framework migrations`を利用してデータベースを作成する  
   NuGetパッケージマネージャのコンソール上で以下のコマンドを入力する  
   1. Migrationsファイルの作成
   ```
   Add-Migration InitialCreate
   ```   
   2. データベース作成
   ```
   Update-Database
   ```
   
