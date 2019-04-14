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
                TimeText.text = oneEnemyAgent.lastEpisode.ToString();
            }
            

        }
    }
}