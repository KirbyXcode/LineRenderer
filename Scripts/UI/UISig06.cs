using UnityEngine;

public class UISig06 : UISignature
{
    private void Awake()
    {
        Bind(UIEvent.SIG06_PANEL);
    }

    protected override void Start()
    {
        sigRootTrans = GameObject.Find("Canvas/SigRoot06").transform;
        planeGo = Global.FindChild(transform, "Plane06");
        base.Start();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SIG06_PANEL:
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
        bool isPainting = LineManager.Instance.IsPainting(WindowType.Window06);

        if (!isPainting) return;

        //Dispatch(AreaCode.Camera, 0, WindowType.Window01);
        //288.59f
        sprite = ScreenShot(7554.53f, 1148f);
        CreateSig(sprite);
        //SetButtonsActive(false);
        Dispatch(AreaCode.Line, 0, WindowType.Window06);
        ExitAnim();
        Dispatch(UIEvent.PLUS_DEACTIVE, WindowType.Window06);
    }

    protected override void OnButtonClear()
    {
        Dispatch(AreaCode.Line, 0, WindowType.Window06);
    }

    protected override void OnButtonClose()
    {
        base.OnButtonClose();
        Dispatch(AreaCode.Line, 0, WindowType.Window06);
        //SetButtonsActive(true);
        //SetColliderActive(true);
    }
}
