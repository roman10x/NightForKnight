using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField]
    private  CanvasGroup m_curtain;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      m_curtain.alpha = 1;
    }
    
    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (m_curtain.alpha > 0)
      {
        m_curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
      
      gameObject.SetActive(false);
    }
  }
}