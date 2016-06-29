using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public interface INodeController
	{
		Vector3 NodePosition { get; }
		bool IsEnabled {get;}

		void Connect();

		void Disconnect();
	}
}

