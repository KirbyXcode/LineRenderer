using UnityEngine;

public class UISig03 : UISignature
{
    private void Awake()
    {
        Bind(UIEvent.SIG03_PANEL);
    }

    protected override void Start()
    {
        sigRootTrans = GameObject.Find("Canvas/SigRoot03").transform;
        planeGo = Global.FindChild(transform, "Plane03");
        base.Start();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SIG03_PANEL:
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
        bool isPainting = LineManager.Instance.IsPainting(WindowType.Window03);

        if (!isPainting) return;

        //Dispatch(AreaCode.Camera, 0, WindowType.Window01);
        //288.59f
        sprite = ScreenShot(2471.4f, 1148f);
        CreateSig(sprite);
        //SetButtonsActive(false);
        Dispatch(AreaCode.Line, 0, WindowType.Window03);
        ExitAnim();
        Dispatch(UIEvent.PLUS_DEACTIVE, WindowType.Window03);
    }

    protected override void OnButtonClear()
    {
        Dispatch(AreaCode.Line, 0, WindowType.Window03);
    }

    protected override void OnButtonClose()
    {
        base.OnButtonClose();
        Dispatch(AreaCode.Line, 0, WindowType.Window03);
        //SetButtonsActive(true);
        //SetColliderActive(true);
    }
}
