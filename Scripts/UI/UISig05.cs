using UnityEngine;

public class UISig05 : UISignature
{
    private void Awake()
    {
        Bind(UIEvent.SIG05_PANEL);
    }

    protected override void Start()
    {
        sigRootTrans = GameObject.Find("Canvas/SigRoot05").transform;
        planeGo = Global.FindChild(transform, "Plane05");
        base.Start();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SIG05_PANEL:
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
        bool isPainting = LineManager.Instance.IsPainting(WindowType.Window05);

        if (!isPainting) return;

        //Dispatch(AreaCode.Camera, 0, WindowType.Window01);
        //288.59f
        sprite = ScreenShot(6364.08f, 1148f);
        CreateSig(sprite);
        //SetButtonsActive(false);
        Dispatch(AreaCode.Line, 0, WindowType.Window05);
        ExitAnim();
        Dispatch(UIEvent.PLUS_DEACTIVE, WindowType.Window05);
    }

    protected override void OnButtonClear()
    {
        Dispatch(AreaCode.Line, 0, WindowType.Window05);
    }

    protected override void OnButtonClose()
    {
        base.OnButtonClose();
        Dispatch(AreaCode.Line, 0, WindowType.Window05);
        //SetButtonsActive(true);
        //SetColliderActive(true);
    }
}
