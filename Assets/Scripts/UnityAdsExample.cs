using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsExample : MonoBehaviour
{
	public static UnityAdsExample m_instance = null;

	void Awake ()
	{
		m_instance = this;
	}

	public void ShowAd ()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		}
	}
}