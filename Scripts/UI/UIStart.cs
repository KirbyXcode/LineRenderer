using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using HedgehogTeam.EasyTouch;

public class UIStart : UIBase
{
    private Button mButton_Plus01;
    private Button mButton_Plus02;
    private Button mButton_Plus03;
    private Button mButton_Plus04;
    private Button mButton_Plus05;
    private Button mButton_Plus06;
    private QuickTap mTap_Logo;


    private void Awake()
    {
        Bind(UIEvent.PLUS_ACTIVE, UIEvent.PLUS_DEACTIVE);
    }

    private void Start()
    {
        mButton_Plus01 = Global.FindChild<Button>(transform, "Left_LeftButton");
        mButton_Plus02 = Global.FindChild<Button>(transform, "Left_MiddleButton");
        mButton_Plus03 = Global.FindChild<Button>(transform, "Left_RightButton");
        mButton_Plus04 = Global.FindChild<Button>(transform, "Right_LeftButton");
        mButton_Plus05 = Global.FindChild<Button>(transform, "Right_MiddleButton");
        mButton_Plus06 = Global.FindChild<Button>(transform, "Right_RightButton");
        mTap_Logo = Global.FindChild<QuickTap>(transform, "Logo");

        InitListener();
    }

    private void InitListener()
    {
        mButton_Plus01.onClick.AddListener(OnButtonPlus01);
        mButton_Plus02.onClick.AddListener(OnButtonPlus02);
        mButton_Plus03.onClick.AddListener(OnButtonPlus03);
        mButton_Plus04.onClick.AddListener(OnButtonPlus04);
        mButton_Plus05.onClick.AddListener(OnButtonPlus05);
        mButton_Plus06.onClick.AddListener(OnButtonPlus06);
        mTap_Logo.onTap.AddListener(OnTapLogo);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.PLUS_DEACTIVE:
                WindowType type = (WindowType)message;
                HidePlus(type);
                break;
            case UIEvent.PLUS_ACTIVE:
                WindowType type1 = (WindowType)message;
                ShowPlus(type1);
                break;
        }
    }

    private void OnTapLogo(Gesture gesture)
    {
        LineManager.Instance.ClearAll();
    }

    private void HidePlus(WindowType type)
    {
        switch (type)
        {
            case WindowType.Window01:
                mButton_Plus01.transform.DOScale(0, 0.4f);
                break;
            case WindowType.Window02:
                mButton_Plus02.transform.DOScale(0, 0.4f);
                break;
            case WindowType.Window03:
                mButton_Plus03.transform.DOScale(0, 0.4f);
                break;
            case WindowType.Window04:
                mButton_Plus04.transform.DOScale(0, 0.4f);
                break;
            case WindowType.Window05:
                mButton_Plus05.transform.DOScale(0, 0.4f);
                break;
            case WindowType.Window06:
                mButton_Plus06.transform.DOScale(0, 0.4f);
                break;
        }
    }

    private void ShowPlus(WindowType type)
    {
        switch (type)
        {
            case WindowType.Window01:
                mButton_Plus01.transform.DOScale(1, 0.4f);
                break;
            case WindowType.Window02:
                mButton_Plus02.transform.DOScale(1, 0.4f);
                break;
            case WindowType.Window03:
                mButton_Plus03.transform.DOScale(1, 0.4f);
                break;
            case WindowType.Window04:
                mButton_Plus04.transform.DOScale(1, 0.4f);
                break;
            case WindowType.Window05:
                mButton_Plus05.transform.DOScale(1, 0.4f);
                break;
            case WindowType.Window06:
                mButton_Plus06.transform.DOScale(1, 0.4f);
                break;
        }
    }

    private void OnButtonPlus01()
    {
        Dispatch(UIEvent.SIG01_PANEL, true);
    }

    private void OnButtonPlus02()
    {
        Dispatch(UIEvent.SIG02_PANEL, true);
    }

    private void OnButtonPlus03()
    {
        Dispatch(UIEvent.SIG03_PANEL, true);
    }

    private void OnButtonPlus04()
    {
        Dispatch(UIEvent.SIG04_PANEL, true);
    }

    private void OnButtonPlus05()
    {
        Dispatch(UIEvent.SIG05_PANEL, true);
    }

    private void OnButtonPlus06()
    {
        Dispatch(UIEvent.SIG06_PANEL, true);
    }
}
