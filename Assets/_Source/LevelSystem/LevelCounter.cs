using UnityEngine;

namespace LevelSystem
{
    public class LevelCounter
    {
        private const string LEVEL_PROPERTY = "Level";
        
        private int Level;

        public LevelCounter()
        {
            GetLevel();
        }
        
        public void SetLevel(int level)
        {
            if(level <= Level) return;
            Level = level;
            SaveData();
        }
        
        public int GetLevel()
        {
            int level = PlayerPrefs.GetInt(LEVEL_PROPERTY, 0);
            return level > Level ? level : Level;
        }
        
        public void ResetLevelData()
        {
            PlayerPrefs.SetInt(LEVEL_PROPERTY, 0);
            PlayerPrefs.Save();
        }
        
        private void SaveData()
        {
            int level = PlayerPrefs.GetInt(LEVEL_PROPERTY, 0);
            Debug.Log("SavedLevel - " + (level > Level ? level : Level));
            PlayerPrefs.SetInt(LEVEL_PROPERTY, level > Level ? level : Level);
            PlayerPrefs.Save();
        }
    }
}