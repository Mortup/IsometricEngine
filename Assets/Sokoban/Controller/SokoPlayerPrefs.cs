using System.Text;
using UnityEngine;

using com.gStudios.sokoban.model.saving;

public static class SokoPlayerPrefs {

    public static bool initializated = false;

    private static string dataKey = "LevelsData";
    private static string levelsData;

    private static void Init() {
        levelsData = PlayerPrefs.GetString(dataKey, new string('0', SokobanLevelSerializer.LevelsCount()));

        initializated = true;
    }

    private static void CheckInit() {
        if (!initializated)
            Init();
    }

    public static int MaxCompletedLevel() {
        CheckInit();

        int maxLevel = -1;
        for (int i = 0; i < levelsData.Length; i++) {
            if (IsCompleted(i)) {
                maxLevel = i;
            }
        }

        return maxLevel;
    }

    public static bool IsCompleted(int n) {
        CheckInit();

        return levelsData[n] == '1';
    }

    public static void CompleteLevel(int n) {
        CheckInit();

        StringBuilder newData = new StringBuilder(levelsData);
        newData[n] = '1';
        levelsData = newData.ToString();
        PlayerPrefs.SetString(dataKey, levelsData);
    }

	
}
