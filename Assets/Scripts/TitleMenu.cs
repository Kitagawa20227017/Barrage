// ---------------------------------------------------------  
// TitleMenu.cs  
//   
// 作成日: 2024/2/22
// 作成者: 北川 稔明 
// ---------------------------------------------------------  
using UnityEngine;
using TMPro;

public class TitleMenu : MonoBehaviour
{

    #region 変数  

    #region const定数

    // キーの入力値の補正
    private const float PLUS = 1f;
    private const float MINUS = -1f;

    private const float SELECT_UI_POS_X = 50f;

    #endregion

    [SerializeField,Header("StageUIオブジェクト")]
    private GameObject _stageSelectUI = default;

    [SerializeField, Header("ExitUIオブジェクト")]
    private GameObject _exitUI = default;

    [SerializeField,Header("ステージ選択UI")]
    private GameObject _stageSummary = default;

    // TextMesh格納用
    private TextMeshProUGUI _stageSelectText = default;
    private TextMeshProUGUI _exitText = default;

    // Exitにカーソルが合ってるかどうか
    private bool _isExit = false;

    // プレイヤーの入力方向の格納場所
    private float _vertical = default;

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        // 初期設定
        _stageSelectText = _stageSelectUI.GetComponent<TextMeshProUGUI>();
        _exitText = _exitUI.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(true);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // 入力値の代入
        _vertical = Input.GetAxis("Vertical");

        #region 入力値の補正

        // 縦軸の移動
        if (_vertical > 0)
        {
            _vertical = PLUS;
        }
        else if (_vertical < 0)
        {
            _vertical = MINUS;
        }
        else
        {
            _vertical = 0;
        }

        #endregion

        if(_vertical == PLUS)
        {
            _isExit = false;
        }
        else if(_vertical == MINUS)
        {
            _isExit = true;
        }

        if(Input.GetButtonDown("Enter") && _isExit)
        {
            // ゲーム終了
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
            #else
            Application.Quit();//ゲームプレイ終了
            #endif
        }
        else if (Input.GetButtonDown("Enter") && !_isExit)
        {
            // ステージ選択UIをアクティブ
            _stageSummary.SetActive(true);
            gameObject.SetActive(false);
        }

        if (_isExit)
        {
            // ステージを選択していない状態にする
            _stageSelectUI.transform.localPosition =
                new Vector3(SELECT_UI_POS_X, _stageSelectUI.transform.localPosition.y, _stageSelectUI.transform.localPosition.x);
            _stageSelectText.color = Color.white;

            // Exitを選択している状態にする
            _exitUI.transform.localPosition = new Vector3(0, _exitUI.transform.localPosition.y, _exitUI.transform.localPosition.x);
            _exitText.color = Color.red;
        }
        else
        {
            // ステージを選択している状態にする
            _stageSelectUI.transform.localPosition = new Vector3(0, _stageSelectUI.transform.localPosition.y, _stageSelectUI.transform.localPosition.x);
            _stageSelectText.color = Color.red;

            // Exitを選択していない状態にする
            _exitUI.transform.localPosition = new Vector3(SELECT_UI_POS_X, _exitUI.transform.localPosition.y, _exitUI.transform.localPosition.x);
            _exitText.color = Color.white;
        }
    }

    #endregion

}
