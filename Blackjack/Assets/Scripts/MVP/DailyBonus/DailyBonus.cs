using UnityEngine;
using UnityEngine.UI;

public class DailyBonus : MonoBehaviour
{
    public int Day => day;
    public Transform TransformBonus => imageBonus.transform;

    [SerializeField] private int day;
    [SerializeField] private GameObject objectClaim;
    [SerializeField] private Image imageBonus;
    [SerializeField] private Sprite spriteCloseBonus;
    [SerializeField] private Sprite spriteOpenBonus;
    [SerializeField] private ScaleEffect scaleEffect;

    public void AlreadyClaim()
    {
        Debug.Log($"DAY {day}: ALREADY");

        scaleEffect.DeactivateEffect();
        objectClaim.SetActive(true);
        imageBonus.sprite = spriteOpenBonus;
    }

    public void CurrentClaim()
    {
        Debug.Log($"DAY {day}: CURRENT");

        scaleEffect.ActivateEffect();
        objectClaim.SetActive(false);
        imageBonus.sprite = spriteOpenBonus;
    }

    public void CloseClaim()
    {
        Debug.Log($"DAY {day}: CLOSE");

        scaleEffect.DeactivateEffect();
        objectClaim.SetActive(false);
        imageBonus.sprite = spriteCloseBonus;
    }
}
