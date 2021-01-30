using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class PlayerHealth : MonoBehaviour
{
    public int Health, maxHealth;
    public float InviTime;
    bool invincible;
    Volume PPVol;
    Vignette vignette;
    public TMP_Text HealthText;

    public GameObject PlayerModel, GameOverPanel;
    bool gameOver;

    private void Start()
    {
        maxHealth = Health;

        PPVol = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Volume>();
        VolumeProfile profile = PPVol.sharedProfile;
        PPVol.profile.TryGet(out vignette);

    }

    public void TakeDamage(int pDmg)
    {
        vignette.color.value = Color.red;

        if (!invincible)
        {
        MasterAudio.PlaySound("PlayerHit1");
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.5f, 0.2f));
        seq.Append(DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 1f));

        Health -= pDmg;

        invincible = true;
        StartCoroutine(HitCD());
        }

        if (Health <= 0)
        {
            Health = 0;
            GameOver();
        }
    }

    IEnumerator HitCD()
    {
        yield return new WaitForSeconds(InviTime);
        invincible = false;
    }

    void GameOver()
    {
        MasterAudio.PlaySound("PlayerDeath1");
        MasterAudio.PlaySound("loser");
        PlayerModel.SetActive(false);
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        gameOver = true;
    }

    private void Update()
    {
        HealthText.text = "Health: " + Health + "/" + maxHealth;
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

    }
}
