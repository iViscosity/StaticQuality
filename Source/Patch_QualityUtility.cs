﻿using RimWorld;
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

				if (quality < 0)
					quality = 0;
				else if (quality > 6)
					quality = 6;

				if (quality == 6)
					if (!inspired && StaticQuality.Settings.LegendaryRequiresInspiration)
						quality = 5;

				return (QualityCategory)quality;

			}
			else if (StaticQuality.Settings.QualitySwitch == 1)
			{
				switch (relevantSkillLevel)
				{
					case 0:
					case 1:
					case 2:
						return (QualityCategory)0;
					case 3:
					case 4:
					case 5:
					case 6:
						return (QualityCategory)1;
					case 7:
					case 8:
					case 9:
					case 10:
						return (QualityCategory)2;
					case 11:
					case 12:
					case 13:
					case 14:
						return (QualityCategory)3;
					case 15:
					case 16:
					case 17:
						return (QualityCategory)4;
					case 18:
					case 19:
						return (QualityCategory)5;
					case 20:
						if (!inspired && StaticQuality.Settings.LegendaryRequiresInspiration == true)
							return (QualityCategory)5;
						else
							return (QualityCategory)6;
					default:
						return (QualityCategory)3;
				}
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