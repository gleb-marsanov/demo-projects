using System.Collections;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator load);
    void StopAllCoroutines();
  }
}