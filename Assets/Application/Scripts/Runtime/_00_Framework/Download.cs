using System ;
using System.Text ;
using System.Collections.Generic ;

using Cysharp.Threading.Tasks ;

using UnityEngine ;

namespace Template
{
	/// <summary>
	/// ダウンロードのファサード(窓口)クラス Version 2020/12/04
	/// </summary>
	public class Download
	{
		//-----------------------------------------------------------

		/// <summary>
		/// ファイルをダウンロードしバイト配列として取得する
		/// </summary>
		/// <param name="url"></param>
		/// <param name="onReceived"></param>
		/// <param name="onProgress"></param>
		/// <param name="useProgress"></param>
		/// <param name="useDialog"></param>
		/// <returns></returns>
		public static async UniTask<byte[]> ToBytes( string url, Action<byte[]> onReceived = null, Action<int, int> onProgress = null, bool useProgress = true, bool useDialog = true, string title = null, string message = null )
		{
			// 正常系の対応のみ考えれば良い(エラーはWebAPIManager内で処理される)
			byte[] responseData = await DownloadManager.SendRequest
			(
				url,			// Location
				onProgress,
				useProgress,
				useDialog,
				title,
				message
			) ;

			if( responseData == null )
			{
				return null ;	// エラー
			}

			onReceived?.Invoke( responseData ) ;
			return responseData ;
		}

		/// <summary>
		/// ファイルをダウンロードしテキストとして取得する
		/// </summary>
		/// <param name="url"></param>
		/// <param name="onReceived"></param>
		/// <param name="onProgress"></param>
		/// <param name="useProgress"></param>
		/// <param name="useDialog"></param>
		/// <returns></returns>
		public static async UniTask<string> ToText( string url, Action<string> onReceived, Action<int, int> onProgress = null, bool useProgress = true, bool useDialog = true, string title = null, string message = null )
		{
			byte[] responseData = await ToBytes( url, null, onProgress, useProgress, useDialog, title, message ) ;
			if( responseData == null || responseData.Length == 0 )
			{
				// 失敗
				return null ;
			}

			string text = UTF8Encoding.UTF8.GetString( responseData ) ;

			onReceived?.Invoke( text ) ;
			return text ;
		}

		/// <summary>
		/// ファイルをダウンロードしテクスチャとして取得する
		/// </summary>
		/// <param name="url"></param>
		/// <param name="onReceived"></param>
		/// <param name="onProgress"></param>
		/// <param name="useProgress"></param>
		/// <param name="useDialog"></param>
		/// <returns></returns>
		public static async UniTask<Texture2D> ToTexture( string url, Action<Texture> onReceived, Action<int, int> onProgress = null, bool useProgress = true, bool useDialog = true, string title = null, string message = null )
		{
			byte[] responseData = await ToBytes( url, null, onProgress, useProgress, useDialog, title, message ) ;
			if( responseData == null || responseData.Length == 0 )
			{
				// 失敗
				return null ;
			}

			Texture2D texture = new Texture2D( 4, 4, TextureFormat.ARGB32, false, true ) ;
			texture.LoadImage( responseData ) ;

			onReceived?.Invoke( texture ) ;
			return texture ;
		}

		/// <summary>
		/// ファイルをダウンロードしアセットバンドルとして取得する(取得後に必ずUnloadする事)
		/// </summary>
		/// <param name="url"></param>
		/// <param name="onReceived"></param>
		/// <param name="onProgress"></param>
		/// <param name="useProgress"></param>
		/// <param name="useDialog"></param>
		/// <returns></returns>
		public static async UniTask<AssetBundle> ToAssetBundle( string url, Action<AssetBundle> onReceived, Action<int, int> onProgress = null, bool useProgress = true, bool useDialog = true, string title = null, string message = null )
		{
			byte[] responseData = await ToBytes( url, null, onProgress, useProgress, useDialog, title, message ) ;
			if( responseData == null || responseData.Length == 0 )
			{
				// 失敗
				return null ;
			}

			AssetBundle assetBundle = AssetBundle.LoadFromMemory( responseData ) ;

			onReceived?.Invoke( assetBundle ) ;
			return assetBundle ;
		}
	}
}

