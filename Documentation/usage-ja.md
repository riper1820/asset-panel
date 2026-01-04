# Asset Panel

VRChatワールドで利用中のアセットを一覧表示するためのパネルです。

同梱されているエディタ拡張により、アセット情報を簡単に入力することができます。

以下の情報源からアセット情報を自動入力することができます。

- BOOTH
- プロジェクトでインストール済みのUPM/VPMパッケージ

手動入力で追加・修正することもできます。

## インストール

### VCC・ALCOMでインストール（推奨）

[VCC](https://vcc.docs.vrchat.com)か[ALCOM](https://vrc-get.anatawa12.com/ja/alcom/)を事前にインストールしてください。

下の画像 をクリックすると、リポジトリをVCC・ALCOMに追加できます。

<a href="vcc://vpm/addRepo?url=https://raw.githubusercontent.com/riper1820/asset-panel/refs/heads/main/vpm.json">
  <img src="https://img.shields.io/badge/Add%20to-VCC-blue?style=social" alt="Add to VCC">
</a>

リンクが機能しない場合は、`https://raw.githubusercontent.com/riper1820/asset-panel/refs/heads/main/vpm.json` を手動で追加してください。

その後、VPM・ALCOMのパッケージ管理画面で `Asset Panel` を追加してください。 

### UnityPackageでインストール

[GitHubのReleasesページ](https://github.com/riper1820/asset-panel/releases)から、拡張子が `.unitypackage` になっているファイルをダウンロードしてください。

ダウンロードしたUnityPackageをUnityエディタにインポートしてください。

## 使い方

### Prefabをシーンに配置する

`Packages/Asset Panel/Runtime/Prefabs/AssetPanel` をシーンに追加してください。

### アセット情報を入力する

AssetPanelオブジェクトのインスペクタを開き、`Assets`にアセットの情報を入力してください。

#### インストール済みのUPM/VPMパッケージから自動入力

1. `Add from packages (UPM, VPM)` をクリックしてください。
2. インストール済みのUPM/VPMパッケージの一覧が表示されます。追加したいパッケージを選び、`Select`ボタンを押してください。

#### BOOTHのURLを参照して自動入力

1. `Add from BOOTH`をクリックしてください。
2. 追加したいアセットのURLを`BOOTH URL`に入力して、`Fetch`をクリックしてください。
3. BOOTHから読み込んだアセット情報が表示されます。`Add to List`を押すと、インスペクタの`Assets`にアセット情報が追加されます。

### アセットパネルを更新する

`Update Asset Panel` をクリックするとアセットパネルが更新されます。
