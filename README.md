# Unity Languages Sample  
  
## 概要  
 JSONファイルで管理する多言語対応のサンプルプロジェクトです。  
 
##  構成  
<pre>
Assets
├── Scripts
│     ├── JsonStream.cs     JSONファイルの読み込み処理を行う
│     └── Languages.cs      言語圏ごとのJSONファイルの取得、単語・メッセージを取得を行う
├── Scenes ── SampleScene   デモシーン
└── StreamingAssets 
      ├── location-en.json  言語（英語）
      └── location-ja.json  言語（日本語）
</pre>
  
## 環境  
- Unity 2021.3.21f1
- Visual Studio Code 1.78.2
- C#
