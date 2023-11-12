namespace TestGame
{
    using NUnit.Framework;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UnityEngine.UI;

    public class PlayModeTestGame
    {
        bool clicked = false;
        [SetUp]
        public void SetUP()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }

        [UnityTest]
        public IEnumerator TestMenu()
        {
            var gameObject = new GameObject();
            string name = "StartButton";

            GameObject startButton = GameObject.Find(name);
            Assert.NotNull(startButton);

            var setupButton = startButton.GetComponent<Button>();
            setupButton.onClick.AddListener(Clicked);
            setupButton.onClick.Invoke();
            Assert.IsTrue(clicked);
            yield return new WaitForSeconds(2);
        }

        private void Clicked() 
        {
            clicked = true;
        }
    }
}