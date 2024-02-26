// ---------------------------------------------------------  
// GameOver.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    #region 変数  

    #region const定数

    // キーの入力値の補正
    private const float PLUS = 1f;
    private const float MINUS = -1f;

    private const float SELECT_UI_POS_X = 170f;

    #endregion

    [SerializeField, Header("StageUIオブジェクト")]
    private GameObject _titleUI = default;

    [SerializeField, Header("ExitUIオブジェクト")]
    private GameObject _retryUI = default;

    // TextMesh格納用
    private TextMeshProUGUI _titleText = default;
    private TextMeshProUGUI _retryText = default;

    // 
    private bool flag = false;

    // プレイヤーの入力方向の格納場所
    private float _horizontal = default;
    private float _vertical = default;

    private string _sceneName = default;

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        // 初期設定
        _titleText = _titleUI.GetComponent<TextMeshProUGUI>();
        _retryText = _retryUI.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(true);
        _sceneName = SceneManager.GetActiveScene().name;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        // 入力値の代入
        _vertical = Input.GetAxis("Vertical");

        if (_vertical == PLUS)
        {
            flag = false;
        }
        else if (_vertical == MINUS)
        {
            flag = true;
        }

        if (Input.GetButtonDown("Enter") && flag)
        {
            SceneManager.LoadScene(_sceneName);
        }
        else if (Input.GetButtonDown("Enter") && !flag)
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (flag)
        {
            // ステージを選択していない状態にする
            _titleUI.transform.localPosition =
                new Vector3(SELECT_UI_POS_X, _titleUI.transform.localPosition.y, _titleUI.transform.localPosition.x);
            _titleText.color = Color.white;

            // Exitを選択している状態にする
            _retryUI.transform.localPosition = new Vector3(0, _retryUI.transform.localPosition.y, _retryUI.transform.localPosition.x);
            _retryText.color = Color.red;
        }
        else
        {
            // ステージを選択している状態にする
            _titleUI.transform.localPosition = new Vector3(0, _titleUI.transform.localPosition.y, _titleUI.transform.localPosition.x);
            _titleText.color = Color.red;

            // Exitを選択していない状態にする
            _retryUI.transform.localPosition = new Vector3(SELECT_UI_POS_X, _retryUI.transform.localPosition.y, _retryUI.transform.localPosition.x);
            _retryText.color = Color.white;
        }
    }

    #endregion

}
