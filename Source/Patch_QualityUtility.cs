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
	class Patch_GenerateQualityCreatedByPawn
	{
		public static QualityCategory Postfix(QualityCategory __result, int relevantSkillLevel, bool inspired)
		{
			if (StaticQuality.Settings.QualitySwitch == 4)
				if (StaticQuality.Settings.LegendaryRequiresInspiration)
					return (QualityCategory)5;
				else
					return (QualityCategory)6;

			if (StaticQuality.Settings.QualitySwitch > 1)
			{
				Random rng = new Random();
				int mod;

				if (StaticQuality.Settings.QualitySwitch == 3)
				{
					mod = rng.Next(-2, 3);
				}
				else
				{
					mod = rng.Next(-1, 2);
				}

				int quality;

				switch (relevantSkillLevel)
				{
					case 0:
					case 1:
					case 2:
					case 3:
						quality = 0 + mod;
						break;
					case 4:
					case 5:
					case 6:
					case 7:
						quality = 1 + mod;
						break;
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 14:
						quality = 3 + mod;
						break;
					case 15:
					case 16:
					case 17:
						quality = 4 + mod;
						break;
					case 18:
					case 19:
						quality = 5 + mod;
						break;
					case 20:
						quality = 6 + mod;
						break;
					default:
						quality = 3;
						break;
				}

				if (inspired)
					quality += 2;

				if (quality < 0)
					quality = 0;
				else if (quality > 6)
					quality = 6;

				if (quality == 6)
					if (StaticQuality.Settings.LegendaryRequiresInspiration && !inspired)
						quality = 5;

				return (QualityCategory)quality;

			}
			else if (StaticQuality.Settings.QualitySwitch == 1)
			{
				int quality;
				switch (relevantSkillLevel)
				{
					case 0:
					case 1:
					case 2:
						quality = 0;
						break;
					case 3:
					case 4:
					case 5:
					case 6:
						quality = 1;
						break;
					case 7:
					case 8:
					case 9:
					case 10:
						quality = 2;
						break;
					case 11:
					case 12:
					case 13:
					case 14:
						quality = 3;
						break;
					case 15:
					case 16:
					case 17:
						quality = 4;
						break;
					case 18:
					case 19:
						quality = 5;
						break;
					case 20:
						quality = 6;
						break;
					default:
						quality = 3;
						break;
				}

				if (inspired)
					quality += 2;

				if (quality < 0)
					quality = 0;
				else if (quality > 6)
					quality = 6;

				if (quality == 6)
					if (StaticQuality.Settings.LegendaryRequiresInspiration && !inspired)
						quality = 5;

				return (QualityCategory)quality;
			}

			return __result;
		}
	}

	[HarmonyPatch(typeof(QualityUtility), nameof(QualityUtility.SendCraftNotification))]
	class Path_SendCraftNotification
	{
		public static bool Prefix() => StaticQuality.Settings.CraftNotify;
	}
}