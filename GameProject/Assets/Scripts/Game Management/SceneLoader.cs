﻿using UnityEngine.SceneManagement;
public class SceneLoader: A {
public void LoadSceneByName(string SceneName) {
if (SceneName != null && SceneName != "") SceneManager.LoadScene(SceneName);}}