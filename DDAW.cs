using Terraria.Chat;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace DDA
{
    public class DDAW : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            spawnRate = (spawnRate / DDA.spRate);
            maxSpawns = (maxSpawns * DDA.maxSp);
        }

        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);

            if (npc.friendly)
                return;

            if(npc.boss)
            {
                Passive.score += 20;
            }
            else
            {
                Passive.score++;
            }

            Passive.score = Math.Min(Passive.score, 300);
        }
    }
}
