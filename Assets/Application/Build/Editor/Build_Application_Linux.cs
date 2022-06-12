using System ;
using System.Collections.Generic ;
using System.IO ;

using UnityEngine ;

using UnityEditor ;
using UnityEditor.Build.Reporting ;

using Template ;


/// <summary>
/// アプリケーションのバッチビルド用クラス(Linux用)
/// </summary>
public partial class Build_Application
{
	//--------------------------------------------------------------------------------------------
	// 設定

	//------------------------------------
	// Windows64用
	private const string	m_Path_Linux								= "Linux/template" ;

	private static string	VersionName_Linux
	{
		get
		{
			( string versionName, _ ) = GetVersion( PlatformTypes.Linux ) ;
			return versionName ;
		}
	}

	private const string	m_ProductName_Linux_Release					= "TEMPLATE" ;
	private const string	m_ProductName_Linux_Staging					= "TEMPLATE(S)" ;
	private const string	m_ProductName_Linux_Development				= "TEMPLATE(D)" ;
	private const string	m_ProductName_Linux_Profiler				= "TEMPLATE(P)" ;

	private const string	m_Identifier_Linux_Release					= "com.TEMPLATE" ;
	private const string	m_Identifier_Linux_Staging					= "com.TEMPLATE" ;
	private const string	m_Identifier_Linux_Development				= "com.TEMPLATE" ;
	private const string	m_Identifier_Linux_Profiler					= "com.TEMPLATE" ;


	//--------------------------------------------------------------------------------------------
	// 処理

	//--------------------------------------------------------------------------------------------
	// for Mono

	/// <summary>
	/// Linux用(Release)
	/// </summary>
	/// <returns></returns>
	[MenuItem("Build/Application/Linux/Mono/Release(ServerBuild)/BuildOnly", priority = 10)]
	internal static bool Execute_Linux_Release_ServerMode_BuildOnly()
	{
		return Execute_Linux_Release( true, true, false ) ;
	}

	/// <summary>
	/// Windows64用(Release)
	/// </summary>
	/// <returns></returns>
	internal static bool Execute_Linux_Release( bool isServer, bool notDebug, bool andRun )
	{
		var settings = PlayerSettings.GetScriptingBackend( BuildTargetGroup.Standalone ) ;
		if( settings != ScriptingImplementation.Mono2x )
		{
			EditorUtility.DisplayDialog( "注意", "Mono が有効化されてません", "閉じる" ) ;
			return false ;
		}

		//-----------------------------------

		PushSetting() ;	// 設定の退避

		SetDefaultEndPoint( Settings.EndPointNames.Release ) ;

		BuildOptions	options	;

		if( isServer == false )
		{
			if( notDebug == true )
			{
				options = BuildOptions.None ;
			}
			else
			{
				options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.WaitForPlayerConnection ;
			}
		}
		else
		{
			options = BuildOptions.EnableHeadlessMode ;
		}

		if( andRun == true )
		{
			options |= BuildOptions.AutoRunPlayer ;
		}

		bool result = Execute_Linux_General
		(
			m_ProductName_Linux_Release,
			m_Identifier_Linux_Release,
			options
		) ;

		PopSetting() ;	// 設定の復帰

		return result ;
	}

	//----------------

	/// <summary>
	/// Linux用(Development)
	/// </summary>
	/// <returns></returns>
	[MenuItem("Build/Application/Linux/Mono/Development(ServerBuild)/BuildOnly", priority = 30)]
	internal static bool Execute_Linux_Development_ServerMode_BuildOnly()
	{
		return Execute_Linux_Development( true, true, false ) ;
	}

	/// <summary>
	/// Linux用(Development)
	/// </summary>
	/// <returns></returns>
	private static bool Execute_Linux_Development( bool isServer, bool notDebug, bool andRun )
	{
		var settings = PlayerSettings.GetScriptingBackend( BuildTargetGroup.Standalone ) ;
		if( settings != ScriptingImplementation.Mono2x )
		{
			EditorUtility.DisplayDialog( "注意", "Mono が有効化されてません", "閉じる" ) ;
			return false ;
		}

		//-----------------------------------

		PushSetting() ;	// 設定の退避

		SetDefaultEndPoint( Settings.EndPointNames.Development ) ;

		BuildOptions	options	;

		if( isServer == false )
		{
			if( notDebug == true )
			{
				options = BuildOptions.None ;
			}
			else
			{
				options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.WaitForPlayerConnection ;
			}
		}
		else
		{
			options = BuildOptions.EnableHeadlessMode ;
		}

		if( andRun == true )
		{
			options |= BuildOptions.AutoRunPlayer ;
		}

		bool result = Execute_Linux_General
		(
			m_ProductName_Linux_Development,
			m_Identifier_Linux_Development,
			options
		) ;

		PopSetting() ;	// 設定の復帰

		return result ;
	}

	//--------------------------------------------------------------------------------------------
	// for IL2CPP

	/// <summary>
	/// Linux用(Release)
	/// </summary>
	/// <returns></returns>
	[MenuItem("Build/Application/Linux/IL2CPP/Release(ServerBuild)/BuildOnly", priority = 10)]
	internal static bool Execute_Linux_IL2CPP_Release_ServerMode_BuildOnly()
	{
		return Execute_Linux_IL2CPP_Release( true, true, false ) ;
	}

	/// <summary>
	/// Linux用(Release)
	/// </summary>
	/// <returns></returns>
	internal static bool Execute_Linux_IL2CPP_Release( bool isServer, bool notDebug, bool andRun )
	{
		var settings = PlayerSettings.GetScriptingBackend( BuildTargetGroup.Standalone ) ;
		if( settings != ScriptingImplementation.IL2CPP )
		{
			EditorUtility.DisplayDialog( "注意", "IL2CPP が有効化されてません", "閉じる" ) ;
			return false ;
		}

		//-----------------------------------

		PushSetting() ;	// 設定の退避

		SetDefaultEndPoint( Settings.EndPointNames.Release ) ;

		BuildOptions	options ;	// ひとまずビルド速度優先

		if( isServer == false )
		{
			if( notDebug == true )
			{
				options = BuildOptions.None ;
			}
			else
			{
				options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.WaitForPlayerConnection ;
			}
		}
		else
		{
			options = BuildOptions.EnableHeadlessMode ;
		}

		if( andRun == true )
		{
			options |= BuildOptions.AutoRunPlayer ;
		}

		bool result = Execute_Linux_General
		(
			m_ProductName_Linux_Release,
			m_Identifier_Linux_Release,
			options
		) ;

		PopSetting() ;	// 設定の復帰

		return result ;
	}

	//----------------

	/// <summary>
	/// Linux用(Development)
	/// </summary>
	/// <returns></returns>
	[MenuItem("Build/Application/Linux/IL2CPP/Development(ServerBuild)/BuildOnly", priority = 30)]
	internal static bool Execute_Linux_IL2CPP_Development_ServerMode_BuildOnly()
	{
		return Execute_Linux_IL2CPP_Development( true, true, false ) ;
	}

	// Linux - Development
	private static bool Execute_Linux_IL2CPP_Development( bool isServer, bool notDebug, bool andRun )
	{
		var settings = PlayerSettings.GetScriptingBackend( BuildTargetGroup.Standalone ) ;
		if( settings != ScriptingImplementation.IL2CPP )
		{
			EditorUtility.DisplayDialog( "注意", "IL2CPP が有効化されてません", "閉じる" ) ;
			return false ;
		}

		//-----------------------------------

		PushSetting() ;	// 設定の退避

		SetDefaultEndPoint( Settings.EndPointNames.Development ) ;

		BuildOptions	options	;

		if( isServer == false )
		{
			if( notDebug == true )
			{
				options = BuildOptions.None ;
			}
			else
			{
				options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.WaitForPlayerConnection ;
			}
		}
		else
		{
			options = BuildOptions.EnableHeadlessMode ;
		}

		if( andRun == true )
		{
			options |= BuildOptions.AutoRunPlayer ;
		}

		bool result = Execute_Linux_General
		(
			m_ProductName_Linux_Development,
			m_Identifier_Linux_Development,
			options
		) ;

		PopSetting() ;	// 設定の復帰

		//-----------------------------------

		return result ;
	}

	//--------------------------------------------------------------------------------------------

	/// <summary>
	/// Linux用でMonoを有効化する
	/// </summary>
	/// <returns></returns>
	[MenuItem("Build/Application/Linux/Switch Mono", priority = 80)]
	internal static bool Execute_Linux_Switch_Mono()
	{
		// Mono を有効化する
		PlayerSettings.SetScriptingBackend( BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x ) ;

		AssetDatabase.SaveAssets() ;

		EditorUtility.DisplayDialog( "ビルドモード変更", "Linux を Mono でビルドします", "閉じる" ) ;

		return true ;
	}

	/// Linux用でIL2CPPを有効化する
	/// </summary>
	/// <returns></returns>
	[MenuItem("Build/Application/Linux/Switch IL2CPP", priority = 80)]
	internal static bool Execute_Linux_Switch_IL2CPP()
	{
		// IL2CPP を有効化する
		PlayerSettings.SetScriptingBackend( BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP ) ;

		AssetDatabase.SaveAssets() ;

		EditorUtility.DisplayDialog( "ビルドモード変更", "Linux を IL2CPP でビルドします", "閉じる" ) ;

		return true ;
	}

	//------------------------------------------------------------

	// Linux 共通
	private static bool Execute_Linux_General( string productName, string identifier, BuildOptions options )
	{
		BuildTarget		target		= BuildTarget.StandaloneLinux64 ;
		string			path		= m_Path_Linux ;

		// 処理時間計測開始
		StartClock() ;

		// ビルドターゲットを変更する
		BuildTarget activeBuildTarget = ChangeBuildTarget( target, "Change" ) ;

		//-----------------------------------------------------------

		// 固有の設定
		PlayerSettings.productName					= productName ;
		PlayerSettings.SetApplicationIdentifier( BuildTargetGroup.Standalone, identifier ) ;

		PlayerSettings.bundleVersion				= VersionName_Linux ;

		//-----------------------------------------------------------

		// ビルドを実行する
		bool result = Process( path, target, options ) ;

		// ビルドターゲットを復帰する
		ChangeBuildTarget( activeBuildTarget, "Revert" ) ;

		// 処理時間計測終了
		StopClock() ;

		Debug.Log( "-------> Target : " + target + " " + productName ) ;

		return result ;
	}
}
