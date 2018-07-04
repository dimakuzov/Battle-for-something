# Battle-for-something
### Unity Machine Learning Agents


As you see this experiment has two type agents: workers push the ball and aggressors attack they. I was inspired by SoccerTwos. Two agents have different observation and action. At first I teached worker. And it wasn't effectively! And I decide to simplify scene. Primary I take away aggressors, after it I take away other workers. Finally one worker agent was and it be able to move alone ball to goal. But it turned out more effective was two worker agents into two opposite teams stayed to learn overnight ;) In last version trained model worker may littel glitch near top border, but I could not to set hyperparameters that it doesn't happened.

I wrote here previous complete this project that aggressor at once learned not bad. That was bad statement. Aggressors too bad fight trained workers. Despite it was look on SoccerTwo's agent. Invert workers was very difficult goal for this agent. But it sometimes strong accelerated and bumped into crowd workers, but it wasn't good result. Also didn' help imitation learning. In addition to I began tired for that project and began find easier solution. And aggressor got two trigger in its endings. They triggers compel workers to jump and invert, when they contact. And I simplify a bit scene (workers slower run). After two days battle for something looked like realy battle!


* Goal:

  Aggressor: invert and disable the opponents' workers.
  
  Worker: get the ball into the opponent's goal.

* Agents: The environment contains 18 agents, with two linked to one brain (aggressor) and other linked to another (worker).

* Agent reward function:

   Aggressor:

  -0.0003 per step.

  +1 when disable opponent's worker.

  -1 when disable own team's worker.

  Worker:

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

  Aggressor: 89 to local 12 ray casts.

  Worker: 16.

* Vector Action space:

  Aggressor (Discrete): 4.

  Worker (Continuous): 1.

* Visual Observations: None.


