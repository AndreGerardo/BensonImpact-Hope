using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUIResize : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private int animId = -1;

    [Header("General Config")]
    [SerializeField]
    private float scaleFactor = 1.1f;

    private Vector3 tempScale;

    private bool canTransition = true;

    private bool isCursorIn = false;

    private void Awake()
    {
        tempScale = GetComponent<RectTransform>().transform.localScale;
    }

    //private void Update()
    //{
    //    if (canTransition == false)
    //    {
    //        return;
    //    }


    //    if (isCursorIn)
    //    {
    //        LeanTween.scale(GetComponent<RectTransform>(), tempScale * scaleFactor, 0.15f)
    //        .setEaseInOutSine()
    //        .setOnComplete(() => canTransition = true);
    //    }
    //    else
    //    {
    //        LeanTween.scale(GetComponent<RectTransform>(), tempScale, 0.15f)
    //            .setEaseInOutSine()
    //            .setOnComplete(() => canTransition = true);
    //    }

    //    canTransition = false;
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        isCursorIn = false;

        //if (canTransition == false)
        //{
        //    return;
        //}

        //canTransition = false;

        LeanTween.cancel(animId);
        animId = LeanTween.scale(GetComponent<RectTransform>(), tempScale, 0.15f)
            .setEaseInOutSine()
            .setOnComplete(() => canTransition = true).id;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isCursorIn = true;

        //if (canTransition == false)
        //{
        //    return;
        //}

        //canTransition = false;
        if (animId != -1) LeanTween.cancel(animId);
        animId = LeanTween.scale(GetComponent<RectTransform>(), tempScale * scaleFactor, 0.15f)
            .setEaseInOutSine()
            .setOnComplete(() => canTransition = true).id;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isCursorIn = false;

        //if (canTransition == false)
        //{
        //    return;
        //}

        //canTransition = false;
        LeanTween.cancel(animId);
        animId = LeanTween.scale(GetComponent<RectTransform>(), tempScale, 0.15f)
            .setEaseInOutSine()
            .setOnComplete(() => canTransition = true).id;
    }


}
