using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NpcAI
{
    public class UIController : MonoBehaviour
    {

        NpcAIAgent oneAgent;
        public Text rewardtext;
        // Use this for initialization
        void Start()
        {
            oneAgent = GameObject.Find("AgentBody").GetComponent<NpcAIAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            rewardtext.text = oneAgent.GetCumulativeReward().ToString();
        }
    }
}