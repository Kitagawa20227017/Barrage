// ---------------------------------------------------------  
// GameClear.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{

    #region 変数  

    #region const定数

    // キーの入力値の補正
    private const float PLUS = 1f;
    private const float MINUS = -1f;

    private const float UI_MOVE_POS_X = 170f;
    private const float UI_POS_X = 220f;

    #endregion

    [SerializeField]
    private GameObject _clearUI = default;

    [SerializeField, Header("StageUIオブジェクト")]
    private GameObject _titleUI = default;

    [SerializeField, Header("ExitUIオブジェクト")]
    private GameObject _nextStageUI = default;

    // TextMesh格納用
    private TextMeshProUGUI _titleText = default;
    private TextMeshProUGUI _nextStageText = default;
    private TextMeshProUGUI _clearText = default;

    // 
    private bool flag = true;

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
        _nextStageText = _nextStageUI.GetComponent<TextMeshProUGUI>();
        _clearText = _clearUI.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(true);
        _clearText.text = SceneManager.GetActiveScene().name + " Clear";
        SceneAnalysis(SceneManager.sceneCountInBuildSettings, SceneManager.GetActiveScene().name);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
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

        if (_vertical == PLUS)
        {
            flag = true;
        }
        else if (_vertical == MINUS)
        {
            flag = false;
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
                new Vector3(UI_POS_X, _titleUI.transform.localPosition.y, _titleUI.transform.localPosition.x);
            _titleText.color = Color.white;

            // Exitを選択している状態にする
            _nextStageUI.transform.localPosition = new Vector3(UI_MOVE_POS_X, _nextStageUI.transform.localPosition.y, _nextStageUI.transform.localPosition.x);
            _nextStageText.color = Color.red;
        }
        else
        {
            // ステージを選択している状態にする
            _titleUI.transform.localPosition = new Vector3(UI_MOVE_POS_X, _titleUI.transform.localPosition.y, _titleUI.transform.localPosition.x);
            _titleText.color = Color.red;

            // Exitを選択していない状態にする
            _nextStageUI.transform.localPosition = new Vector3(UI_POS_X, _nextStageUI.transform.localPosition.y, _nextStageUI.transform.localPosition.x);
            _nextStageText.color = Color.white;
        }
    }
    /// <summary>
    /// 次のステージの検索処理
    /// </summary>
    /// <param name="sceneConut">シーンの合計数</param>
    /// <param name="nowSceneName">現在のシーン名</param>
    /// <returns>次のシーン/returns>
    public string SceneAnalysis(int sceneConut, string nowSceneName)
    {
        int nameConut = nowSceneName.Length;
        int stageConut = int.Parse(nowSceneName.Substring(5, nameConut - 5));
        stageConut++;
        if (stageConut <= sceneConut - 1)
        {
            _sceneName = nowSceneName.Substring(0, 5) + stageConut.ToString();
        }
        else
        {
            _sceneName = "TitleScene";
        }
        return _sceneName;
    }

    #endregion

}
