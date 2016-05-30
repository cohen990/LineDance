using System;

namespace AssemblyCSharp
{
	public interface IEndOfLevel
	{
		void AlertOfEnd();

		bool IsEndOfLevel();

		Action GetAction();
	}
}

