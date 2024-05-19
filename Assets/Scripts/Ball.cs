using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject splashImg;
    [SerializeField] private float jumpForce;
    private GameManager gm;
    private Ring ring;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }


    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        rb.AddForce(Vector3.up * jumpForce);
        AudioManager.Instance.PlaySFX("Jump");
        GameObject splash = Instantiate(splashImg, transform.position - new Vector3(0, 0.22f, 0f), transform.rotation);
        splash.transform.SetParent(other.gameObject.transform);
        Destroy(splash, 1);

        string metarialName = other.gameObject.GetComponent<MeshRenderer>().material.name;
        Debug.Log(metarialName);
        if (metarialName == "Unsafe Color (Instance)")
        {
            AudioManager.Instance.PlaySFX("GameOver");
            gm.GameOver();
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
        else if (metarialName == "Last Ring (Instance)")
        {
            AudioManager.Instance.PlaySFX("MissionComplete");
            gm.NextLevel();
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
