using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : ManagerBase
{
    public static LineManager Instance;

    //private Dictionary<int, List<Vector3>> positionsDict = null;
    private Dictionary<int, Line> lineDict = null;

    private GameObject linePrefab;
    private Touch[] touches;

    private Color color = Color.white;
    private float width = 0.05f;
    private int numCornerVertices = 10;
    private int numCapVertices = 10;

    private float lineDistance = 0.02f;

    private Line currentLine;

    private Transform[] spawnsTrans;
    private Transform[] sigRootTrans;

    private void Awake()
    {
        Instance = this;
        Add(0, this);
    }

    private void Start()
    {
        //positionsDict = new Dictionary<int, List<Vector3>>();
        lineDict = new Dictionary<int, Line>();

        linePrefab = Resources.Load<GameObject>("Line");

        Transform rootTrans = GameObject.Find("_LineRoot").transform;
        spawnsTrans = new Transform[rootTrans.childCount];
        for (int i = 0; i < rootTrans.childCount; i++)
        {
            spawnsTrans[i] = rootTrans.GetChild(i);
        }

        sigRootTrans = new Transform[6];
        sigRootTrans[0] = GameObject.Find("Canvas/SigRoot01").transform;
        sigRootTrans[1] = GameObject.Find("Canvas/SigRoot02").transform;
        sigRootTrans[2] = GameObject.Find("Canvas/SigRoot03").transform;
        sigRootTrans[3] = GameObject.Find("Canvas/SigRoot04").transform;
        sigRootTrans[4] = GameObject.Find("Canvas/SigRoot05").transform;
        sigRootTrans[5] = GameObject.Find("Canvas/SigRoot06").transform;
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case 0:
                WindowType type = (WindowType)message;
                Clear(type);
                break;
        }
    }

    private void Update()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
            { 
                Ray ray = GetMouseRay();
                currentLine = SetParent(ray);

                if (currentLine == null) return;

                currentLine.Init(width, width, color, color, numCornerVertices, numCapVertices);

                Vector3 position = GetMousePoint();

                if (position == Vector3.zero) return;

                position.z -= lineDistance;

                currentLine.AddPosition(position);
                List<Vector3> positions = currentLine.GetPositions();
                //positionsDict.Add(fingerId, positions);

                currentLine.SetProperty(positions.Count, positions.ToArray());
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 position = GetMousePoint();

                if (position == Vector3.zero) return;

                position.z -= lineDistance;

                if (currentLine == null) return;

                List<Vector3> positions = currentLine.GetPositions();
                positions.Add(position);

                currentLine.SetProperty(positions.Count, positions.ToArray());
            }
        }

        if (Input.touchCount > 0) 
        {
            touches = Input.touches;

            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Began)
                {
                    int fingerId = touches[i].fingerId;
                    //GameObject go = Instantiate(linePrefab);
                    //go.transform.SetParent(this.transform);
                    Ray ray = GetTouchRay(touches[i]);
                    Line line = SetParent(ray);

                    if (line == null) return;

                    line.Init(width, width, color, color, numCornerVertices, numCapVertices);
                    lineDict.Add(fingerId, line);

                    Vector3 position = GetTouchPoint(touches[i]);

                    if (position == Vector3.zero) return;

                    position.z -= lineDistance;

                    line.AddPosition(position);
                    List<Vector3> positions = line.GetPositions();
                    //positionsDict.Add(fingerId, positions);

                    lineDict[fingerId].SetProperty(positions.Count, positions.ToArray());
                }
            }

            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Moved)
                {
                    int fingerId = touches[i].fingerId;
                    Vector3 position = GetTouchPoint(touches[i]);

                    if (position == Vector3.zero) return;

                    position.z -= lineDistance;

                    Line line = lineDict[fingerId];

                    if (line == null) return;

                    List<Vector3> positions = line.GetPositions();
                    positions.Add(position);

                    lineDict[fingerId].SetProperty(positions.Count, positions.ToArray());
                }
            }

            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Ended)
                {
                    int fingerId = touches[i].fingerId;
                    //positionsDict.Remove(fingerId);
                    lineDict.Remove(fingerId);
                }
            }
        }
    }

    private Ray GetTouchRay(Touch touch)
    {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(touch.position);
        return ray;
    }

    private Ray GetMouseRay()
    {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray;
    }

    private Line SetParent(Ray ray)
    {
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit);
        if (isCollider)
        {
            GameObject go = Instantiate(linePrefab);
            switch (hit.transform.name)
            {
                case "Plane01":
                    go.transform.SetParent(spawnsTrans[0]);
                    break;
                case "Plane02":
                    go.transform.SetParent(spawnsTrans[1]);
                    break;
                case "Plane03":
                    go.transform.SetParent(spawnsTrans[2]);
                    break;
                case "Plane04":
                    go.transform.SetParent(spawnsTrans[3]);
                    break;
                case "Plane05":
                    go.transform.SetParent(spawnsTrans[4]);
                    break;
                case "Plane06":
                    go.transform.SetParent(spawnsTrans[5]);
                    break;
            }
            return go.GetComponent<Line>();
        }
        return null;
    }

    private Vector3 GetTouchPoint(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit);
        if(isCollider)
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public bool IsPainting(WindowType type)
    {
        switch (type)
        {
            case WindowType.Window01:
                if (spawnsTrans[0].childCount > 0)
                {
                    return true;
                }
                return false;
            case WindowType.Window02:
                if (spawnsTrans[1].childCount > 0)
                {
                    return true;
                }
                return false;
            case WindowType.Window03:
                if (spawnsTrans[2].childCount > 0)
                {
                    return true;
                }
                return false;
            case WindowType.Window04:
                if (spawnsTrans[3].childCount > 0)
                {
                    return true;
                }
                return false;
            case WindowType.Window05:
                if (spawnsTrans[4].childCount > 0)
                {
                    return true;
                }
                return false;
            case WindowType.Window06:
                if (spawnsTrans[5].childCount > 0)
                {
                    return true;
                }
                return false;
        }
        return true;
    }

    private Vector3 GetMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit);
        if (isCollider)
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Clear(WindowType type)
    {
        switch (type)
        {
            case WindowType.Window01:
                ClearLines(spawnsTrans[0]);
                break;
            case WindowType.Window02:
                ClearLines(spawnsTrans[1]);
                break;
            case WindowType.Window03:
                ClearLines(spawnsTrans[2]);
                break;
            case WindowType.Window04:
                ClearLines(spawnsTrans[3]);
                break;
            case WindowType.Window05:
                ClearLines(spawnsTrans[4]);
                break;
            case WindowType.Window06:
                ClearLines(spawnsTrans[5]);
                break;
        }
    }

    public void ClearAll()
    {
        for (int i = 0; i < sigRootTrans.Length; i++)
        {
            for (int j = 0; j < sigRootTrans[i].childCount; j++)
            {
                Destroy(sigRootTrans[i].GetChild(j).gameObject);
            }
        }
    }

    private void ClearLines(Transform spawnTrans)
    {
        for (int i = 0; i < spawnTrans.childCount; i++)
        {
            Destroy(spawnTrans.GetChild(i).gameObject);
        }
    }

    #region onvaluechanged method
    //public void OnRedColorChanged(bool isOn ){
    //	if (isOn) {
    //		paintColor = Color.red;
    //	}
    //}
    //public void OnGreenColorChanged(bool isOn){
    //	if (isOn) {
    //		paintColor = Color.green;
    //	}
    //}
    //public void OnBlueColorChanged(bool isOn){
    //	if (isOn) {
    //		paintColor = Color.blue;
    //	}
    //}
    public void OnPoint1Changed(bool isOn)
    {
        if (isOn)
        {
            width = 0.1f;
        }
    }
    public void OnPoint2Changed(bool isOn)
    {
        if (isOn)
        {
            width = 0.2f;
        }
    }
    public void OnPoint4Changed(bool isOn)
    {
        if (isOn)
        {
            width = 0.4f;
        }
    }

    #endregion
}
