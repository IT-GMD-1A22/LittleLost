using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class LittleKitchenLevelManager : MonoBehaviour
    {
        public static bool BreadPickedUp = false;
        public static int LevelStage = 1;
        [SerializeField] private List<GameObject> stage1Arrows;
        [SerializeField] private List<GameObject> stage2Arrows;

        private void Update()
        {
            switch (LevelStage)
            {
                case 2:
                {
                    stage1Arrows.ForEach(obj => obj.SetActive(false));
                    stage2Arrows.ForEach(obj => obj.SetActive(true));
                    break;
                }
                case 3:
                {
                    stage2Arrows.ForEach(obj => obj.SetActive(false));
                    stage1Arrows.ForEach(obj => obj.SetActive(true));
                    break;
                }
            }
        }
    }
}
