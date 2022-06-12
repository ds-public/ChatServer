using System ;
using System.IO ;
using System.Text ;

using System.Collections ;
using System.Collections.Generic ;

using UnityEngine ;

namespace Template
{
	/// <summary>
	/// コンソール出力クラス
	/// </summary>
	public class Console
	{
		public static void Log( string s )
		{
#if UNITY_EDITOR
			Debug.Log( s ) ;

#else

#if UNITY_STANDALONE
			System.Console.WriteLine( s ) ;
#endif

#endif
		}
	}
}
