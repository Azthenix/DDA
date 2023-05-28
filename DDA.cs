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

using DDA.UI;

namespace DDA
{
	public class DDA : ModPlayer
    {
        public static int spRate = 1;
        public static int maxSp = 1;
        public float damage;

        internal DDAUi ddaUI;
        public UserInterface questInterface;

        public override void Load()
        {
            // this makes sure that the UI doesn't get opened on the server
            // the server can't see UI, can it? it's just a command prompt
            if (!Main.dedServ)
            {
                ddaUI = new DDAUi();
                dda.Initialize();
                questInterface = new UserInterface();
                questInterface.SetState(ddaUI);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            // it will only draw if the player is not on the main menu
            if (!Main.gameMenu
                && DDAUi.visible)
            {
                questInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Add(new LegacyGameInterfaceLayer("Cool Mod: Something UI", DrawSomethingUI, InterfaceScaleType.UI));
        }

        private bool DrawSomethingUI()
        {
            // it will only draw if the player is not on the main menu
            if (!Main.gameMenu
                && DDAUi.visible)
            {
                questInterface.Draw(Main.spriteBatch, new GameTime());
            }
            return true;
        }
    }

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