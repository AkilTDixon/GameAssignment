using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageableEnvironment : MonoBehaviour
{
    public bool SecretStash = false;
    public GameObject AssociatedBountyCount;
    public void TakeDamage(int damage, string player)
    {
        if (GetComponent<ExplodeBarrel>() != null)
            GetComponent<ExplodeBarrel>().ObjectShot(player);

        if (SecretStash)
        {
            GameObject obj = Instantiate(AssociatedBountyCount, transform.position + (new Vector3(0, 1f, 0)), AssociatedBountyCount.transform.rotation);
            obj.GetComponentInChildren<TextMeshPro>().text = "+10";
            Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseWitchBounty(10, player);
            Destroy(gameObject);
        }
    }
}
