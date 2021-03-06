﻿using UnityEngine;
using System;

namespace TCUtils{


	/// <summary>
	/// Enum flag attribute.
	/// Copied from Online Open source: http://wiki.unity3d.com/index.php/EnumFlagPropertyDrawer
	/// </summary>
	public class EnumFlagAttribute : PropertyAttribute
	{
		public string enumName;

		public EnumFlagAttribute() {}

		public EnumFlagAttribute(string name)
		{
			enumName = name;
		}
	}
	
	
	 public class TagSelectorAttribute : PropertyAttribute
	 {
		 public bool UseDefaultTagFieldDrawer = false;
	 }
}