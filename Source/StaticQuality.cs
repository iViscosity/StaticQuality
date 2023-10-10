/**
 *  Static Quality Plus (Stripped) - Made compatible with Harmony
 *  Copyright (C) 2022  Vis

 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.

 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.

 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Verse;
using UnityEngine;
using HarmonyLib;

namespace StaticQualityPlus
{
	public class StaticQuality : Mod
	{
		public static Settings Settings;

		public StaticQuality(ModContentPack content) : base(content)
		{
			new Harmony("vis.rimworld.Static_Quality.main").PatchAll();
			Settings = GetSettings<Settings>();
		}

		public void Save()
		{
			LoadedModManager.GetMod<StaticQuality>().GetSettings<Settings>().Write();
		}

		public override string SettingsCategory() => "Static Quality";

		public override void DoSettingsWindowContents(Rect canvas)
		{
			Settings.DoWindowContents(canvas);
		}
	}
}
