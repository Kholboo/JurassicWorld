using System.Collections;
using UnityEngine;

public class UICoin : MonoBehaviour {
    bool isSpread = true;
    Vector3 euler, spreadPositon;
    float randomDelay, time;
    int repeatTime;
    Animator animator;

    void Start () {
        repeatTime = 0;
        time = 0;
        euler = transform.eulerAngles;
        euler.z = Random.Range (0f, 360f);
        randomDelay = Random.Range (0.8f, 1f);
        animator = SpreadCollectable.instance.targetObject.GetComponent<Animator> ();
        transform.eulerAngles = euler;
        transform.localScale = new Vector3 (SpreadCollectable.instance.startSize, SpreadCollectable.instance.startSize, SpreadCollectable.instance.startSize);
        StartCoroutine ("WaitForSend");
    }

    void LateUpdate () {
        if (isSpread) {
            Spread ();
        }
        if (!isSpread) {
            transform.localPosition = Vector3.Lerp (transform.localPosition, Vector3.zero, SpreadCollectable.instance.speed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.identity, SpreadCollectable.instance.speed * Time.deltaTime);
            if (transform.localScale.x >= 0) {
                transform.localScale -= new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);
            }
        }
    }
    public void Spread () {
        transform.localPosition = Vector3.Lerp (transform.localPosition, spreadPositon, SpreadCollectable.instance.spreadSpeed * Time.deltaTime);
    }
    IEnumerator WaitForSend () {
        Destroy (gameObject, 2.5f);
        transform.SetParent (SpreadCollectable.instance.transform);
        spreadPositon = new Vector3 (transform.localPosition.x + Random.Range (-SpreadCollectable.instance.spreadRangeX, SpreadCollectable.instance.spreadRangeX), transform.localPosition.y + Random.Range (-SpreadCollectable.instance.spreadRangeY, SpreadCollectable.instance.spreadRangeY), transform.localPosition.z);
        yield return new WaitForSeconds (randomDelay);
        transform.SetParent (SpreadCollectable.instance.targetObject.transform);
        isSpread = false;
        yield return new WaitForSeconds (randomDelay);
        animator.Play ("CollectableScale");
        GameManager.Instance.tapticManager.Impact (HapticTypes.HeavyImpact);

    }
}