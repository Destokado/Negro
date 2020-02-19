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
      
      if (gameStateInProperTextFormat[0] == '_')
         return new GameState(gameStateInProperTextFormat.Substring(1), GameState.Type.ForceEvent);
      
      if (gameStateInProperTextFormat[0] == '!')
         return new GameState(gameStateInProperTextFormat.Substring(1), GameState.Type.NotExists);
      
      return new GameState(gameStateInProperTextFormat, GameState.Type.Exists);
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