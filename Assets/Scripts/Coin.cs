using UnityEngine;

public class Coin : MonoBehaviour {
    public GameObject increaseText, particle;
    public bool showText, showParticle;
    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            Destroy (gameObject, 0.45f);
            GameManager.Instance.coinManager.UpdateCoin (1);
            GameManager.Instance.tapticManager.Impact (HapticTypes.MediumImpact);
            GetComponent<Animator> ().enabled = true;
            GetComponent<Animator> ().Play ("CoinPickUp");
            GetComponent<BoxCollider> ().enabled = false;
            if (showText)
                Instantiate (increaseText, transform.position, Quaternion.identity);
            if (showParticle) {
                Instantiate (particle, transform.position, Quaternion.identity);
            }
        }
    }
}