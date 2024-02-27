// ---------------------------------------------------------  
// LexicalAnalysis.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using System;

public class LexicalAnalysis 
{
    public string SceneAnalysis(int sceneConut ,string nowSceneName)
    {
        string sceneName = default;
        int nameConut = nowSceneName.Length;
        int stageConut = int.Parse(nowSceneName.Substring(5, nameConut - 5));
        stageConut++;
        if (stageConut <= sceneConut - 1)
        {
            sceneName = nowSceneName.Substring(0, 5) + stageConut.ToString();
        }
        else
        {
            sceneName = "TitleScene";
        }
        return sceneName;
    }
}
