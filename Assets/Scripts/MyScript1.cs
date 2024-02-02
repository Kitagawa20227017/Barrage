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

    private float _xPos = default;
    private float _yPos = default;
    private float phase = default;
    private float x = default;
    private float y = default;

    private void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
    }

    private void Update()
    {
        phase = Time.time * 0.5f * Mathf.PI;
        _xPos = 4 * Mathf.Sin(phase);
        _yPos = 4 * Mathf.Cos(phase);
        transform.position = new Vector2((x - 4) + _xPos,(y - 4) + _yPos);
    }

    private void OnBecameInvisible()
    {
        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }
}
