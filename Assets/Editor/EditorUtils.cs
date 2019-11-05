using System.IO;
using UnityEngine;
using UnityEditor;

public class EditorUtils
{
    [MenuItem("Utils/Delete PlayerPrefs")]
    static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    // Path to save the screenshot file to.
    const string ScreenshotsPath = "Screenshots";

    [MenuItem("Utils/Screenshot %#d")]
    static void Screenshot()
    {
        if (!Directory.Exists(ScreenshotsPath))
            Directory.CreateDirectory(ScreenshotsPath);

        string fileName = ScreenshotsPath + "/Screenshot"
                + System.DateTime.Now.ToString("_yyyy-MM-dd_")
                + System.DateTime.Now.ToString("hh-mm-ss")
                + ".png";

#if UNITY_5
		Application.CaptureScreenshot(fileName, 1);
#else
        ScreenCapture.CaptureScreenshot(fileName, 1);
#endif
    }
}
