using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Unity.Mathematics;

public class TrickRamp : MonoBehaviour
{
    public float jumpForce = 35f;
    public float trickDurationInSeconds = 5f;
    public GameObject LandingLocation;

    private bool shouldCatch = false;
    private GameObject playerWithNavMesh;
    private GameObject TrickingPlayer;
    private GameObject followCam;
    private GameObject mainHud;

    public void Awake()
    {
        followCam = GameObject.FindGameObjectWithTag("FollowCam");
        playerWithNavMesh = GameObject.FindGameObjectWithTag("NavMeshCharacter");
        mainHud = GameObject.FindGameObjectWithTag("HUD");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TrickingPlayer = other.gameObject;
            // We're using rigidBodies to launch them up using gravity, a fun effect. Cooler looking than just translating them up and down
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().mass = 1f;
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            GameManager.Instance.CancelBlaster(); // Ends blaster powerup so player doesn't shoot mid jump
            StartCoroutine(trickingRoutine());
        }
    }

    public IEnumerator trickingRoutine()
    {
        GameObject trickCam = TrickingPlayer.transform.Find("TrickCam").gameObject;
        GameObject trickGame = mainHud.transform.Find("QTE_Tricks").gameObject;
        
        yield return new WaitForSeconds(0.5f); // Wait half a second before camera transition
        TrickingPlayer.transform.parent.GetComponent<RealPlayerController>().canMove = false;
        trickGame.GetComponentInChildren<TrickingMinigame>().player = TrickingPlayer ;
        followCam.SetActive(false);
        trickCam.SetActive(true);
        trickGame.SetActive(true);
        playerWithNavMesh.GetComponent<PlayerController>().targetTransform = LandingLocation.transform; // As camera is away transition the player nav mesh to the landing location
        
        yield return new WaitForSeconds(trickDurationInSeconds); // wait until trick sequence is done

        GameObject endTransform = GameObject.FindGameObjectWithTag("End");
        playerWithNavMesh.GetComponent<PlayerController>().targetTransform = endTransform.transform; //set the player to return back to moving forward
        
        yield return new WaitForSeconds(1f);
        
        trickGame.SetActive(false);
        trickCam.SetActive(false); //cameras go back to normal
        followCam.SetActive(true);
        shouldCatch = true;

        yield return true;
    }

    public void FixedUpdate()
    {
        if(shouldCatch)
        CatchPlayer(TrickingPlayer);
    }

    public void CatchPlayer(GameObject trickingPlayer)
    {
        if (trickingPlayer.gameObject.transform.localPosition.y <= -3.5f)
        {
            shouldCatch = false;
            trickingPlayer.gameObject.transform.localPosition = new Vector3(0, -3.5f, 0); //These are the exact local coordinates the player needs to rotate well on half pipe pivot
            trickingPlayer.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            trickingPlayer.gameObject.GetComponent<Rigidbody>().useGravity = false;
            trickingPlayer.gameObject.GetComponent<Rigidbody>().mass = 0.1f;
            trickingPlayer.transform.localRotation = Quaternion.Euler(0,90,0) ;
            TrickingPlayer.transform.parent.GetComponent<RealPlayerController>().canMove = true;




        }
    }


}
