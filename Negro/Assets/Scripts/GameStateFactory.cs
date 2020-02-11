using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameStateFactory
{
   
   public static HashSet<GameState> BuildGameStates(string gameStatesInTextFormat)
   {
      List<string> gsSeparatedInText = SeparateGameStatesFromText(gameStatesInTextFormat);
      
      HashSet<GameState> gsList = new HashSet<GameState>();
      foreach (string gs in gsSeparatedInText)
         gsList.Add(BuildGameState(gs));
      
      return gsList;
   }
   
   private static GameState BuildGameState(string gameStateInProperTextFormat)
   {
      if (string.IsNullOrEmpty(gameStateInProperTextFormat))
         return null;
      
      if (gameStateInProperTextFormat[0] != '!') 
         return new GameState(gameStateInProperTextFormat, true);
      
      string gsName = gameStateInProperTextFormat.Substring(1);  
      return new GameState(gsName, false);
   }

   private static List<string> SeparateGameStatesFromText(string gameStatesInTextFormat)
   {
      List<string> separatedStrings = gameStatesInTextFormat.Split(',').ToList<string>();
      
      List<string> retList = new List<string>();
      foreach (string element in separatedStrings)
         retList.Add(element.Trim().ToUpper());
      
      return retList;
   }
   
}