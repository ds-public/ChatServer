using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Threading ;

using WebSocketSharp.Server ;

using Cysharp.Threading.Tasks ;

namespace Template.Screens
{
	/// <summary>
	/// チャットの制御処理
	/// </summary>
	public partial class Server : ExMonoBehaviour
	{
		//-------------------------------------------------------------------------------------------

		// WebSocketServer のインスタンス
		private WebSocketServer m_WebSocketServer ;

		/// <summary>
		/// クライアントの管理クラス(定義のみ)
		/// </summary>
		public class Client : ExWebSocketBehavior<Client>
		{
			protected override void OnConnected()
			{
//				Console.WriteLine( "接続ありです:" + this.ID ) ;
			}

			protected override void OnReceived( string text )
			{
//				Console.WriteLine( "受信ありです:" + text ) ;
			}

			protected override void OnDisconnected()
			{
//				Console.WriteLine( "切断ありです:" + this.ID ) ;
			}
		}

		// クライアントの制御用インスタンス群を保持する
		private  List<Client> m_Clients = new List<Client>() ;

		//-------------------------------------------------------------------------------------------

		internal void Start()
		{
			Process().Forget() ;
		}


		/// <summary>
		/// アクション選択
		/// </summary>
		/// <returns></returns>
		private async UniTask Process()
		{
			// WebSocket 準備
			int serverPortNumber = 32000 ;
			var settings = ApplicationManager.LoadSettings() ;
			if( settings != null )
			{
				serverPortNumber = settings.ServerPortNumber ;
			}

			m_WebSocketServer = new WebSocketServer( serverPortNumber ) ;

			//----------------------------------------------------------
			// WebSocketServer 準備

			// メインスレッドのコンテキストを取得する
			var context = SynchronizationContext.Current ;

			m_WebSocketServer.AddWebSocketService<Client>( "/", ( Client client ) =>
			{
				if( client != null )
				{
//					Console.Log( "[重要]クライアント接続通知はきているのか:" + client.ID ) ;

					// 生成されたクライアント制御クラスのインスタンスにサーバーのインスタンスを渡す
					client.SetServer( context, OnOpen, OnData, OnText, OnClose ) ;

					// クライアントの管理リストにインスタンスを保存する
					m_Clients.Add( client ) ;
				}
			} ) ;

			// サーバー開始
			m_WebSocketServer.Start() ;

			Console.Log( "[ChatServer] スタート(ポート番号:" + serverPortNumber + ")" ) ;

			//----------------------------------------------------------
			// サーバーでのイベントコールバック

			void OnOpen( Client client )
			{
				// 接続
				Console.Log( "[ChatServer] クライアントの接続 : " + client.ID ) ;
			}

			void OnData( Client client, byte[] data )
			{
			}

			void OnText( Client client, string text )
			{
				// 発言

				var chat = JsonUtility.FromJson<NetworkData.ChatData>( text ) ;
				if( chat != null )
				{
					// ログ追加
					Console.Log( "[ChatServer] クライアントの発言 : " + chat.Speaker + ": " + chat.Message ) ;
				}
				
				// 全てのクライアントに送信する
				client.Broadcast( text ) ;
			}

			void OnClose( Client client )
			{
				// 切断
				Console.Log( "[ChatServer] クライアントの切断 : " + client.ID ) ;
			}

			await Yield() ;
		}

		/// <summary>
		/// MonoBehaviour が破棄される際に呼び出される
		/// </summary>
		internal void OnDestroy()
		{
			// WebSocketServer を停止させる
			if( m_WebSocketServer != null )
			{
				m_WebSocketServer.Stop() ;
				m_WebSocketServer  = null ;
			}
		}
		//-------------------------------------------------------------------------------------------




	}
}
