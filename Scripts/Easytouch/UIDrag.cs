using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrag : MonoBehaviour 
{
    //private bool isPushDown;
    private bool isDrag;

    public void EndDrag()
    {
        if (isDrag) return;

        switch (transform.parent.name)
        {
            case "SigRoot01":
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG01_PANEL, true);
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.PLUS_ACTIVE, WindowType.Window01);
                isDrag = true;
                break;
            case "SigRoot02":
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG02_PANEL, true);
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.PLUS_ACTIVE, WindowType.Window02);
                isDrag = true;
                break;
            case "SigRoot03":
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG03_PANEL, true);
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.PLUS_ACTIVE, WindowType.Window03);
                isDrag = true;
                break;
            case "SigRoot04":
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG04_PANEL, true);
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.PLUS_ACTIVE, WindowType.Window04);
                isDrag = true;
                break;
            case "SigRoot05":
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG05_PANEL, true);
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.PLUS_ACTIVE, WindowType.Window05);
                isDrag = true;
                break;
            case "SigRoot06":
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG06_PANEL, true);
                MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.PLUS_ACTIVE, WindowType.Window06);
                isDrag = true;
                break;
        }
    }
    
}
