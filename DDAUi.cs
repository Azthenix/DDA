using IL.Terraria.GameContent.Events;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.ModLoader;



using DDA;
using Terraria.GameInput;

namespace DDA.UI
{
	public class DDAUi : UIState
	{
		public static bool visible;
		public UIPanel panel;
        public static UIList list = new UIList();

        public int width = 260;
		public int height = 250;

		public override void OnInitialize()
		{
			// if you set this to true, it will show up in game
			visible = true;

			panel = new DragableUIPanel(); //initialize the panel
										   // ignore these extra 0s
			panel.Left.Set(600, 0); //this makes the distance between the left of the screen and the left of the panel 500 pixels (somewhere by the middle)
			panel.Top.Set(100, 0); //this is the distance between the top of the screen and the top of the panel
			panel.Width.Set(width, 0);
			panel.Height.Set(height, 0);

			UIText title = new UIText("Game Mode Data\n", 0.6f, true);
			UIList container = new UIList();
			container.Width.Set(width, 0);
			container.Height.Set(height, 0);

			//Dictionary<int, int> fqList = new Dictionary<int, int>();
			//fqList.Add(ItemID.Gel, 10);
			//fqList.Add(ItemID.PinkGel, 1);
			//FetchQuest fq = new FetchQuest(NPCID.Guide, fqList);
			//FetchQuest fq2 = new FetchQuest(NPCID.Guide, fqList);
			//FetchQuest fq3 = new FetchQuest(NPCID.Guide, fqList);
			//FetchQuest fq4 = new FetchQuest(NPCID.Guide, fqList);
			//QuestJournal.questList.Add(fq);
			//QuestJournal.questList.Add(fq2);
			//QuestJournal.questList.Add(fq3);
			//QuestJournal.questList.Add(fq4);

			UIText qSample2 = new UIText($"{Lang.GetItemNameValue(ItemID.Gel)}");
			UIList qContainer = new UIList();
			qContainer.Width.Set(width, 0);
			qContainer.Height.Set(height, 0);
			//qContainer.AddRange(QuestJournal.questList);
			qContainer.SetScrollbar(new UIScrollbar());

            //Score: {Passive.score}\nDifficulty: {Passive.difficulty}


            Dictionary<int, GameModeData> registeredGameModes = Main.RegisteredGameModes;
			if (registeredGameModes.ContainsKey(4))
			{
                GameModeData cMode = registeredGameModes[4];
                list.Width.Set(width, 0);
                list.Height.Set(height, 0);
                list.Add(new UIText("\n"));
                list.Add(new UIText("Enemy Damage: " + cMode.EnemyDamageMultiplier + "x"));
                list.Add(new UIText("Enemy Defense: " + cMode.EnemyDefenseMultiplier + "x"));
                list.Add(new UIText("Enemy HP: " + cMode.EnemyMaxLifeMultiplier + "x"));
            }

            //list.Add(new UIText("\n"));
            //list.Add(new UIText("Player Damage: " + Main.LocalPlayer.GetDamage(DamageClass.Generic).Base + "x"));

			container.Add(title);
			container.Add(list);	
			container.Add(qContainer);

			panel.Append(container);

			Append(panel); //appends the panel to the UIState
		}
	}

	public class QuestPanel : UIPanel
	{
		public QuestPanel() { }
	}
}
