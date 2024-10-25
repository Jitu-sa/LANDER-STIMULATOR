using UnityEngine.SceneManagement;
using UnityEngine;

public class collisonhandeller : MonoBehaviour
{
    public AudioClip crashsound;
    public AudioClip sucesssound;
    public ParticleSystem crashparticle;
    public ParticleSystem sucessparticle;

    AudioSource audioSource;

    bool istransitioning=false;
    bool iscollideron=false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        cheatcode();
    }

    void cheatcode()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            loadnextlevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            iscollideron = !iscollideron;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (istransitioning || iscollideron){return;}

        switch (other.gameObject.tag)
        {
            case "start":
                Debug.Log("game start");
                break;

            case "Finish":
                sucess();
                break;

            case "fuel":
                Debug.Log("hurray fuel up");
                break;

            default:
                crashed();
                break;
        }
    }

    void crashed()
    {
        istransitioning = true;
        GetComponent<playerMovement>().enabled = false;
        audioSource.PlayOneShot(crashsound);
        crashparticle.Play();
        Invoke("reloadscene", 2f);
    }

    void sucess()
    {
        istransitioning=true;
        GetComponent<playerMovement>().enabled = false;
        audioSource.PlayOneShot(sucesssound);
        sucessparticle.Play();
        Invoke("loadnextlevel",2f);
    }

    void loadnextlevel()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        int nextlevelindex = currentsceneindex + 1;
        int totalscene = SceneManager.sceneCountInBuildSettings;
        if (nextlevelindex==totalscene)
        {
            nextlevelindex = 0;
        }
        SceneManager.LoadScene(nextlevelindex);
    }
    void reloadscene()
    {
        int currentsceneindex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex);
    }
}
