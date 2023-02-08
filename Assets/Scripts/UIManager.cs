using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
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

    public GameObject StartPanel
    {
        get => startPanel;
    }
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
        winPanel.SetActive(true);
    }

    public void OffWinPanel()
    {
        winPanel.SetActive(false);
    }

    public void OnLosePanel()
    {
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
        SceneManager.LoadScene("Chewy");
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
        inGameStageText.text = string.Format("레벨 : {0:n0}", GameManager.Instance.GetCurrentStageLevel());
    }

    public void SetInGameScoreText()
    {
        inGameScoreText.text = string.Format("{0:n0}", GameManager.Instance.Score);
    }

    public void SetWinGameStageText()
    {
        inGameStageText.text = string.Format("레벨 : {0:n0}", GameManager.Instance.GetCurrentStageLevel());
    }

    public void SetWinScoreText()
    {
        winScoreText.text = string.Format("{0:n0}", GameManager.Instance.Score);
    }

    public void SetLoseScoreText()
    {
        loseScoreText.text = string.Format("{0:n0}", GameManager.Instance.Score);
    }
}
