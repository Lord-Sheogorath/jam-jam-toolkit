using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace LordSheo.JJTK
{
	public interface IJuiceAction
	{
		UniTask Execute();
	}
}