using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace DDA
{
	public class DDA : ModPlayer
    {
        public static int spRate = 1;
        public static int maxSp = 1;
        public float damage;

        public override void OnRespawn(Player player)
		{
			base.OnRespawn(player);

            Main.RegisteredGameModes[4] = new GameModeData
            {
                Id = 4,
                EnemyMaxLifeMultiplier = 1f,
                EnemyDamageMultiplier = 1f,
                DebuffTimeMultiplier = 1f,
                KnockbackToEnemiesMultiplier = 1f,
                TownNPCDamageMultiplier = 1f,
                EnemyDefenseMultiplier = 1f,
                EnemyMoneyDropMultiplier = 1f
            };

            Main.GameMode = 4;

            Passive.score = Math.Max(Passive.score - 50, 0);
        }

		public override void OnEnterWorld(Player player)
		{
			base.OnEnterWorld(player);

            Main.RegisteredGameModes[4] = new GameModeData
            {
                Id = 4,
                EnemyMaxLifeMultiplier = 1f,
                EnemyDamageMultiplier = 1f,
                DebuffTimeMultiplier = 1f,
                KnockbackToEnemiesMultiplier = 1f,
                TownNPCDamageMultiplier = 1f,
                EnemyDefenseMultiplier = 1f,
                EnemyMoneyDropMultiplier = 1f
            };

            Main.GameMode = 4;

            player.GetDamage(DamageClass.Generic) += 1.0f;
        }

        public override void PostUpdate()
        {
            base.PostUpdate();

            Main.LocalPlayer.GetDamage(DamageClass.Generic) += 1.0f;
        }
    }
}