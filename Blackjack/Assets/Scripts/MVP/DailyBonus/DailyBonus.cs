using UnityEngine;

public class DailyBonus : MonoBehaviour
{
    public int Day => day;

    [SerializeField] private int day;
    [SerializeField] private GameObject objectClaim;
    [SerializeField] private GameObject objectNotClaim;

    public void AlreadyClaim()
    {
        objectClaim.SetActive(true);
        objectNotClaim.SetActive(false);
    }

    public void CurrentClaim()
    {
        objectClaim.SetActive(false);
        objectNotClaim.SetActive(false);
    }

    public void CloseClaim()
    {
        objectClaim.SetActive(false);
        objectNotClaim.SetActive(true);
    }
}
