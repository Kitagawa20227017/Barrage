// ---------------------------------------------------------  
// IGameClear.cs  
//   
// ゲームクリアのインターフェース
//
// 作成日: 2024/2/24
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public interface IGameClear 
{
    public void GameClearProcessing(bool isStop);
}
