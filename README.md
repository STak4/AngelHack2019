# AngelHack2019

# リポジトリ概要
Slingshot-AR-Unity -> Unityプロジェクト
それ以外のフォルダ（サーバープログラムやビルドファイル等）はrootに各々追加してください。


## ブランチ構成
masterをひたすら更新していく・・・か、

develop/機能名
で機能毎にブランチを切って、マスターはリリース時等のみにマージするか

## Unityプロジェクト概要

### 構成

- Unity 2019.1.6f1
- ARFoundation 2.1.0 (Preview)
- ARKit XR Plugin 2.1.0 (Preview)
- iOS 12
- XCode 10.2

NDAの関係でBeta版は使わない方針（つまりARKit3の機能は使わない）で行きます。

### Assets内
既存のフォルダ構成に従ってください、サードパーティ製のアセット等追加した場合はThihrdpartyフォルダに全て移した上で、READMEを更新してください。

インポートしたパッケージはGoogleDrive等でお願いします。

本当は機能ごとにScripts等フォルダ分けしたいのですが、最初に全て羅列するのは難しいので個人名のフォルダをScripts等内部に作り、そこに追加していく感じでしばらくは行きます。

## サードパーティ（Unityアセット）
Git LFSを使わないことやライセンスの関係で全てコミット対象から外します。各自ローカルで

[Assets]- [Thirdparty]-[対象アセット]

のように追加してください。


### 現在使用しているアセット
- DarkRift 2
- BLE
- ARFoundation Samples : https://github.com/Unity-Technologies/arfoundation-samples
