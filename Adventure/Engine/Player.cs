﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player: LivingCreature
    {
        public Location CurrentLocation { get; set; }

        public int Coins { get; set; }
        public int EXPPoints { get; set; }
        public int Level { get; set; }

        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }

        public Player(int currentMeatLeft, int maximumMeatLeft, int coins,
            int expPoints, int level):base(currentMeatLeft, maximumMeatLeft)
        {
            Coins = coins;
            EXPPoints = expPoints;
            Level = level;

            Inventory = new List<InventoryItem>();
            Quests= new List<PlayerQuest>();
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.ItemRequiredToEnter == null)
            { 
                return true;
            }

            foreach(InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == location.ItemRequiredToEnter.ID)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasThisQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CompletedThisQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }

            return false;
        }


        public bool HasAllQuestCompletionItems(Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                bool foundItemInPlayersInvetory = false;

                foreach(InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == qci.Details.ID)
                    {
                        foundItemInPlayersInvetory = true;

                        if (ii.Quantity < qci.Quantity)
                        {
                            return false;
                        }
                    }
                }

                if (!foundItemInPlayersInvetory)
                {
                    return false;
                }
            }

            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                foreach(InventoryItem ii in Inventory)
                {
                    if(ii.Details.ID == qci.Details.ID)
                    {
                        ii.Quantity -= qci.Quantity;
                        break;
                    }
                }
            }
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            foreach(InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == itemToAdd.ID)
                {
                    ii.Quantity++;

                    return;
                }
            }

            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }

        public void MarkQuestCompleted(Quest quest)
        {
            foreach(PlayerQuest pq in Quests)
            {
                if (pq.Details.ID == quest.ID)
                {
                    pq.IsCompleted = true;

                    return;
                }
            }
        }  
    }
}
