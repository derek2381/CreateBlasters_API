using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public TextMeshProUGUI endGameText;
    public GameObject titleScreen;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTarget(){
      while(isGameActive){
        yield return new WaitForSeconds(spawnRate);
        int index = Random.Range(0, targets.Count);
        Instantiate(targets[index]);
      }
    }

    public void UpdateScore(int scoreToAdd){
      score += scoreToAdd;
      scoreText.text = "Score: " + score;
    }


    public void GameOver(){
      restartButton.gameObject.SetActive(true);
      gameOverText.gameObject.SetActive(true);
      endGameText.text = "Score :- " + score;
      endGameText.gameObject.SetActive(true);
      scoreText.gameObject.SetActive(false);
      isGameActive = false;

      StartCoroutine(SendScoreToAPI(
        "self_rewards", "473993", "typeJk", "3947", "9387-423",
        "Derek", "SP101", "SphereSurviv", "0", "0", 0, 0,
        score, "image", "1:00 AM", "2:30 AM"));
    }

    public void RestartGame(){
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void StartGame(float difficulty){
      isGameActive = true;
      spawnRate /= difficulty;
      StartCoroutine(SpawnTarget());
      score = 0;
      UpdateScore(0);

      titleScreen.gameObject.SetActive(false);
    }

      IEnumerator SendScoreToAPI(
    string operation, string scMemberID, string memberType, string schoolID, string mobileNo,
    string userName, string gameID, string gameName, string exps, string level, int kills,
    int badges, int score, string img, string times, string times2)
{
    string url = "https://dev.smartcookie.in/core/webservice_game.php";
    WWWForm form = new WWWForm();

    form.AddField("operation", operation);
    form.AddField("SC_Member_ID", scMemberID);
    form.AddField("Member_type", memberType);
    form.AddField("School_id", schoolID);
    form.AddField("mobile_no", mobileNo);
    form.AddField("user_name", userName);
    form.AddField("game_id", gameID);
    form.AddField("game_name", gameName);
    form.AddField("exps", exps);
    form.AddField("level", level);
    form.AddField("kils", kills);
    form.AddField("badges", badges);
    form.AddField("score", score);
    form.AddField("img", img);
    form.AddField("times", times);
    form.AddField("times2", times2);

  Debug.Log("The Score is :- " + score);
    if(score > 0){
      Debug.Log("Estimation Reward:- " + score/10);
    }else{
      Debug.Log("Estimation Reward :- 0");
    }
    using (UnityWebRequest www = UnityWebRequest.Post(url, form))
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Debug.Log("Data submitted successfully " + www.downloadHandler.text);
        }
    }
}

}
