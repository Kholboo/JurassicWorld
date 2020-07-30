using System.Collections;
using UnityEngine;

public class UICoin : MonoBehaviour
{
    SpreadCollectable spreadCollectable;
    bool isSpread = true;
    Vector3 euler, spreadPositon;
    float randomDelay, time;
    int repeatTime;
    Animator animator;

    void Start()
    {
        spreadCollectable = GetComponentInParent<SpreadCollectable>();
        repeatTime = 0;
        time = 0;
        euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        randomDelay = Random.Range(0.8f, 1f);
        animator = spreadCollectable.targetObject.GetComponent<Animator>();
        transform.eulerAngles = euler;
        transform.localScale = new Vector3(spreadCollectable.startSize, spreadCollectable.startSize, spreadCollectable.startSize);
        StartCoroutine("WaitForSend");
    }

    void LateUpdate()
    {
        if (isSpread)
        {
            Spread();
        }
        if (!isSpread)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, spreadCollectable.speed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, spreadCollectable.speed * Time.deltaTime);
            if (transform.localScale.x >= 0)
            {
                transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            }
        }
    }
    public void Spread()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, spreadPositon, spreadCollectable.spreadSpeed * Time.deltaTime);
    }
    IEnumerator WaitForSend()
    {
        Destroy(gameObject, 2.5f);
        transform.SetParent(spreadCollectable.transform);
        spreadPositon = new Vector3(transform.localPosition.x + Random.Range(-spreadCollectable.spreadRangeX, spreadCollectable.spreadRangeX), transform.localPosition.y + Random.Range(-spreadCollectable.spreadRangeY, spreadCollectable.spreadRangeY), transform.localPosition.z);
        yield return new WaitForSeconds(randomDelay);
        transform.SetParent(spreadCollectable.targetObject.transform);
        isSpread = false;
        yield return new WaitForSeconds(randomDelay);
        animator.Play("CollectableScale");
        GameManager.Instance.tapticManager.Impact(HapticTypes.HeavyImpact);
    }
}