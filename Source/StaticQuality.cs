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
			Settings = GetSettings<Settings>();
#if DEBUG
			Harmony.DEBUG = true;
#endif
			new Harmony("vis.rimworld.Static_Quality.main").PatchAll(); 
		}

		public override string SettingsCategory() => "Static Quality";

		public override void DoSettingsWindowContents(Rect canvas)
		{
			base.DoSettingsWindowContents(canvas);
			Settings.DoWindowContents(canvas);
		}
	}
}
