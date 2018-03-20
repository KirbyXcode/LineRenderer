using UnityEngine;

public class UISig02 : UISignature
{
    private void Awake()
    {
        Bind(UIEvent.SIG02_PANEL);
    }

    protected override void Start()
    {
        sigRootTrans = GameObject.Find("Canvas/SigRoot02").transform;
        planeGo = Global.FindChild(transform, "Plane02");
        base.Start();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SIG02_PANEL:
                bool active = (bool)message;
                if (active)
                {
                    EnterAnim();
                }
                else
                {
                    OnButtonClose();
                }
                break;
        }
    }

    protected override void OnButtonOK()
    {
        bool isPainting = LineManager.Instance.IsPainting(WindowType.Window02);

        if (!isPainting) return;

        //Dispatch(AreaCode.Camera, 0, WindowType.Window01);
        //288.59f
        sprite = ScreenShot(1284.62f, 1147.75f);
        CreateSig(sprite);
        //SetButtonsActive(false);
        Dispatch(AreaCode.Line, 0, WindowType.Window02);
        ExitAnim();
        Dispatch(UIEvent.PLUS_DEACTIVE, WindowType.Window02);
    }

    protected override void OnButtonClear()
    {
        Dispatch(AreaCode.Line, 0, WindowType.Window02);
    }

    protected override void OnButtonClose()
    {
        base.OnButtonClose();
        Dispatch(AreaCode.Line, 0, WindowType.Window02);
        //SetButtonsActive(true);
        //SetColliderActive(true);
    }
}
