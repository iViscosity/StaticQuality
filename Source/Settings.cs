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

using UnityEngine;
using Verse;

namespace StaticQualityPlus
{
	public class Settings : ModSettings
	{
		public static int QualitySwitch = 3;

		public static int PoorThreshold = 3;
		public static int NormalThreshold = 7;
		public static int GoodThreshold = 11;
		public static int ExcellentThreshold = 15;
		public static int MasterworkThreshold = 18;
		public static int LegendaryThreshold = 20;

		public static bool CraftNotify = false;
		public static bool LegendaryRequiresInspiration = true;

		public void DoWindowContents(Rect canvas)
		{
			Listing_Standard options = new Listing_Standard()
			{
				ColumnWidth = (canvas.width - 17) / 2
			};
			options.Begin(canvas);

			Text.Font = GameFont.Medium;
			options.Label("STAQ.ItemQualityOptions".Translate());
			Text.Font = GameFont.Small;

			bool[] qs = new bool[5];

			qs[0] = options.RadioButton("STAQ.VanillaQuality".Translate(), (QualitySwitch == 0), tooltip: "STAQ.TheRimWorldDefault".Translate());
			qs[1] = options.RadioButton("STAQ.StaticQuality".Translate(), (QualitySwitch == 1), tooltip: "STAQ.ItemQualityIs".Translate());
			qs[2] = options.RadioButton("STAQ.StaticQuality1".Translate(), (QualitySwitch == 2), tooltip: "STAQ.ItemQualityIs1".Translate());
			qs[3] = options.RadioButton("STAQ.StaticQuality2".Translate(), (QualitySwitch == 3), tooltip: "STAQ.ItemQualityIs2".Translate());
			qs[4] = options.RadioButton("STAQ.CheatQuality".Translate(), (QualitySwitch == 4), tooltip: "STAQ.CheatQualityIs".Translate());

			options.GapLine();
			Text.Font = GameFont.Medium;
			options.Label("STAQ.MiscellaneousSettingsLabel".Translate()); // Header
			Text.Font = GameFont.Small;
			string text_cn = "STAQ.CraftingNotification".Translate();
			options.CheckboxLabeled(text_cn, ref CraftNotify, "STAQ.CraftingEnabled".Translate());

			string text_li = "STAQ.LegendaryOnlyWithInspiration".Translate();
			options.CheckboxLabeled(text_li, ref LegendaryRequiresInspiration, "STAQ.LegendaryWithInspirationEnabled".Translate());

			options.GapLine();
			if (options.ButtonText("STAQ.Reset".Translate()))
			{
				QualitySwitch = 3;

				PoorThreshold = 3;
				NormalThreshold = 7;
				GoodThreshold = 11;
				ExcellentThreshold = 15;
				MasterworkThreshold = 18;
				LegendaryThreshold = 20;

				CraftNotify = false;
				LegendaryRequiresInspiration = true;
			}

			options.NewColumn();
			Text.Font = GameFont.Medium;
			options.Label("STAQ.ThresholdSettings".Translate()); // Header
			Text.Font = GameFont.Small;

			options.Label("STAQ.RequiredStatPoor".Translate(PoorThreshold.ToString()));
			PoorThreshold = (int)options.Slider(PoorThreshold, 0, NormalThreshold - 1);
			options.Label("STAQ.RequiredStatNormal".Translate(NormalThreshold.ToString()));
			NormalThreshold = (int)options.Slider(NormalThreshold, PoorThreshold + 1, GoodThreshold - 1);
			options.Label("STAQ.RequiredStatGood".Translate(GoodThreshold.ToString()));
			GoodThreshold = (int)options.Slider(GoodThreshold, NormalThreshold + 1, ExcellentThreshold - 1);
			options.Label("STAQ.RequiredStatExcellent".Translate(ExcellentThreshold.ToString()));
			ExcellentThreshold = (int)options.Slider(ExcellentThreshold, GoodThreshold + 1, MasterworkThreshold - 1);
			options.Label("STAQ.RequiredStatMasterwork".Translate(MasterworkThreshold.ToString()));
			MasterworkThreshold = (int)options.Slider(MasterworkThreshold, ExcellentThreshold + 1, LegendaryThreshold - 1);
			options.Label("STAQ.RequiredStatLegendary".Translate(LegendaryThreshold.ToString()));
			LegendaryThreshold = (int)options.Slider(LegendaryThreshold, MasterworkThreshold + 1, 20);

			for (int i = 0; i < (qs.Length); ++i)
			{
				if (qs[i])
				{
					QualitySwitch = i;
					break;
				}
			}

			options.End();
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref QualitySwitch, "quality_switch", 1, false);

			Scribe_Values.Look(ref PoorThreshold, "poor_threshold", 3, false);
			Scribe_Values.Look(ref NormalThreshold, "normal_threshold", 7, false);
			Scribe_Values.Look(ref GoodThreshold, "good_threshold", 11, false);
			Scribe_Values.Look(ref ExcellentThreshold, "excellent_threshold", 15, false);
			Scribe_Values.Look(ref MasterworkThreshold, "masterwork_threshold", 18, false);
			Scribe_Values.Look(ref LegendaryThreshold, "legendary_threshold", 20, false);

			Scribe_Values.Look(ref CraftNotify, "craft_notify", true, false);

			Scribe_Values.Look(ref LegendaryRequiresInspiration, "legendary_inspiration", false, false);
		}
	}
}
