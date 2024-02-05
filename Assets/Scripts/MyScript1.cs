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
    private float time = default;
    [SerializeField, Range(-30f, 30f)]
    private float nPosY = 10;

    [SerializeField, Range(-30f, 30f)]
    private float nPosX = 0;

    [SerializeField, Range(-30f, 30f)]
    private float goalPosX = 10;

    [SerializeField, Range(-30f, 30f)]
    private float goalPosY = 10;

    [SerializeField, Range(0.1f, 30f)]
    private float speed = 1;

    private Vector3 start;
    private Vector3 goal;
    private Vector3 n;
    private Vector3 t1;
    private Vector3 t2;

    private Transform _pos;

    private void Start()
    {
        _pos = gameObject.transform;
        n = new Vector3(this.gameObject.transform.position.x - nPosX, this.gameObject.transform.position.y - nPosY, this.gameObject.transform.position.z);
        start = this.gameObject.transform.position;
        goal = new Vector3(this.gameObject.transform.position.x - goalPosX,this.gameObject.transform.position.y - goalPosY, this.gameObject.transform.position.z);
    }

    private void Update()
    {
        time += Time.deltaTime;
        t1 = Vector3.Lerp(start, n, time * speed);
        t2 = Vector3.Lerp(n, goal, time * speed);
        _pos.position = Vector3.Lerp(t1, t2, time * speed);
    }

    private void OnBecameInvisible()
    {
        _pos.position = start;
        time = 0;
        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }
}
