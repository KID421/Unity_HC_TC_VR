using UnityEngine;
using System.Collections;

// 要求元件(類型(元件類型))，再套用腳本的時候才會觸發
[RequireComponent(typeof(AudioSource))]
public class RotateObject : MonoBehaviour
{
    [Header("旋轉角度"), Range(0, 150)]
    public float angle = 90;
    [Header("旋轉速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("音效")]
    public AudioClip sound;
    [Header("音量"), Range(0, 5)]
    public float volume = 1;

    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 開始旋轉
    /// </summary>
    public void StartRotate()
    {
        StartCoroutine(Rotate());
    }

    /// <summary>
    /// 旋轉
    /// </summary>
    private IEnumerator Rotate()
    {
        aud.PlayOneShot(sound, volume);
        GetComponent<Collider>().enabled = false;       // 關閉碰撞器

        // 當 角度 不等於 指定的旋轉角度
        while (transform.rotation != Quaternion.Euler(0, angle, 0))
        {
            // 角度的插值
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), speed * Time.deltaTime);
            yield return null;
        }
    }
}
