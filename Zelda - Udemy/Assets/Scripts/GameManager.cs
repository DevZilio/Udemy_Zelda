using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public enum enemyState
{
    IDLE,
    ALERT,
    PATROL,
    FOLLOW,
    EXPLORE,
    FURY,
    ATTACK,
    DEAD
}


public class GameManager : MonoBehaviour
{
    [Header("Slime IA")]
    public float slimeIdleWaitTime;
    public Transform[] slimeWayPoints;
    public float slimeDistanceToAttack = 2.25f;
    public float slimeAlertTime = 3f;
    public float slimeAttackDelay = 1f;
    public float slimeLookAtSpeed = 1f;
    public float slimeTimeFollowLimit = 2f;

    [Header("Player")]
    public Transform player;

    [Header("Rain manager")]
    public PostProcessVolume postB;
    public ParticleSystem rainParticle;
    public int rainRateOverTime;
    public int rainIncrement;
    public float rainIncrementDelay;
    private ParticleSystem.EmissionModule _rainModule;

    private void Start() {
        _rainModule = rainParticle.emission;
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
}
