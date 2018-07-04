# Battle-for-something
### Unity Machine Learning Agents



As you see this [experiment's video](https://www.youtube.com/watch?v=9Kk-oHBFylg) has two type agents: workers push the ball and aggressors attack them. I was inspired by [SoccerTwos](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Learning-Environment-Examples.md#soccer-twos). Two agents have different observation and action. At first I teached worker. And it wasn't effectively! And I decide to simplify scene. Primary I take away aggressors, after it I take away other workers. Finally there was one worker agent left so it would able to move ball to goal alone. But it turned out more effective scene was: two worker agents from two opposite teams stayed to learn overnight ;) In last version trained model worker may littel glitch near top border.
 
I wrote here previous complete this project that aggressor at once learned not bad. That was bad statement. Aggressors too bad fight trained workers. Despite it was look on SoccerTwo's agent. Invert workers was very difficult goal for this agent. But it sometimes strong accelerated and bumped into crowd workers, but it wasn't good result. Also didn' help imitation learning. In addition to I began tired for that project and began find easier solution. And aggressor got two trigger in its endings. They triggers compel workers to jump and invert, when they contact. And I simplify a bit scene (workers slower run). After two days battle for something looked like realy battle!

Unity Machine Learning Agents allows researchers and developers to create games and simulations using the Unity Editor which serve as environments where intelligent agents can be trained using reinforcement learning, neuroevolution, or other machine learning methods through a simple-to-use Python API. For more information, see the documentation page.

For a walkthrough on how to train an agent in one of the provided example environments, start here.


* Goal:

  * Aggressor: invert and disable the opponents' workers.
  
  * Worker: get the ball into the opponent's goal.

* Agents: The environment contains 18 agents, with two linked to one brain (aggressor) and other linked to another (worker).

* Agent reward function:

   * Aggressor:

  -0.0003 per step.

  +1 when disable opponent's worker.

  -1 when disable own team's worker.

  * Worker:

  -0.0003 per step.

  -0.1 when worker move away for ball.

  +0.1 when worker approach for ball.

  -0.4 when ball move away for opponent's goal.

  +0.4 when ball approach for opponent's ball.

  +0.7 when worker enter and stay to ball. 

  -1 when ball enters team's goal.

  +1 when ball enters opponent's goal.

  -1 when act[0] > 1 or act[0] < -1

* Brains: Two brain with the following observation/action space.
* Vector Observation space (Continuous):

  * Aggressor: 89 to local 12 ray casts.

  * Worker: 16.

* Vector Action space:

  * Aggressor (Discrete): 4.

  * Worker (Continuous): 1.

* Visual Observations: None.

**Worker's TensorBoard**

![GitHub Logo](https://github.com/dimakuzov/Battle-for-something/blob/master/TensorBoardWorker.png)

**Aggressor's TensorBoard**

![GitHub Logo](https://github.com/dimakuzov/Battle-for-something/blob/master/TensorBoardAggressor.png)

***

