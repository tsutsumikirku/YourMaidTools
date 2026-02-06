# YourMaidTools
ユアメイド開発用ツール
### 導入方法
1.PackageManagerを開く

2.左上の➕ボタンを押してinstall package from git urlを押す

3.このリンクをペーストしてインストールする```https://github.com/tsutsumikirku/YourMaidTools.git```

## 前提アセット
以下のアセットが導入されている前提になります。
- [DoTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676?locale=ja-JP&srsltid=AfmBOorhZbMokJcaKvI-vD14OiwmOOFwa8oWHreoYJW6xBfzqpoeBMFM)
- [UniTask](https://github.com/Cysharp/UniTask)
- TextMeshPro


# 各クラスの使い方
## ツール関連
### YMSafeArea
Canvas内に空のオブジェクトを作成しアタッチすることによって実行時セーフエリアと同じサイズ位置になります
### YMButton
ボタンのコンポーネントです、ボタンにアニメーションをつけることが可能です
### YMAnimationPlayer
アニメーションのコンポーネントですアニメーションを再生することができます
## Animation関連
### YMScaleAnimation
スケールが変化するアニメーションを作る時に使えます。
### YMTextScaleAnimation
TextMeshProUGUIのテキストのスケールを変化するアニメーションを作る時に使えます。
### YMColorChangeAnimation
ImageかTextMeshProUGUIのスケールを変化するアニメーションを作る時に使えます
### YMPictureChangeAnimation
元の画像からクロスフェードで画像を変更することができます
