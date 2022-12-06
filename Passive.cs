using Terraria.Chat;
using Steamworks;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

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

                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"{result.Passive_score} {result.Difficulty} {result.Score}"), Microsoft.Xna.Framework.Color.White);

                    counter %= 60;
                }
            }
            catch(Exception e)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(e.ToString()), Microsoft.Xna.Framework.Color.White);
            }
        }
    }
}
