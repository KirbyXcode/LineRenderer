using UnityEngine;
using HedgehogTeam.EasyTouch;

public class Swipe : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isSwipe;
    private int id = -1;

    private Vector2 startPos;
    private Vector2 endPos;

    private void Start()
    {
        EasyTouch.On_SwipeStart += On_SwipeStart;
        //EasyTouch.On_Swipe += On_Swipe;
        EasyTouch.On_SwipeEnd += On_SwipeEnd;

        rb = GetComponent<Rigidbody2D>();
        Global.id = Random.Range(0, 10000);
        id = Global.id;
        isSwipe = true;
    }
	
	void OnDestroy()
    {
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent()
    {
		EasyTouch.On_SwipeStart -= On_SwipeStart;
		//EasyTouch.On_Swipe -= On_Swipe;
		EasyTouch.On_SwipeEnd -= On_SwipeEnd;	
	}
	

	// At the swipe beginning 
	private void On_SwipeStart( Gesture gesture)
    {
        //print(gesture.GetCurrentFirstPickedUIElement());

        if (gesture.GetCurrentFirstPickedUIElement() == gameObject)
        {
            startPos = gesture.startPosition;
        }

        if (isSwipe && id == Global.id)
        {
            
        }
    }

    //// During the swipe
    //private void On_Swipe(Gesture gesture)
    //   {
    //	// the world coordinate from touch for z=5
    //	Vector3 position = gesture.GetTouchToWorldPoint(5);
    //}

    // At the swipe end 
    private void On_SwipeEnd(Gesture gesture)
    {
        float angle = gesture.GetSwipeOrDragAngle();

        //print(gesture.GetCurrentPickedObject());

        if (gesture.GetCurrentFirstPickedUIElement() == gameObject)
        {
            //startPos = gesture.startPosition;
            endPos = gesture.position;

            Vector2 dir = endPos - startPos;

            rb.AddRelativeForce(dir.normalized * 1000);

            switch (transform.parent.name)
            {
                case "SigRoot01":
                    MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG01_PANEL, false);
                    break;
                case "SigRoot02":
                    MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG02_PANEL, false);
                    break;
                case "SigRoot03":
                    MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG03_PANEL, false);
                    break;
                case "SigRoot04":
                    MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG04_PANEL, false);
                    break;
                case "SigRoot05":
                    MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG05_PANEL, false);
                    break;
                case "SigRoot06":
                    MsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.SIG06_PANEL, false);
                    break;
            }
        }

        //if (isSwipe && angle >= 15 && angle <= 165 && id == Global.id) 
        //{

        //    isSwipe = false;
        //}

        // Get the swipe angle
        //float angles = gesture.GetSwipeOrDragAngle();
        //swipeText.text = "Last swipe : " + gesture.swipe.ToString() + " /  vector : " + gesture.swipeVector.normalized + " / angle : " + angles.ToString("f2");
    }
}
