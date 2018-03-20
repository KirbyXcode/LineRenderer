using UnityEngine;
using UnityEngine.UI;

public class UISig01 : UISignature
{
    private void Awake()
    {
        Bind(UIEvent.SIG01_PANEL);
    }

    protected override void Start()
    {
        sigRootTrans = GameObject.Find("Canvas/SigRoot01").transform;
        planeGo = Global.FindChild(transform, "Plane01");
        base.Start();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SIG01_PANEL:
                bool active = (bool)message;
                if(active)
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
        bool isPainting = LineManager.Instance.IsPainting(WindowType.Window01);

        if (!isPainting) return;

        //Dispatch(AreaCode.Camera, 0, WindowType.Window01);
        //288.59f
        sprite = ScreenShot(98.38f, 1148f);
        CreateSig(sprite);
        //SetButtonsActive(false);
        Dispatch(AreaCode.Line, 0, WindowType.Window01);
        ExitAnim();
        Dispatch(UIEvent.PLUS_DEACTIVE, WindowType.Window01);
    }

    protected override void OnButtonClear()
    {
        Dispatch(AreaCode.Line, 0, WindowType.Window01);
    }

    protected override void OnButtonClose()
    {
        base.OnButtonClose();
        Dispatch(AreaCode.Line, 0, WindowType.Window01);
        //SetButtonsActive(true);
        //SetColliderActive(true);
    }
}
