﻿using System;
using System.Collections.Generic;

public static class LevelNames
{
	private static IList<string> _levels;
	private static IEnumerator<string> _enumerator;

	static LevelNames(){
		_levels = new List<string> () {
			"level1",
			"level2",
			"line"
		};
		_enumerator = _levels.GetEnumerator ();
	}

	public static string First(){
		_enumerator = _levels.GetEnumerator();
		_enumerator.MoveNext ();
		return _enumerator.Current;
	}

	public static string Next(){
		if(_enumerator.MoveNext())
			return _enumerator.Current;

		return "finished";
	}
}