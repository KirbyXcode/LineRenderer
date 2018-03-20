using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISignature : UIBase
{
    private Button mButton_OK;
    private Button mButton_Close;
    private Button mButton_Clear;
    private Transform window;
    private GameObject sigPrefab;
    protected Transform sigRootTrans;
    protected GameObject planeGo;

    private RenderTexture render;
    private Texture2D texture;
    protected Sprite sprite;
    private const int width = 965;
    private const int height = 483;

    protected virtual void Start()
    {
        mButton_OK = Global.FindChild<Button>(transform, "OKButton");
        mButton_Close = Global.FindChild<Button>(transform, "CloseButton");
        mButton_Clear = Global.FindChild<Button>(transform, "ClearButton");
        window = transform.Find("Window");
        sigPrefab = Resources.Load<GameObject>("Sig");

        InitListener();
        InitWindow();
    }

    private void InitListener()
    {
        mButton_OK.onClick.AddListener(OnButtonOK);
        mButton_Close.onClick.AddListener(OnButtonClose);
        mButton_Clear.onClick.AddListener(OnButtonClear);
    }

    private void InitWindow()
    {
        window.localScale = new Vector3(0, 1, 1);
        SetPanelActive(false);
    }

    protected virtual void OnButtonOK()
    {
        
    }

    protected virtual void OnButtonClose()
    {
        ExitAnim();
    }

    protected virtual void OnButtonClear()
    {
        
    }

    protected void EnterAnim()
    {
        SetPanelActive(true);
        window.DOScaleX(1, 0.4f);
    }

    protected void ExitAnim()
    {
        window.DOScaleX(0, 0.4f).OnComplete(() => SetPanelActive(false));
    }

    protected void CreateSig(Sprite sprite)
    {
        GameObject sigGo = Instantiate(sigPrefab);
        sigGo.transform.SetParent(sigRootTrans, false);
        sigGo.GetComponent<Image>().sprite = sprite;

        //SigUpAnim(sigGo);
    }

    protected void SigUpAnim(GameObject go)
    {
        go.transform.DOLocalMoveY(585, 0.4f);
        SetColliderActive(false);
    }

    protected Sprite ScreenShot(float offsetX, float offsetY)
    {
        render = new RenderTexture(Screen.width, Screen.height, 1);
        Camera.main.targetTexture = render;
        Camera.main.Render();
        RenderTexture.active = render;

        texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(offsetX, offsetY, width, height), 0, 0, false);

        //texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        texture.Apply();

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), Vector2.zero);

        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        GameObject.Destroy(render);

        return sprite;
    }

    protected void SetColliderActive(bool active)
    {
        planeGo.SetActive(active);
    }

    protected void SetButtonsActive(bool active)
    {
        mButton_OK.interactable = active;
        mButton_Close.interactable = active;
        mButton_Clear.interactable = active;
    }
}
