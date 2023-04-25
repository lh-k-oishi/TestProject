//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace Human.Yokohama.Tabibito
//{
//    static public class SaveData
//    {
//        static int dataSlot = 0;
//        static public List<int> learnedOnomatopoeiaId = new List<int>() { };
//        static public int day = 1;
//        static public int eventNumber = 2;
//        static public int playerMoney = 0;

//        static public void LoadGameData(int slot)
//        {
//            dataSlot = slot;
//            learnedOnomatopoeiaId.Clear();
//            for (int i = 0; i < Onomatopoeia.onomatopoeiaTotal; i++)
//            {
//                int learnedOnomatopia = PlayerPrefs.GetInt("Slot" + dataSlot + "LearnedOnomatopoeia" + i, 0);
//                if (learnedOnomatopia == 1)
//                {
//                    learnedOnomatopoeiaId.Add(i);
//                }
//            }
//            day = PlayerPrefs.GetInt("Slot" + dataSlot + "Day", 1);
//            eventNumber = PlayerPrefs.GetInt("Slot" + dataSlot + "EventNumber", 1);
//            playerMoney = PlayerPrefs.GetInt("Slot" + dataSlot + "PlayerMoney", 0);
//        }

//        static public void SaveGameData()
//        {
//            for (int i = 0; i < learnedOnomatopoeiaId.Count; i++)
//            {
//                PlayerPrefs.SetInt("Slot" + dataSlot + "LearnedOnomatopoeia" + learnedOnomatopoeiaId[i], 1);
//            }
//            PlayerPrefs.SetInt("Slot" + dataSlot + "Day", day);
//            PlayerPrefs.SetInt("Slot" + dataSlot + "EventNumber", eventNumber);
//            PlayerPrefs.SetInt("Slot" + dataSlot + "PlayerMoney", playerMoney);
//        }

//        static public void ClearSaveData(int slot)
//        {
//            for (int i = 0; i < Onomatopoeia.onomatopoeiaTotal; i++)
//            {
//                PlayerPrefs.DeleteKey("Slot" + slot + "LearnedOnomatopoeia" + i);
//            }
//            PlayerPrefs.DeleteKey("Slot" + slot + "Day");
//            PlayerPrefs.DeleteKey("Slot" + slot + "EventNumber");
//            PlayerPrefs.DeleteKey("Slot" + slot + "PlayerMoney");
//        }
//    }
//}