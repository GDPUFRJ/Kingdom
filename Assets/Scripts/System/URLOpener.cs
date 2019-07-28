using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOpener : MonoBehaviour
{
    public void OpenPrivacyPolicy()
    {
        Application.OpenURL(KingdomULRs.PRIVACY_POLICY);
    }
}
