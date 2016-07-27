using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public static class CollisionObjectChecker
	{
		public static bool IsNode(Collider2D col){
			if (col.gameObject.tag.ToLower () == "node") {
				return true;
			}
			return false;
		}
	}
}

