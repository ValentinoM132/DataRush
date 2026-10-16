using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class TrickRamp : MonoBehaviour
{
    public float jumpForce = 35f;
    public GameObject followCam;
    public GameObject trickCam;
    public float trickDurationInSeconds = 5f;
    public GameObject playerWithNavMesh;
    public GameObject transformForNavMesh;
    public Vector3 targetPosition; // The destination coordinates
    public float duration = 1.0f;   // How long the movement takes in seconds
    public GameObject playerWithANavMesh;

    public Vector3 startPosition;
    public float elapsedTime;
    public bool shouldLerp = false;
    public bool shouldCatch = false;

    private void OnTriggerEnter(Collider other)
    {
        
        other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        other.gameObject.GetComponent<Rigidbody>().mass = 1f;
        other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        StartCoroutine(trickingRoutine(other.gameObject));

    }

    public IEnumerator trickingRoutine(GameObject PlayerNoNavMesh)
    {
        playerWithANavMesh = PlayerNoNavMesh;
        yield return new WaitForSeconds(0.5f);
        followCam.SetActive(false);
        trickCam.SetActive(true);
        playerWithNavMesh.GetComponent<PlayerController>().targetTransform = transformForNavMesh.transform;
        yield return new WaitForSeconds(trickDurationInSeconds);
        GameObject endTransform = GameObject.FindGameObjectWithTag("End");
        playerWithNavMesh.GetComponent<PlayerController>().targetTransform = endTransform.transform;
        yield return new WaitForSeconds(1f);
        trickCam.SetActive(false);
        followCam.SetActive(true);
        shouldCatch = true;
        yield return new WaitForSeconds(2f);
       







    }

    public void FixedUpdate()
    {
        if(shouldCatch)
        CatchPlayer(playerWithANavMesh);
    }

    public void CatchPlayer(GameObject PlayerNoNavMesh)
    {
        if (PlayerNoNavMesh.gameObject.transform.localPosition.y <= -3.5f)
        {
            shouldCatch = false;
            Debug.Log(PlayerNoNavMesh.gameObject.transform.localPosition);
            PlayerNoNavMesh.gameObject.transform.localPosition = new Vector3(0, -3.5f, 0);

            PlayerNoNavMesh.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            PlayerNoNavMesh.gameObject.GetComponent<Rigidbody>().useGravity = false;
            PlayerNoNavMesh.gameObject.GetComponent<Rigidbody>().mass = 0.1f;
            



        }
    }


}
