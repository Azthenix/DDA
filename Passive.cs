using Terraria.Chat;
using Steamworks;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI.Chat;
using ReLogic.Graphics;

using DDA.UI;
using Terraria.GameContent.UI.Elements;

namespace DDA
{
    public class Passive: ModSystem
    {
        public static float score = 100;
        public static float difficulty;

        private int counter = 0;

        public override void OnWorldLoad()
        {
            base.OnWorldLoad();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (!Main.gameMenu
                && DDAUi.visible)
            {
                ddaInterface?.Update(gameTime);

                Dictionary<int, GameModeData> registeredGameModes = Main.RegisteredGameModes;
                if (registeredGameModes.ContainsKey(4))
                {
                    GameModeData cMode = registeredGameModes[4];
                    DDAUi.list.Clear();

                    DDAUi.list.Add(new UIText("\n"));
                    DDAUi.list.Add(new UIText("Enemy Damage: " + cMode.EnemyDamageMultiplier + "x"));
                    DDAUi.list.Add(new UIText("Enemy Defense: " + cMode.EnemyDefenseMultiplier + "x"));
                    DDAUi.list.Add(new UIText("Enemy HP: " + cMode.EnemyMaxLifeMultiplier + "x"));
                }
            }
            //Vector2 pos = new Vector2(Main.screenWidth / 2, 20);
            //Main.spriteBatch.Begin();
            //ChatManager.DrawColorCodedString(Main.spriteBatch, FontAssets.DeathText.Value, $"{Passive.score} {Passive.difficulty}", pos, Color.Red, 1f, Vector2.Zero, Vector2.One * 5);

            //Main.spriteBatch.End();
        }

        public override void PostUpdateEverything()
        {
            base.PostUpdateEverything();

            //counter++;

            //if(counter >= 3600)
            //{

            //}
            try
            {
                counter++;
                if (counter >= 60)
                {
                    var sampleData = new DDAModel.ModelInput()
                    {
                        Passive_score = score,
                    };

                    var result = DDAModel.Predict(sampleData);
                    difficulty = result.Score;

                    bool expert = false;
                    bool master = false;

                    if (difficulty >= GameModeData.MasterMode.EnemyDamageMultiplier)
                    {
                        master = true;
                    }
                    else if (difficulty >= GameModeData.ExpertMode.EnemyDamageMultiplier)
                    {
                        expert = true;
                    }

                    Main.RegisteredGameModes[4] = new GameModeData
                    {
                        Id = 4,
                        EnemyMaxLifeMultiplier = difficulty,
                        EnemyDamageMultiplier = difficulty,
                        DebuffTimeMultiplier = difficulty,
                        KnockbackToEnemiesMultiplier = 1f,
                        TownNPCDamageMultiplier = difficulty,
                        EnemyDefenseMultiplier = difficulty,
                        EnemyMoneyDropMultiplier = difficulty,
                        IsExpertMode = expert,
                        IsMasterMode = master
                    };

                    Main.GameMode = 4;

                    counter %= 60;
                }
            }
            catch(Exception e)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(e.ToString()), Microsoft.Xna.Framework.Color.Red);
            }
        }

        internal DDAUi ddaUI;
        public UserInterface ddaInterface;

        public override void Load()
        {
            // this makes sure that the UI doesn't get opened on the server
            // the server can't see UI, can it? it's just a command prompt

            if (!Main.dedServ)
            {
                ddaUI = new DDAUi();
                ddaUI.Initialize();
                ddaInterface = new UserInterface();
                ddaInterface.SetState(ddaUI);
            }
        }

        private bool DrawSomethingUI()
        {
            // it will only draw if the player is not on the main menu
            if (!Main.gameMenu
                && DDAUi.visible)
            {
                ddaInterface.Draw(Main.spriteBatch, new GameTime());
            }
            return true;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Add(new LegacyGameInterfaceLayer("Cool Mod: Something UI", DrawSomethingUI, InterfaceScaleType.UI));

            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "DDA: AI Stats",
                    delegate {
                        Vector2 pos = new Vector2(Main.screenWidth/2, 20);

                        ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, $"Score: {Passive.score}\nDifficulty: {Passive.difficulty}", pos, Color.Cyan, 0f, Vector2.Zero, Vector2.One);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
