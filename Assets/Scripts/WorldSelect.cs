using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class WorldSelect : MonoBehaviour
	{
		public World world;
		private Dictionary<World, string> WorldSceneNames;

		public WorldSelect(){
			WorldSceneNames = new Dictionary<World, string> () {
				{ World.Red, "LevelSelectRed" },
				{ World.Blue, "LevelSelectBlue" },
				{ World.Green, "LevelSelectGreen" },
				{ World.Black, "LevelSelectBlack" },
				{ World.White, "LevelSelectWhite" },
			};
		}

		public void SelectWorld(){
			SceneManager.LoadScene(WorldSceneNames[world]);
		}
	}
}

