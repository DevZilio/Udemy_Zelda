using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;

public enum enemyState
{
    IDLE,
    ALERT,
    PATROL,
    FOLLOW,
    EXPLORE,
    FURY,
    ATTACK,
    SHIELD,
    DEAD
}

public enum GameState
{
    GAMEPLAY,
    GAMEOVER,
}


public class GameManager : MonoBehaviour
{
    [Header("Game State Manager")]
    public GameState gameState;
    
    
    [Header("Slime AI")]
    public float slimeIdleWaitTime;
    public Transform[] slimeWayPoints;
    public float slimeDistanceToAttack = 2.25f;
    public float slimeAlertTime = 3f;
    public float slimeAttackDelay = 1f;
    public float slimeLookAtSpeed = 1f;
    public float slimeTimeFollowLimit = 2f;

    [Header("Turtle AI")]
    public float turtleIdleWaitTime;
    public Transform[] turtleWayPoints;
    public float turtleDistanceToAttack = 2.25f;
    public float turtleAlertTime = 3f;
    public float turtleAttackDelay = 1f;
    public float turtleLookAtSpeed = 1f;
    public float turtleTimeFollowLimit = 2f;
    public float turtleTimeShield = 3f;


    [Header("UI")]
    public TextMeshProUGUI textGemsUI;


    [Header("Player")]
    public Transform player;
    public int gems;

    [Header("Drop Item")]
    public GameObject gemPrefab;
    public int percDrop = 25;





    [Header("Rain manager")]
    public PostProcessVolume postB;
    public ParticleSystem rainParticle;
    public int rainRateOverTime;
    public int rainIncrement;
    public float rainIncrementDelay;
    private ParticleSystem.EmissionModule _rainModule;

    private void Start() {
        _rainModule = rainParticle.emission;

        textGemsUI.text = gems.ToString();
    }


  public void OnOffRain(bool isRain)
  {
      StopCoroutine(RainManager(isRain));
      StartCoroutine(RainManager(isRain));
      StopCoroutine(PostBManager(isRain));
      StartCoroutine(PostBManager(isRain));
  }

  IEnumerator RainManager(bool isRain)
  {
      switch(isRain)
      {
          case true: //Aumentar a chuva

          for(float r = _rainModule.rateOverTime.constant; r < rainRateOverTime; r += rainIncrement)
          {
              _rainModule.rateOverTime = r;
              yield return new WaitForSeconds(rainIncrementDelay);
          }

          _rainModule.rateOverTime = rainRateOverTime;
          break;

          case false: //Diminui a chuva
          for(float r = _rainModule.rateOverTime.constant; r > 0; r -= rainIncrement)
          {
              _rainModule.rateOverTime = r;
              yield return new WaitForSeconds(rainIncrementDelay);
          }

          _rainModule.rateOverTime = 0;

          break;
      }
  }

  IEnumerator PostBManager(bool isRain)
  {
      switch(isRain)
      {
          case true:
            for(float w = postB.weight; w < 1; w += 1 * Time.deltaTime)
            {
                postB.weight = w;
                yield return new WaitForEndOfFrame();
            }
          break;

          case false:
             for(float w = postB.weight; w > 0; w -= 1 *Time.deltaTime)
            {
                postB.weight = w;
                yield return new WaitForEndOfFrame();
            }

            postB.weight = 0;
          break;
      }
  }

  public void ChangeGameState(GameState newState)
  {
      gameState = newState;
  }

  public void SetGems(int amount)
  {
      gems += amount;
      textGemsUI.text = gems.ToString();
  }

  public bool Perc(int p)
  {
      int temp = Random.Range(0,100);
      bool retorno = temp <= p ? true : false;
      return retorno;
  }
}
