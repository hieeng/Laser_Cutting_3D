using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] float Delaytime;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    [SerializeField] Text gemText;
    [SerializeField] Text inGameStageText;
    [SerializeField] Text inGameScoreText;
    [SerializeField] Text winStageText;
    [SerializeField] Text winScoreText;
    [SerializeField] Text loseScoreText;
    [SerializeField] Text BestScoreText;
    [SerializeField] GameObject[] FeedBackText;

    [SerializeField] GameObject[] session;
    [SerializeField] GameObject[] clearSession;
    [SerializeField] Button _nextLevelBtn;
    public GameObject StartPanel
    {
        get => startPanel;
    }

    public void Init()
    {
        _nextLevelBtn.onClick.AddListener(NextLevel);
    }
    void NextLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
    //SceneManager.LoadScene("Chewy");
    //패널
    public void OnStartPanel()
    {
        startPanel.SetActive(true);
    }

    public void OffStartPanel()
    {
        startPanel.SetActive(false);
    }

    public void OnInGamePanel()
    {
        SetInGameStageText();
        SetInGameScoreText();
        inGamePanel.SetActive(true);
    }

    public void OffInGamePanel()
    {
        inGamePanel.SetActive(false);
    }

    public void OnWinPanel()
    {
        SetWinGameStageText();
        SetWinScoreText();
        inGamePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void OffWinPanel()
    {
        winPanel.SetActive(false);
    }

    public void OnLosePanel()
    {
        OffInGamePanel();
        SetLoseScoreText();
        losePanel.SetActive(true);
    }

    public void OffLosePanel()
    {
        losePanel.SetActive(false);
    }

    
    //버튼
    public void NextButton()
    {
        OffWinPanel();
        OnStartPanel();
    }

    public void ReStartButton()
    {
        OffLosePanel();
        SceneManager.LoadScene("SampleScene");
    }

    public void ADButton()
    {
        //AD ON
    }
    

    //텍스트
    public void SetGemText(int gem)
    {
        gemText.text = string.Format("{0:n0}", gem);
    }

    public void SetInGameStageText()
    {
        inGameStageText.text = string.Format("Stage {0:n0}", GameManager.Instance.GetCurrentStageLevel());
    }

    public void SetInGameScoreText()
    {
        inGameScoreText.text = string.Format("{0:n0}", GameManager.Instance.Score);
    }

    public void SetWinGameStageText()
    {
        winStageText.text = string.Format("Stage {0:n0}", GameManager.Instance.GetCurrentStageLevel());
    }

    public void SetWinScoreText()
    {
        winScoreText.text = string.Format("{0:n0}", GameManager.Instance.Score);
    }

    public void SetLoseScoreText()
    {
        loseScoreText.text = string.Format("{0:n0}", GameManager.Instance.Score);
    }

    public void ShowFeedBack()
    {
        int rand = Random.Range(0, 3);

        FeedBackText[rand].SetActive(true);
        StartCoroutine(CoroutineShowFeedBack(FeedBackText[rand]));
    }

    IEnumerator CoroutineShowFeedBack(GameObject text)
    {
        var time = 0f;

        while (time <= Delaytime)
        {
            time += Time.deltaTime;
            text.transform.localScale = Vector3.one * time/Delaytime * 1.5f;

            yield return null;
        }
        time = 0;
        text.SetActive(false);
    }

    public void ShowCurrentSession()
    {
        session[GameManager.Instance.CurrentSession].SetActive(true);
    }

    public void ShowClearSession(int value)
    {
        clearSession[GameManager.Instance.CurrentSession - value].SetActive(true);
    }
}
