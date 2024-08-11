﻿using System;
using System.Collections;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.Loading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(Load(name, onLoaded));

        private IEnumerator Load(string nextScene, Action onLoaded)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}