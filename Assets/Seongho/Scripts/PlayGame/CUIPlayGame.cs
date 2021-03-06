﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CUIPlayGame : MonoBehaviour
{
    public Image InstEdgeFire = null;//테두리효과
    public Image InstStartPanel = null;//시작시 페이드인효과

    public bool InstChangeHPBar = true;
    //PlayUI
    public Slider InstSliderHPBar = null;//체력바
    public Slider InstSliderAddHPBar = null;//추가체력바
    public Image InstSliderHPBarFill;
    public Slider InstSliderBoostBar = null;//부스터게이지
    public Slider InstSliderJoyStick = null;//조이스틱
    public Image InstItem_1 = null;
    public Image InstItem_2 = null;

    [SerializeField]
    private Text InstTxtScore = null;
    [SerializeField]
    private Text InstTxtCoin = null;
    public Button InstBtnPause = null;
    public Button InstBtnPauseClose = null;
    public Slider InstSliderTrackProgress = null;
    [SerializeField]
    private Text InstTxtStageNumber = null;

    //Theme1 UI
    [SerializeField]
    private Image InstTheme1Left = null;
    [SerializeField]
    private Image InstTheme1Right = null;
    //Theme2 UI
    [SerializeField]
    private Image InstTheme2Slow = null;

    [SerializeField]
    private Image InstImageWarning = null;
    [SerializeField]
    private Image InstImageSlide = null;

    [SerializeField]
    private Image mInstImageLeftTheme = null;
    [SerializeField]
    private Image mInstImageRightTheme = null;

    [SerializeField]
    private Sprite mTheme2Icon = null;
    [SerializeField]
    private Sprite mTheme3Icon = null;
    
    
    //PauseUI
    [Space]
    [SerializeField]
    private GameObject InstPanelPause = null;
    [SerializeField]
    private Text InstTxtPauseScore = null;

    //GameOverUI
    [Space]
    public GameObject InstUIGameOver = null;
    public Button InstBtnMoveLobby = null;
    [SerializeField]
    private Text InstTxtGameOverStage = null;
    [SerializeField]
    private Text InstTxtGameOverScore = null;
    [SerializeField]
    private Text InstTxtGameOverCoin = null;

    //Retire - CheckCoin
    [Space]
    public GameObject InstPanelCheckCoin = null;
    public Button InstBtnSubmitRetire = null;
    [SerializeField]
    private Text InstTxtSelectTheme= null;

    private void Update()
    {
        HealthBarColorChange();
    }
    void HealthBarColorChange()
    {
        if(InstSliderHPBar.value<0.2f&&InstChangeHPBar==true)
        {
            //InstSliderHPBar.fillRect.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            
            DOTween.To(() => InstSliderHPBar.fillRect.GetComponent<Image>().color, (color) =>
                InstSliderHPBar.fillRect.GetComponent<Image>().color = color, new Color(1, 1, 0, 0.5f), 0.5f)
                .OnComplete(() => { InstSliderHPBar.fillRect.GetComponent<Image>().color = new Color(0, 0, 0, 1); }).
                SetLoops(-1,LoopType.Restart).SetId("ABCD");

            InstChangeHPBar = false;
        }
        else if(InstSliderHPBar.value>0.2f&&InstChangeHPBar==false)
        {
            InstSliderHPBar.fillRect.GetComponent<Image>().color = new Color(1, 0, 0, 1);
            DOTween.Kill("ABCD");
            InstChangeHPBar = true;
        }
    }
    
    #region Play UI
    public void SetTxtScore(int value)
    {
        InstTxtScore.text = string.Format("SCORE : {0}", value.ToString());
    }
    public void SetTxtCoin(int value)
    {
        InstTxtCoin.text = string.Format("{0}", value.ToString());
    }
    public void SetTxtStageNumber(int tNumber)
    {
        InstTxtStageNumber.text = string.Format("STAGE : {0}", tNumber);

        DOTween.To(() => InstTxtStageNumber.color, (color) => InstTxtStageNumber.color = color, new Color(1, 1, 1, 0), 1.0f)
            .OnStart(() => InstTxtStageNumber.gameObject.SetActive(true))
            .OnComplete(()=>InstTxtStageNumber.gameObject.SetActive(false));

    }

    public void ShowTxtSelectTheme(int left,int right)
    {
        //InstTxtSelectTheme.text = string.Format("{0} : LEFT / RIGHT : {1}", left, right);
        //InstTxtSelectTheme.gameObject.SetActive(true);
        mInstImageLeftTheme.sprite = GetThemeIcon(left);
        mInstImageRightTheme.sprite = GetThemeIcon(right);
        mInstImageLeftTheme.color = Color.white;
        mInstImageRightTheme.color = Color.white;
    }
    public void HideTxtSelectTheme()
    {
        InstTxtSelectTheme.gameObject.SetActive(false);
    }
    public void SelectTheme(int tLeftOrRight)
    {
        if(tLeftOrRight == -1)
        {
            mInstImageRightTheme.color = new Color(1, 1, 1, 0.35f);
            if (DOTween.IsTweening("TweenSelectTheme") == false)
            {
                DOTween.To(() => mInstImageLeftTheme.color, (color) => mInstImageLeftTheme.color = color, Color.white, 0.15f)
                    .SetLoops(12, LoopType.Yoyo)
                    .OnComplete(() =>
                    {
                        mInstImageLeftTheme.color = new Color(1, 1, 1, 0);
                        mInstImageRightTheme.color = new Color(1, 1, 1, 0);
                    })
                    .SetId("TweenSelectTheme");
            }
        }
        else
        {
            mInstImageLeftTheme.color = new Color(1, 1, 1, 0.35f);
            if (DOTween.IsTweening("TweenSelectTheme") == false)
            {
                DOTween.To(() => mInstImageRightTheme.color, (color) => mInstImageRightTheme.color = color, Color.white, 0.15f)
                    .SetLoops(12, LoopType.Yoyo)
                    .OnComplete(() =>
                    {
                        mInstImageRightTheme.color = new Color(1, 1, 1, 0);
                        mInstImageLeftTheme.color = new Color(1, 1, 1, 0);

                    })
                    .SetId("TweenSelectTheme");
            }
        }
    }
    private Sprite GetThemeIcon(int theme)
    {
        Sprite tIcon = null;

        if(theme == 1)
        {
            tIcon = mTheme2Icon;
        }
        else
        {
            tIcon = mTheme3Icon;
        }

        return tIcon;
    }

    public void AlphaValue()
    {
      InstEdgeFire.color = new Color(1, 0, 0, 0.3f);
      Invoke("InvokeAlphaValue",1.0f);
    }
    public void InvokeAlphaValue()
    {
        InstEdgeFire.color = new Color(1, 0, 0, 0.0f);
    }
    public void FadeInPanel()
    {
        DOTween.To(() => InstStartPanel.color, (color) => 
        InstStartPanel.color = color, new Color(0, 0, 0, 0), 1.0f).OnComplete(() =>
        InstStartPanel.gameObject.SetActive(false));
    }
    #endregion

    #region Pause
    public void ShowUIPause()
    {
        InstPanelPause.SetActive(true);
    }
    public void SetTxtPauseScore(int score)
    {
        InstTxtPauseScore.text = score.ToString();
    }
    public void HideUIPause()
    {
        InstPanelPause.SetActive(false);
    }
    #endregion
    
    #region Retire - CheckCoin
    public void OnClickBtnCheckCoinOpen()
    {
        InstPanelCheckCoin.SetActive(true);
    }
    public void OnClickBtnCheckCoinClose()
    {
        InstPanelCheckCoin.SetActive(false);
    }
    #endregion

    #region GameOver
    public void ShowUIGameOver(int tStage,int tScore,int tCoin)
    {
        SetTxtGameOverStage(tStage);
        SetTxtGameOverScore(tScore);
        SetTxtGameOverCoin(tCoin);
        InstUIGameOver.SetActive(true);
    }
    public void HideUIGameOver()
    {
        InstUIGameOver.SetActive(false);
    }
    private void SetTxtGameOverStage(int tStage)
    {
        InstTxtGameOverStage.text = string.Format("STAGE : {0}", tStage);
    }
    private void SetTxtGameOverScore(int tScore)
    {
        InstTxtGameOverScore.text = string.Format("SCORE\n{0}", tScore);
    }
    private void SetTxtGameOverCoin(int tCoin)
    {
        InstTxtGameOverCoin.text = string.Format("COIN : {0}", tCoin);
    }
    #endregion


    #region Theme1 UI
    public void ShowTheme1UI(int dir,float duration)
    {
        GameObject tTarget = dir == -1 ? InstTheme1Left.gameObject : InstTheme1Right.gameObject;

        tTarget.transform.DOMoveX(0, duration)
            .SetRelative()
            .OnStart(() =>
            {
                tTarget.SetActive(true);
            })
            .OnComplete(() => 
            {
                tTarget.SetActive(false);
            });
    }
    #endregion
    #region Theme2 UI
    public void ShowTheme2UI(bool isSlow)
    {
        InstTheme2Slow.gameObject.SetActive(isSlow);
    }
    #endregion

    public void ShowWarning()
    {
        if (DOTween.IsTweening("TweenWarning") == false)
        {
            DOTween.To(() => InstImageWarning.color, (color) => InstImageWarning.color = color, Color.white, 0.15f)
                .SetLoops(12, LoopType.Yoyo)
                .OnComplete(() => InstImageWarning.color = new Color(1, 1, 1, 0))
                .SetId("TweenWarning");
        }
    }
    public void ShowSlideIcon()
    {
        if (DOTween.IsTweening("TweenSlide") == false)
        {
            DOTween.To(() => InstImageSlide.color, (color) => InstImageSlide.color = color, Color.white, 0.15f)
                .SetLoops(12, LoopType.Yoyo)
                .OnComplete(() => InstImageWarning.color = new Color(1, 1, 1, 0))
                .SetId("TweenSlide");
        }

    }
}

