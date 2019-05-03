# Design of intelligent non-player character in games

---

## Motivation

Over 90% of PC games so far have single-player mode and a large portion of these recreations require Non-Player Characters (NPC) against which the player is contending. Constrained by the game's AI as opposed to by a gamer, NPCs exhibit astute practices amid the procedure of the diversion. 

The purpose for this project is to investigate AI in game playing, particularly how to create intelligent NPC that adjusts to the conduct of human players, and\or playing a lot of game as opposed to a particular diversion. Understudies taking on this task are relied upon to investigate computational AI methodologies and calculations.

This project should make a AI model for the controlling of NPC in game, and should be use in many game with similar structures of game logic, this project model will aim at build model for a simple chasing and collecting game. 

---

## Related Work

This project will create a new game that can use reinforcement learning to train the agent inside of the scene, player can take over the controll of the agent and player against other agent (NPC) in the game. One of the purpose of this project is to research the way of agent training by Unity, therefore, there would be a detailed introduction and explaination of training process inside of this paper.

---

## Description Of The Work

### Scene Building

Using Unity editor to build a proper scene for AI reinforcement learning training. 
There are several functionalities that the training scene should have:

- A proper complexity
  - Make it for easy to training of the agent, we do not want a complex scene that use too much time for training, but it should still shows a acceptable training result.
- Multiplayer 
  - There should be two characters play against each other, in other words, one of them should be NPC and player can play againest them.
- Use general game development materials
  - Incresing the training speed by using save the computing power on render the scene itself, so that, we can copy the training scene for multiple train at the same time, when the training is over, we can consider the view of the scene, and using the higher quality materials for the game objects.
- Easy to immigrate to other scene
  - The trained model should not be use by the training scene only, the design of scene should consider about immigration of the training scene to another scene, such as setting the same value of collider component of agent, add different tag for the detectable object, etc. The main idea is to make the scene more compatible.
- The scene should have a brand new design different from any example that offer by ML-Agents, the object in scene should easy for observe.

### Agent training

The project should train two model for two different agent, since the game has two different role of player, one is the **thief** the other is the **police**. They will be train together in the same scene and play against each other, they will both get improve of their decision making.

- Thief:
  - Finding and collecting the goal object inside of the scene.
  - Escape from the policy
- Police:
  - Finding and catching the thieves
  - Protect the goal object

The agent should train with a perceptron as the input of reinforcement learning algorithm, and out put the action of the current state. The agent will train by TensorFlow in order to output the .nn file that we can using in other scene.

---

## Methodology

### Unity & **The Unity Machine Learning Agents Toolkit** (ML-Agents)  Introduction

Unity + ML-Agents is this project’s main development tools, the reason I choose them is that, Unity is a mainstream game editor in the world now, it can build many complex scenes include nice physic simulation, simple way to import 3D-model etc. it’s a very good editor to build the reinforcement learning scene. And ML-Agent is a open-source Unity plugin that offer a simple but powerful tool for the programmer to use the deep reinforcement learning to train the model. It’s using Proximal Policy Optimization (PPO) as training algorithm, which is the latest release of OpenAI’s reinforcement algorithm, and it is become the default deep reinforcement learning algorithm of OpenAI's research (OpenAI, 2017).

### Unity Methodology

There is so many ways of game developing, using a game editor is a common way of game developing, there are so many game engine in the world right now, for 3D game, there is Unreal Engine, Frostbite Engine, for 2D game, there is Cocos2d etc. Some company like Tencent even have their own engine (QuickSilverX develop with Nvidia) for developing games. However, Unity have some feature that they do not have:

- Multiple platform support including Windows, Linux, MacOS, Android and iOS (there is over 25 platfroms), all mainstream PC opearting system and mobile operating system.
- **Free to use** as a student
- Support for 3D game development (it support 2D as well but in this project is not needed)
- Use OOP language **C#** as scripting language

Also, half of the mobile games in the world is made with Unity, and made with Unity program were install in the past 12 months (Unity, 2019). Therefore, Unity is a mainstream game editor, it has excellent flexibility, and free to use. The ability it has is more than enough for this project.

### Reinforcement Learning Methodology

There is many kind of Machine Learning techniques, such as Deep Learning mainly used in value estiamte, GAN mainly used in picture generate etc. The game AI require a model that can accept current state and make the decision, and it's a unsupervised learning since the agent know nothing from the beginning. 

In reinforcement learning, we provide agent a reward when it make a decision, we can justify the decision is whether good or bad and give agent a postive reward or a negative reward. In the beginning of training, the agent do not know anything, after the training agent would know how to make decision to make postive reward gain optimaize.

Moreover, there is many successful reinforcement learning product are there until today, such as Google DeepMind's *AlphaGo* which defeat the best human go player in the world, OpenAI's *OpenAI Five* which defeat the best human Dota2 team in the world.

Let see an example of what is currently one of the leading reinforcement learning methods, Deep Q-Learning work.

> #### Deep Q-Learning Example 
>
> Deep Q-Learning is a classic deep reinforcement learning method, it is conbine neural network and reinforcement learning, it is enhance the training result. We can watch the equation of updating of Q value:
> $$
> N e w Q(s, a)=Q(s, a)+\alpha\left[R(s, a)+\gamma \max Q^{\prime}\left(s^{\prime}, a^{\prime}\right)-Q(s, a)\right]
> $$
> α: Learning rate
> γ: Discount rate
> A brief training process would be (Comi, 2018):
>
> 1. Randomly assign a Q-value in the beginning of game
> 2. Environment get the current state s
> 3. Agent brain excute an action for the agent, the action is rancomly assign or estiamate by the neural network, the action excution will more depend on the neural network over time.
> 4. Get the reward of the action, and now we have s', we can update new Q using the equation, each action and state would be store in memory for the following training.
> 5. Repeat 3 and 4 

Therefore, Reinforcement Learning which is a unsupervised learning technique and it is a Markov decision process is a better alternative for our project. It is can let our agent make more optimaize decision, and it's more appropriate than other Machine Learning technique. 

### ML-Agents Methodology

To apply the reinforcement learning in Unity, in other word, using C# to implement the reinforcement learning, is not a easy work, since 90% of machine learning programmer is using TensorFlow or PyTorch as the development or research tools, and it is using Python as programing language, we cannot use them in C#. However, we can try to apply reinforement learning ourself, such as algorithm like Q-Learning and SARSA, but we cannot export a model after training like TensorFlow can do, that not what we want, there should be generate a model that we can use to apply to other agnet.  Also the traditional 

ML-Agents solve these problems, it is using protobuf to build a communication between Unity and Python API,![image-20190424171147554](/Users/like/Library/Application%20Support/typora-user-images/image-20190424171147554.png)**Figure :** ML-Agents learning environment communicate abstract (Juliani et al, 2018)
so that, ML-Agents can generate TensorFlow's .nn file, the .nn file is widely use, and we can load this file as the trained model to the brain of our agents.

There is another aspect that we should consider about **the performance of the algorithm**.

ML-Agents using PPO as the main reinforcement learning algorithm (ML-Agents can also train model with LSTM), it's a new efficient reinforcement learning algorithm, it has similar or better performance than the most advance algorithms (OpenAI 2017), it's release by OpenAI at July 20, 2017.

### OpenAI introduction

OpenAI is a non-profit artificial intelligence research company (Brockman et al, 2015). It's founded by Elon Musk and Sam Altman and running with many AI experts including Ilya Sutskever, Greg Brockman etc. 

It is release many product and paper since it was founded:

- OpenAI Five a AI agent for the MOBA game Dota 2 which defeated the 2018 world champion team OG.
- A language model that can generate coherent text as paragraphs.

And publish AI related paper about once a month since May 2016. PPO is one of the product of them.

### PPO Methodology

There are several reinforcement learning algorithm been found in recent years, deep Q-learning,  trust region policy optimization(TRPO) and "vanila" policy gradient mathod are the most competitive method. But they all have some weakness (Schulman et al., 2017). Therefore, OpenAI decide to find a new class of algorithm to overcome those weakness, that is, PPO. PPO is a policy optimization method, but it use more than one epochs of random gradient ascent to apply to each update of policy, it been test that have similar or better performance than three method been mention:![image-20190424203844044](/Users/like/Library/Application Support/typora-user-images/image-20190424203844044.png)**Figure :** Comparison of some mainstream deep reinforcement algorithm on MuJoCo environment (Schulman et al., 2017) 
Hence, PPO is a good choise for a reinforcement learning project, since ML-Agents is using PPO as the main training algorithm, we can trust the performance of ML-Agents's training result. 

### Curriculum Training Methodology

The reinforcement learning is strong, but we still need a properly design of the training, we should divide the whole training process into different stage, we incresing the complexity of the scene during the training depend on the training result, it would increse the training performance. 

The agent like a person know nothing about the game at the beginning of the training, we cannot teach a person calculus when he did not even know about algebra, the reinforcement learning process is similar to human learning new knowledge. We should teach it simple things first, then we incresing the complexity of the problem, so it can learning faster than directly learning the most complex problem, somtimes, the agent cannot learn anything due to the higher complexity of the scene.

ML-Agents also give us a experiment scene (Wall Jump Scene) for introduction of curriculum training, it offered a experiment result of this scene with and without curriculum training:![curriculum_progress](/Users/like/Documents/git/NPC-AI-Project/Dissertation%20files/curriculum_progress.png)**Figure :** Comparison of WallJump scene cumulative reward of with curriculum training and without curriculum training (Unity Technologies, 2018)

There is a fairly significant gap between the trainning results with and without curriculum training, even when the training result is converged the reward gain of curriculum training is still better than the one without. 

Hence, this project will use curriculum training to incresing the training perfromence.

---

## Design

The design of this project will split to three parts, since this project should training a model for agent, it is meaningless to training other's pre-set-up scene, hence, this project will create a new AI training scene, and train reinforcement learning model for the agent inside of the game scene. While training, the materials of the scene will be some low quality standard 3D model, the 3D model should be replaceable after the training of the agent, the game project would be training friendly by easy changing the constant number of the game and most of the code is hot code instead of hardcode. 

### Game Design

The main logic of game would be simple and well targeted, the playability would not be consider as a feature of the game, since this project is mainly for AI training.

The original design of game was a single agent player, and many NPC randomly moving in the scene, the agent need to   chase and catch each of them, but after trying of the ML-Agents' examples, I think this task is too simple to acomplish, and the complexity of the scene is too low. Hence, I decide to build a game with two different agent, they both have a task to do, and they will play against each other, and there would be more detectable object inside of the scene. 
Here's a list of every object inside of the game scene:

- Ground
  - A ground that place all other game object, and become a location benchmark of other game object, easy for runtime controll of the game scene.
- Tresure object
  - There is a tresure object inside of the scene, when it be steal by the thieves, it will randomly reborn inside of a point ground in the scene.
  - It is make by a simple 3D box object with a collider conponent and controll by a script.
- Obstacles 
  - There are some obstacles inside of the scene, it design to block some of the move of police and thieves, it is one of the things that incresing the complexity of the scene.
  - It is make by a simple scaled 3D box object with a collider conponent.
- Walls
  - Walls are like obstacles, it place on the edge of the ground, once police touch the walls, it die and randomly reborn in the scene.
  - The wall is invisible, because it design to detect if agent drop from the ground, it is more like a detector, but it can still be a perceptible object in the scene, it is increse the complexity of the scene also.
  - It is make by a simple scaled 3D box object with a collider conponent, but disable the mesh renderer to acomplish the invisible feature.
- Two different roles of player:

  - Police agent 
    - The agent should move around the scene to find the thieves and catch them.
    - There is a tresure object in the scene, police should protect this object from stealing by the thieves.
    - There is only one policy agent in the scene.
  - Thief agent
    - The agent should move around the scene to find the tresure while escape from the police.
    - There are two of the thief agent in the scene.
  - The thief will reborn after it be catched by the police.
  

### Training Design

The training should implement the curriculum training method.

Two agents will train separately at the beginning of the training process, when each of them have some "knowledge" of the complete scene, we then put them together and train.
This training design is the final design, the design have changing during the each times I train the model since the project is start, each value of this project is been adjust many time until the result is acceptable.
The detailed training stage explaination would be:

1. First training stage
   1. Police agent training
      1. Training scene set up
         - No obstacles, only one thief, thief will stand still, no tresure, 4 wall is active
      2. Change stage when agent can easily find and catch the thief (the mean cumulative reward curve is converge)
   2. Thief agent training
      1. Training scene set up
         - No obstacles, only one thief, no police agent, one tresure, 4 wall is active
      2. Change stage when agent can easily find and catch the thief (the mean cumulative reward curve is converge)
2. Second training stage
   1. Police agent training
      1. Training scene set up
         - No obstacles, only one thief, thief randomly moving in the scnen, no tresure, 4 wall is active
      2. Change stage when agent can easily find and catch the thief (the mean cumulative reward curve is converge)
   2. Thief agent training
      1. Training scene set up
         - No obstacles, only one thief, one police agent, police agent randomly moving in the scene, one tresure, 4 wall is active
      2. Change stage when agent can easily find and catch the thief while get away from the police agent (the mean cumulative reward curve is converge)
3. Third training stage
   1. Police agent training
      1. Training scene set up
         - With obstacles, two thief, thief randomly moving in the scnen, one tresure, 4 wall is active
      2. Change stage when agent can easily find and catch the thief while dodge the obstacles (the mean cumulative reward curve is converge)
   2. Thief agent training
      1. Training scene set up
         - With obstacles, only one thief, one police agent, police agent randomly moving in the scene, one tresure, 4 wall is active
      2. Change stage when agent can easily find and catch the thief, dodge the obstacles and get away from the police agent (the mean cumulative reward curve is converge)
4. Forth training stage
   1. Police and Thief agent train together
      1. Training scene set up
         - Whole complexity scene: with obstacles, two thief agents with half-trained brain, one police agents with half-trained brain, one tresure, 4 walls is active
      2. Keep training until both agents is well-trained (the mean cumulative reward curve of both agents is converge).

We can see that in this four stages the higher stages have the higher scene complexity, this curriculum design would theoretically incresing the training performance, reduce the training time and get a better result.

---

## Implementation

### Environment Set Up & First training

Here's a detailed development environment set up, since ML-Agents is still in beta phase, and there may be a lot of programmer want to research or use reinforcement learning in there scene which build by Unity editor. This part of implementation may let them start their training quicker than reading the document of ML-Agents. 

#### Download:

- Unity 2017.4+
- Visual Studio 2019 with Unity programming package
- ML-Agents github clone
- Python 3.6
- TensorFlowSharp

After download of ML-Agents project use this command to download more dependency:

> ```shell
> cd ml-agents-envs
> pip3 instal  l -e ./
> cd ..
> cd ml-agents
> pip3 install -e ./
> ```
>

There is also a detailed [installation guide][ML-Agent installation guide] include in the project.

#### Try Sample training Scene

Use Unity editor to open the project: ml-agents/UnitySDK, depend on the differenct of Unity version it may have warning like this:

![image-20190422171052872](/Users/like/Library/Application Support/typora-user-images/image-20190422171052872.png)**Figure 1:** Warning message box of Unity (Like Chen 2019) 

mostly we can just ignore this and click upgrade.

Then we can see there is a Example folder in Assets/ML-Agents/ , we can see there is many training scene and pretrain model inside there, open the example scene and click the play button, we can see the pretrain model agent behavior in Inference mode. 

Some screen shot of train scene:

![image-20190422175122447](/Users/like/Library/Application Support/typora-user-images/image-20190422175122447.png)**Figure 2:** Banana collector training scene (Like Chen, 2019)

![image-20190423154949754](/Users/like/Library/Application Support/typora-user-images/image-20190423154949754.png)**Figure 3:** SoccerTwos training scene (Like Chen, 2019)

![image-20190422175251814](/Users/like/Library/Application Support/typora-user-images/image-20190422175251814.png)**Figure 4:** WallJump training scene (Like Chen, 2019)

ML-Agents provide difference scene in purpose that showing the ability of ML-Agents can do.

For example, 

Banana colloctor shows that ML-Agents can train multiple agents with same brain in a training scene, all agent in the scene actually using same brain, and they can be train at the same time.

SoccerTwos shows that ML-Agents can train multiple agents with multiple brain in the same time, in the scene two agents in one side act different role in the game, one for goal-keeper and one for striker.

WallJump is a special one, because we cannot know what happen inside one figure, this one is a ML-Agents example of curriculum training, the complexity of scene is increasing by a time or a reward gain of training process which we can adjust before training.

> ```json
> {
>     "measure" : "progress",
>     "thresholds" : [0.1, 0.3, 0.5],
>     "min_lesson_length": 100,
>     "signal_smoothing" : true, 
>     "parameters" : 
>     {
>         "big_wall_min_height" : [0.0, 4.0, 6.0, 8.0],
>         "big_wall_max_height" : [4.0, 7.0, 8.0, 8.0]
>     }
> }
> ```
>
> (ml-agents/config/curricula/wall-jump/BigWallJumpLearning.json, ML-Agents, 2017)

After trying and seeing the example scenes, we can have a little bit understand of what ML-Agents can do, it is has enough ability like multiple agent training, multiple brain training to achieve our goal of agent training.

####First Try of ML-Agents training

We can use the most simple scene for our first training using ML-Agents. ML-Agents already provide a simple scene inside of the example folder, the Basic training scene.

![image-20190423164404979](/Users/like/Library/Application Support/typora-user-images/image-20190423164404979.png) **Figure 5:** Basic training scene

This agent only have two actions, go left or right, and there are two goal that it can collect, right one have more reward than the left one.

The agent script and the scene is already set up, but before we start training, we should check the control toggle inside the acdemy gameobject of scene, 

![image-20190423175507651](/Users/like/Library/Application Support/typora-user-images/image-20190423175507651.png)**Figure 6:** Check control toggle

the training config and the training script is set up by ML-Agents, therefore, we can open the ml-agents directory and type command:

```shell
mlagents-learn config/trainer_config.yaml --run-id=basic --train
```

When we should train the model that we trained before, we can use the command:

```
mlagents-learn config/trainer_config.yaml --run-id=basic --train --load
```

By adding the load arguement to the command line, we can load the model that have the same run id, which is 'basic' in this situation. 																																								when we seeing this:![image-20190423165701283](/Users/like/Library/Application Support/typora-user-images/image-20190423165701283.png) **Figure 7:** training message (Like Chen, 2019)

we can click the play button of Unity, and the training will begin. The train will take time due to different device, we can watch the process by directly seeing the agent's behavior inside the editor, or we can observe the mean reward that post on terminal period,![image-20190423181526598](/Users/like/Library/Application Support/typora-user-images/image-20190423181526598.png)**Figure 8:** Command line training result

But to observe the training process by tensorboard is a better choice.

Open another terminal in ml-agents directory and type command:

```shell
tensorboard --logdir=summaries
```

We can open webpage at localhost:6006 to open the tensorboard main page, then we can observe the data through some chart, it is more clearly to watch the training process.

![image-20190423194720924](/Users/like/Library/Application Support/typora-user-images/image-20190423194720924.png)**Figure 9:** TensorBoard view of basic scene training

TensorBoard will update the data over time, so we can watch the training process continuous, by observing the cumulative reward, value estimate and entropy chart, we will can know that the training process is useful or not, cause a bad hyperparameter cannot train a useful model, we should adjust the hyperparameter in time before the model went into worse direction, or we should consider change the whole training logic.

By observing the chart, we can find that the training result may converged over time. This time we can stop the training by just click the play button again or 'ctrl + c', to termate the training program. And find our trained model in ml-agents/model/'runid' folder.![image-20190423203249156](/Users/like/Library/Application Support/typora-user-images/image-20190423203249156.png)                 **Figure 10:** Trained model

The final work is copy this .nn file into out assets folder, then drag it to our learning brain we use.![image-20190423203556360](/Users/like/Library/Application Support/typora-user-images/image-20190423203556360.png)**Figure 11:** Set up learning brain with trained model

Finally, we go back to the academy and uncheck the control toggle, then click the play button, we can see our trained agent is playing.

To watch other example and to training a model first time is really important, it is give you a basic idea of what reinforcement learning can do and how to build a nice scene.

#### First Try Of Building a ML-Agents Training Scene (*This scene train by ML-Agents bata v0.5)

The first scene I build for ML-Agents training was the simple test game, follow the scene building [guide](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Learning-Environment-Create-New.md) offer by ML-Agents project.

The test game scene is a simple game: Agent adding force to white sphere, trying to reach the goal cube and try not to fall from the black plane. This game is build all by myself with mlagents’ document. 

##### The scene is simple, only contain a few object: 

Plane, Roller(Sphere) and Target(Cube). 

![image-20190428193330327](/Users/like/Library/Application%20Support/typora-user-images/image-20190428193330327.png)**Figure: ** Screen shot of the test game I built

##### Academy, Brain and Agent

ML-Agents using this three script for the training, which mention before that academy contain a external communicator with python API. Therefore, we need to setting up the training environment for the training inside of Unity. 

To do this, we should create a ***BasicAcademy*** class inherit the ***Academy*** class from ML-Agents, and create ***BasicBrain*** class inherit the ***Brain*** class from ML-Agents (*this process is different from the v0.7 of ML-Agents), then create and edit  the **BasicAgent*** class inherit the ***Agent*** class from ML-Agents. About the coding of ***BasicAgent*** class will be explain in continous part.

##### Training Settings: 

To train a model, brain need input data for decision making, and inside agent script we have a recall function for apply new action to out agent. 

##### Observation: 

Use function AddVector inside override function CollectObservations to add input to brain. 

- Relative position of agent and goal.
- Agent’s speed.
- Distance to edge of plane.

![image-20190428193934052](/Users/like/Library/Application%20Support/typora-user-images/image-20190428193934052.png)
**Figure:** Code of Observations

Using function ***AddVectorObs*** to add a observation value or a list of observation values to the agent brain, the brain would take these observation as input, the ***CollectObservations*** function will be call before each action.

##### Reward Setting: 

In reinforcement learning, we should tell agent which result is good and which result is bad, then it will adjust itself for optimal decision. 

- When agent fall from plane add negative reward. 
- Reach goal cube add positive reward. 
- Move closer to goal add positive reward. 
- Give a time punishment to let the training process not dilatory. 

##### Training Process Observation

Open TensorBoard and observe the training process. A normal training data may look like this:![image-20190428194915043](/Users/like/Library/Application%20Support/typora-user-images/image-20190428194915043.png)**Figure:** TensorBoard View

Normally, we should more focus on cumulative reward, entropy and value estimate, since this three data more related to the training result. While training step increasing, if the cumulative reward and value estimate should be increasing,  and entropy should be decreasing, if the data look like that means the training is healthy, otherwise, we should adjust the hyperparameter or agent script to retrain the model.

Let look back to the test scene training. The result of training was good: ![image-20190428195445703](/Users/like/Library/Application%20Support/typora-user-images/image-20190428195445703.png)**Figure:** Cumulative reward and entropy data of test scene
We can see that cumulative reward is keep increasing and entropy is decreasing by when training step increases, it reach 10 and not keep increasing since 90k steps, it reach the optimal position. 

The training of agent in the test scene only last five minutes on my computer with CPU Intel Core i7-4710HQ and Graphic card NVIDIA GeForce 980M. 

### Scene Building

#### Game Objects of Training Ground

To building a game scene by Unity should mainly concern about two aspect, game objects in scene and controlling scripts. Moreover, to build a scene for ML-Agents trainable scene should have more concern about the scene building, since the scene should follow a certain structure, otherwise, the training cannot process.

By design, the scene should build with simple materials, so the standard Unity 3D model and ML-Agents shared assets:
 ![image-20190428205516448](/Users/like/Library/Application Support/typora-user-images/image-20190428205516448.png)
**Figure: **Police agent
![image-20190428205607237](/Users/like/Library/Application Support/typora-user-images/image-20190428205607237.png)
**Figure:** Thief agents
![image-20190428205803543](/Users/like/Library/Application Support/typora-user-images/image-20190428205803543.png)
**Figure:** Obstacle create by Unity standard 3D object cube scale
![image-20190429150820133](/Users/like/Library/Application Support/typora-user-images/image-20190429150820133.png)**Figure:** Invisable walls create by 4 cube without mesh renderer
![image-20190429151052991](/Users/like/Library/Application Support/typora-user-images/image-20190429151052991.png)**Figure:** Ground and green point ground create with Unity standard 3D object plane
These scene all using standard assets offer by Unity or ML-Agents, it is save the calculation resource of computer. And the view of the whole scene is clearly and obviously, the agent's behaviour can be shown clearly that we can easily tell the agent is doing good or bad.

Moreover, the training ground is been make as prefab that we can easily copy the training ground for multiple agent training to incresing the training speed. Prefab as a nice feature of Unity that I can easily change a single training ground and apply change to every copy of this prefab. In this training scene, I use 30 copies of the training ground to training at the same time.

#### UI

In order to obtain timely feedback, I add two Text of the current cumulative reward on the game screen, these Text is significantly useful, that you may found the agent's behaviour is wrong but it still keep doing the wrong things, for example in my training process the agent may keep heading to the wall, running circle in the edge of training ground, police keep away from the thief etc. The reason why agent keep doing strange things it is because doing this things is actually incresing the cumulative reward, you may check if the reward setting is correct, one of the reason that the example things happen is because the reward of get closer to thief is been set to a negative reward incorrectly, the agent is getting away from thief to reduce the negative reward gain. Hence, add a UI of instant cumulative reward is important and useful for debuging the training process, since it is different from the normal programming.

![image-20190429174508693](/Users/like/Library/Application Support/typora-user-images/image-20190429174508693.png)**Figure:** UI text of current cumulative reward

#### Scripts of Training Ground

The Unity game scene need the support of C# scripts, the scripts control the whole game logic.

This project has 11 scripts including 2 agent scripts and 1 academy script which using for ML-Agents' training, the other scripts are more about create a train friendly scene.

##### Training Scene Scripts

At the beginning of this project the police agent is build as the only agent which call NPC and thief agent call as enemy, so the class name of thief and police related scripts' name is different from the "police" and "thief" in the paper.

- Consts.cs

  - The script that including most of the constant values that should use by the game.

    ```c#
     public static class Consts
        {
    
            public static float GroundScale { get; set; } = 1.0f;
            public readonly static int NumOfEnemies = 1;
            static int PointGroundLengthNormal = 10;
            static int AlertGroundLengthNormal = 17;
            static int OutsideGroundLengthNormal = 24;
            public readonly static float PointGroundLength = GroundScale * PointGroundLengthNormal;
            public readonly static float AlertGroundLength = GroundScale * AlertGroundLengthNormal;
            public readonly static float OutsideGroundLength = GroundScale * OutsideGroundLengthNormal;
            public readonly static float killDistance = 1.2f;
            public readonly static float agentMoveSpeed = 10.0f;
            public readonly static float enemyMoveSpeed = 8.5f;
            public readonly static Vector3 HeightOffset = new Vector3(0,0.5f, 0);
            public readonly static float EnemyRespawnTime = 0f;
            public readonly static float TurningSpeed = 600.0f;
            /// <summary>
            /// in second
            /// </summary>
            public readonly static float episodeTime = 30f;
        }
    ```

    When other scripts need to use these value, just use the reference of class ***Consts***, this because when the constant values need to change I can just change the value inside this class and all reference would change to the new one.

- TrainingGround.cs

  - Get the reference of game obects inside the training ground itself, for easy access to the obects.

- Timer.cs

  - A class of in game time, during training, the in game time would be scale to be faster for incresing the training speed, therefore, the in game time is different to the real time, this class using ***Time.fixedDeltaTime*** to record the current time in game, ***Time.fixedDeltaTime*** is the time that each frame cost, after update of each frame, Unity would update time, if the in game time been scaled, ***Time.fixedDeltaTime*** would be different to it normal value.

- TreasureController.cs

  - This class control the behavior of the treasure, it reborn to a random position inside of the point ground each time it been steal by the thife.
  - Once an episode of training end, it reborn to new position as one of the step to reset the whole training ground.

- EnemyController.cs

  - Let thief game object can be catch by police agent as the thief agent, by adjust the value ***moveable*** inside of the class can change the thief state to randomly moving or freeze.
  - This class hardcode the thief's behaviour,  mainly use for the training of police agent.

- PuppetAgentController.cs

  - Similar as the ***EnemyController.cs*** but this one is for training of the thief agent.

- ObjectPerception.cs: The "vision" of Agent

  - This script is make for agent to perceive the objects around itself, the agent will cast fixed number of sphere (following a straight line), the number is the length of a list of angles, the sphere will fly foward due to the input angles and a fixed length of distance, then the ***Physics.SphereCast*** will return the first game object it hit, we can using the tag of the object to regonize which object it hit.
    ![image-20190430155758238](/Users/like/Library/Application Support/typora-user-images/image-20190430155758238.png)
    **Figure:** Sketch of sphere cast line, the "vision" of agent
    This class is actually the agent's "vision", the data it got will become a big part of the brain's observation input, it simulate human's vision, and return with some numerical data for the reinforcement learning brain.

  - The reason why we need this script is that there may be many objects with same type in the scene, due to the constraint of ML-Agents, the number of observatoin data should be a static fixed number of value,  it should detemind before the training start, and cannot change during the training, moreover, we cannot load a half-training model with different number of observation data. 

    ![image-20190430165840948](/Users/like/Library/Application Support/typora-user-images/image-20190430165840948.png)
    **Figure:** Example of Observation data setting 

    With this class we can adjust the scene with as many object as we want, we do not need to reference each of this object to the observation to the brain. In this project, the ***ObjectPerception*** provide 7 lists of detection data to the observation, it's mean that the agent can "see" at most 7 object at the same time (each frame). Hence, we can use the ***ObjectPerception*** to solve the observation fixed number problem.

  - This script is edit base on ***RayPerception.cs***. The main idea is to using one hot encoding to collect the data, for example of thief agent's code of observation collect:
  
    ```c#
     public override void CollectObservations()
            {
                float rayDistance = Consts.OutsideGroundLength * 0.8f;
                float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
                string[] detectableObjects = { "obstacle","wall", "agent", "target"};
                List<float> buffer = rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);
                AddVectorObs(buffer);
                AddVectorObs(transform.forward.x);
                AddVectorObs(transform.forward.z);
            }
    ```
  
    We can see that the thief agent can detect five objects: obstacle, wall, agent and target.
    In code, it repersent as a list of string which contain the detectable objects' tag, the tag is a Unity feature which can let us easily reference to the game object in scene. 
    The data of each angle would be something like this:
  
    | Obstacle | Wall | Agent | Target | Nothing | Delta X | Delta Y |
    | -------- | ---- | ----- | ------ | ------- | ------- | ------- |
    | 0        | 1    | 0     | 0      | 0       | 2.4     | 3.1     |
  
    This data means the relative position of the wall relative to the agent is on the vector of (2.4, 3.1), the data structure is been change by me, since the ***RayPerception*** would collect data like this:
  
    | Obstacle | Wall | Agent | Target | Nothing | Relative Distance         |
    | -------- | ---- | ----- | ------ | ------- | ------------------------- |
    | 0        | 1    | 0     | 0      | 0       | Hit distance/ray distance |
  
    It using a relative distance to represent the position of the detected object, which I think adding a value to it is easier for reinforcement learning brain to find the relation between data, although one value is save the obsrevation space for the input, I rather using two value for it, since it can reducing the training time.

##### ML-Agents' Scripts

###### Academy

The academy of this project control the scale of each training ground due to the value in ***Const*** at the beginning of training. Other function of academy is set up by ML-Agents already.

###### Brain

ML-Agents is a iterative beta project, the project is upgrade from v0.5 to v0.7 during this project processing (at the time of writing this paper, it upgrade to v0.8, but since the model is already trained, I did not choose to update to new version), the brain part had a significant change than before, it become a .asset file instead of a script can be inherit, it is more convenient to use than before, just right click and choose the brain from the menu we have the .asset file, then we can adjust the values easily.

![image-20190501191704953](/Users/like/Library/Application Support/typora-user-images/image-20190501191704953.png)
**Figure:** Sample brain data setting
Two brains have similar value, since the police using the vector of tresure instead of using the object perception to observe the tresure, it is observation space size is 46 less than brain of thief. And it will output 4 continuous values which represant the movement and the  rotation of the agent.

###### Agent

- Police Agent

  - Collect Observations

    ```c#
            public override void CollectObservations()
            {
                float rayDistance = Consts.OutsideGroundLength * 0.8f;
                float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f};
                string[] detectableObjects = { "Enemy" , "wall" , "obstacle"};
                List<float> buffer = rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);
                AddVectorObs(buffer);
                AddVectorObs(transform.forward.x);
                AddVectorObs(transform.forward.z);
                AddVectorObs(target.transform.position.x - transform.position.x);
                AddVectorObs(target.transform.position.z - transform.position.z);
            }
    ```

    - 7 angles of object perception data with 3 kind of deteable objects
    - Facing direction of agent
    - Treasure relative position, police should have the exact position of the treasure all the time

  - Reward Setting

    - Catch a thief get +5 reward
    - Hit wall get -1 reward
    - Hit or stuck with obstacle get -0.05 reward per frame
    - Get close from enemy get +0.1 reward
    - Get away from enemy get -0.1 reward
    - Look at enemy get +0.001 (This reward will disable during the curriculum training)

  - Action Setting

    ```c#
                Vector3 dirToGo = Vector3.zero;
                Vector3 rotateDir = Vector3.zero;
                Vector3 MoveToward = Vector3.zero;
                Vector3 LookToward = Vector3.zero;
                if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
                {
                    //clamp value bigger than 1 return 1 less than -1 return -1
                    MoveToward.x += Mathf.Clamp(vectorAction[0], -1, 1);
                    MoveToward.z += Mathf.Clamp(vectorAction[1], -1, 1);
                    LookToward.x += Mathf.Clamp(vectorAction[2], -1, 1);
                    LookToward.z += Mathf.Clamp(vectorAction[3], -1, 1);
                    transform.LookAt(transform.position + LookToward.normalized);
                    agentRb.AddForce(MoveToward.normalized * Consts.agentMoveSpeed, ForceMode.VelocityChange);
                    if (agentRb.velocity.magnitude > Consts.agentMoveSpeed)
                    {
                        agentRb.velocity = agentRb.velocity.normalized * Consts.agentMoveSpeed;
                    }
                }
    ```

    Use 4 continuous values for the agent controling, two values for move direction, other two values for rotation direcition. 

- Thief Agent

  - Collect Observations
    - Similar to police agent, but it percept 4 kind of detectable objects.
  - Reward Setting
    - Steal treasure get +5 reward
    - Hit wall get -1 reward
    - Hit or stuck with obstacle get -0.05 reward per frame
    - Be catch by police get -2.5 reward
    - Get close to target get +0.05 reward
  - Action Setting
    - Identical to the police agent

### Training

ML-Agents has a nice curriculum learning feature, but by design of this training process, the agent should be train one by one and when they each have a better behaviour we train them together. ML-Agents did not support this process yet (until ML-Agents v0.8), the agent should be train start with either one agent or two agent, but cannot change the training agent or mix up two agent training during training. Hence, this project implement curriculum learning manually by manually start each training.

The training of agent is not like the supervize training, we should observe most of training process including data in TensorBoard, agent behaviour in game and current reward in game, and we adjust the hyperparameter or even re-design a part of training, the design of this project is already the final version of training after many modified. 

####Agent Training

##### Setting up scene

1. Setting up training scene by design
2. Modify ground scale in ***Const*** to 0.7f at the first stage of training (The training was very slow in the full size ground) 
3. ![image-20190502213415390](/Users/like/Library/Application Support/typora-user-images/image-20190502213415390.png)

**Figure:** Each stages training scene of police agent

4.![image-20190502213805357](/Users/like/Library/Application Support/typora-user-images/image-20190502213805357.png)**Figure:** Each stages training scene of thief agent

#####Training Configurations

1. ```yaml
  NPCAILearningBrain:
      gamma: 0.995
      beta: 1.0e-3
      batch_size: 512
      buffer_size: 10240
      num_epoch: 3
      max_steps: 5.0e5
      hidden_units: 256
      num_layers: 2

  EnemyLearningBrain:
      gamma: 0.995
      beta: 9.0e-4
      batch_size: 512
      buffer_size: 10240
      num_epoch: 3
      max_steps: 5.0e5
      hidden_units: 256
      num_layers: 2
  ```
  
  **The beta value is controlling  randomness of the policy, we should adjust the beta value during training by oberving the entropy curve of TensorBoard.**
  
  Two configurations is similar but with different beta value, because during training the entropy of them have different curve, it is should adjust separately. 

#####Training Observing

######Police Agent First Three stages

![image-20190502222534343](/Users/like/Library/Application Support/typora-user-images/image-20190502222534343.png)
**Figure:** Cumulative Reward of Police Agent 

*A data loss happened between 150k to 200k step, pause a train and resume again may cause some data loss.

We can see that the curve would have a change after the stage change (new complexity of scene), but the reward is still incresing. This line is fluctuation due to a lot of hyperparameter change during the training, most of the change was the change of the reward.

###### Thief Agent First Three Stages

![image-20190503001213524](/Users/like/Library/Application Support/typora-user-images/image-20190503001213524.png)**Figure:** Cumulative Reward of Thife Agent

*A data loss happened between 170k to 210k step, pause a train and resume again may cause some data loss.

######Final Training With Two Agent Together

![image-20190503161109664](/Users/like/Library/Application Support/typora-user-images/image-20190503161109664.png)**Figure:** Cumulative Reward of Two Agents (Blue line is thief agent, red line is police agent)

To merge two training agent together, we should copy two agent's training file from the folder ml-agents/models to a new folder with the new run id that we going to training two agents together, in this project particularly, is "npcai-dual", the ML-Agents will load the file from the new folder and continous training with two half-trained model.

Two line start at different x axis because the previous training is ending in different step.

The thief agent cumulative reward curve is already converge at about 300k steps, but after about 200k steps training, police agent's cumulative reward curve is very steep, the reason why this happen may be the random position of treasure and thief agents, it may let police agent catch too many thief in a episode or too little in a episode. But the cumulative reward do not have a obvioursly incresing or decresing, and the agent behaviour look well inside of the scene. I choose to end the training now. 

In this point, two model of agent is already generated.

---

## Evaluation





---

## Reference List

Brockman, G., Sutskever, I. and OpenAI (2015). *Introducing OpenAI*. [online] OpenAI. Available at: https://openai.com/blog/introducing-openai/ [Accessed 24 Apr. 2019].

Comi, M. (2018). *How to teach an AI to play Games: Deep Reinforcement Learning*. [online] Towards Data Science. Available at: https://towardsdatascience.com/how-to-teach-an-ai-to-play-games-deep-reinforcement-learning-28f9b920440a [Accessed 25 Apr. 2019].

Juliani, A., Berges, V.P., Vckay, E., Gao, Y., Henry, H., Mattar, M. and Lange, D., 2018. *Unity: A general platform for intelligent agents*. *arXiv preprint arXiv:1809.02627*.

Mnih, V., Badia, A.P., Mirza, M., Graves, A., Lillicrap, T., Harley, T., Silver, D. and Kavukcuoglu, K., 2016, June. Asynchronous methods for deep reinforcement learning. In *International conference on machine learning* (pp. 1928-1937).

OpenAI. (2019). *Proximal Policy Optimization*. [online] Available at: https://openai.com/blog/openai-baselines-ppo/ [Accessed 24 Apr. 2019].

Schulman, J., Wolski, F., Dhariwal, P., Radford, A. and Klimov, O., 2017. Proximal policy optimization algorithms. *arXiv preprint arXiv:1707.06347*.

Teunissen, X. (2019). *OpenAI Five defeat Dota 2 champions OG in showmatch*. [online] Daily Esports. Available at: https://www.dailyesports.gg/openai-five-defeat-dota-2-champions-og-in-showmatch/ [Accessed 24 Apr. 2019].

Unity. (2019). *Unity Public Relations Fact Page*. [online] Available at: https://unity3d.com/public-relations [Accessed 23 Apr. 2019].

Unity Technologies (2018). *Unity-Technologies/ml-agents*. [online] GitHub. Available at: https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Training-Curriculum-Learning.md [Accessed 27 Apr. 2019].



[ML-Agent installation guide]: https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Installation.md

