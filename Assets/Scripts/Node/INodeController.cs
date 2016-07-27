using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public interface INodeController
	{
		Vector3 NodePosition { get; }
		bool IsEnabled {get;}
		bool IsBeingHoveredOver {get;set;}

		void Connect();

		void Disconnect();
	}
}

