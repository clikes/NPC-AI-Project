using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NpcAI
{
    public class UIController : MonoBehaviour
    {

        NpcAIAgent oneAgent;
        EnemyAgent oneEnemyAgent;
        public Text rewardtext;
        public Text enemyrewardtext;
        public Text TimeText;
        public Text FinalReward;
        bool finalFlag = false;
        // Use this for initialization
        void Start()
        {
            if (GameObject.Find("AgentBody") != null)
            {
                oneAgent = GameObject.Find("AgentBody").GetComponent<NpcAIAgent>();
            }
            if (GameObject.Find("EnemyBody") != null)
            {
                oneEnemyAgent = GameObject.Find("EnemyBody").GetComponent<EnemyAgent>();
                
            }
             
        }

        // Update is called once per frame
        void Update()
        {
            if (oneAgent != null)
            {
                rewardtext.text = oneAgent.GetCumulativeReward().ToString();
            }
            if (oneEnemyAgent != null)
            {
                enemyrewardtext.text = oneEnemyAgent.GetCumulativeReward().ToString();

            }
            if (30 - Timer.time >= 0)
            {
                TimeText.text = "Remain Time: " + (30 - Timer.time) + "s";

            } 
            else if (!finalFlag)
            {
                finalFlag = true;
                FinalReward.text = "Final Reward: " + oneAgent.GetCumulativeReward().ToString() + " "+ oneEnemyAgent.GetCumulativeReward().ToString();
            }
            


        }
    }
}