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
        public int QualitySwitch = 0;
        public bool CraftNotify = false;
        public bool LegendaryRequiresInspiration = true;

        public void DoWindowContents(Rect canvas)
        {
            Listing_Standard options = new Listing_Standard();
            options.ColumnWidth = 250f;
            options.Begin(canvas);

            options.Label(Translator.Translate("ItemQualityOptions"));
            options.Gap(12f);

            bool[] qs = new bool[4];
            
            qs[0] = options.RadioButton(Translator.Translate("VanillaQuality"), (QualitySwitch == 0), 0f, Translator.Translate("TheRimWorldDefault"));
            qs[1] = options.RadioButton(Translator.Translate("StaticQuality"), (QualitySwitch == 1), 0f, Translator.Translate("ItemQualityIs"));
            qs[2] = options.RadioButton(Translator.Translate("StaticQuality1"), (QualitySwitch == 2), 0f, Translator.Translate("ItemQualityIs1"));
            qs[3] = options.RadioButton(Translator.Translate("StaticQuality2"), (QualitySwitch == 3), 0f, Translator.Translate("ItemQualityIs2"));
            options.Gap(24f);

            string text_cn = Translator.Translate("CraftingNotification");
            options.CheckboxLabeled(text_cn, ref CraftNotify, Translator.Translate("CraftingEnabled"));
            options.Gap(12f);

            string text_li = Translator.Translate("LegendaryOnlyWithInspiration");
            options.CheckboxLabeled(text_li, ref LegendaryRequiresInspiration, Translator.Translate("LegendaryWithInspirationEnabled"));
            options.Gap(12f);

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
            Scribe_Values.Look<int>(ref QualitySwitch, "quality_switch", 1, true);
            Scribe_Values.Look<bool>(ref CraftNotify, "craft_notify", true, true);
            Scribe_Values.Look<bool>(ref LegendaryRequiresInspiration, "leg_in", false, true);
        }
    }
}
