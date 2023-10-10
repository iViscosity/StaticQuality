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

using RimWorld;
using System;
using Verse;
using HarmonyLib;

namespace StaticQualityPlus
{
	[HarmonyPatch(typeof(QualityUtility), nameof(QualityUtility.GenerateQualityCreatedByPawn), new Type[] { typeof(int), typeof(bool) })]
	[HarmonyPriority(Priority.Last)]
	class Patch_GenerateQualityCreatedByPawn
	{
		public static QualityCategory Postfix(QualityCategory __result, int relevantSkillLevel, bool inspired)
		{
			if (Settings.QualitySwitch == 0) // Do nothing
				return __result;

			if (Settings.QualitySwitch == 4) // Cheat quality
				if (Settings.LegendaryRequiresInspiration && !inspired) // Still respects Inspiration requirement.
					return (QualityCategory)5;
				else
					return (QualityCategory)6;

			int quality = 0; // Default to "awful"
			int mod = 0;

			if (relevantSkillLevel >= Settings.LegendaryThreshold)
			{
				if (Settings.LegendaryRequiresInspiration && !inspired)
					quality = 5;
				else
					quality = 6;
			}
			else if (relevantSkillLevel >= Settings.MasterworkThreshold)
				quality = 5;
			else if (relevantSkillLevel >= Settings.ExcellentThreshold)
				quality = 4;
			else if (relevantSkillLevel >= Settings.GoodThreshold)
				quality = 3;
			else if (relevantSkillLevel >= Settings.NormalThreshold)
				quality = 2;
			else if (relevantSkillLevel >= Settings.PoorThreshold)
				quality = 1;
			
			if (Settings.QualitySwitch > 1)
			{
				Random rng = new Random();

				if (Settings.QualitySwitch == 3)
				{
					mod = rng.Next(-2, 3);
				}
				else
				{
					mod = rng.Next(-1, 2);
				}
			}

			quality += mod;

			if (quality < 0)
				quality = 0;
			else if (quality >= 6)
			{
				if (Settings.LegendaryRequiresInspiration && !inspired)
					quality = 5;
				else
					quality = 6;
			}


			__result = (QualityCategory)quality;

			return __result;
		}
	}

	[HarmonyPatch(typeof(QualityUtility), nameof(QualityUtility.SendCraftNotification))]
	class Path_SendCraftNotification
	{
		public static bool Prefix() => Settings.CraftNotify;
	}
}