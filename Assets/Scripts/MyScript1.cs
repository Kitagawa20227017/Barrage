// ---------------------------------------------------------  
// MyScript1.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MyScript1 : MonoBehaviour
{

    // 円の半径を設定します。
    public float radius = 10f;
    bool flag = false;

    // 初期位置を取得し、高さを保持します。
    Vector3 initPos;

    void Start()
    {
        // 初期位置を保持します。
        initPos = gameObject.transform.position;
    }

    void Update()
    {
        CalcPosition();
    }

    /// <Summary>
    /// オブジェクトの位置を計算するメソッドです。
    /// </Summary>
    void CalcPosition()
    {
        // 位相を計算します。
        float phase = Time.time * 0.5f * Mathf.PI;

        // 現在の位置を計算します。
        float xPos = radius * Mathf.Cos(phase);
        float zPos = radius * Mathf.Sin(phase);

        // ゲームオブジェクトの位置を設定します。
        Vector3 pos = new Vector3(-xPos, initPos.y, zPos);
        gameObject.transform.position = pos;
    }

}
