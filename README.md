# Battle-for-something
### Unity Machine Learning Agents



As you can see this [experiment's video](https://www.youtube.com/watch?v=9Kk-oHBFylg) has two types of agents: workers push the ball and aggressors attack them. I was inspired by [SoccerTwos](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Learning-Environment-Examples.md#soccer-twos). The agents of the two types have different observation and action. They had to be taught separately from each other. First I teached the worker, and it wasn't effectively! So I decided to simplify the scene. In the beginning, I take away aggressors, then I take away the other workers. Finally there was one worker agent left so it would able to move ball to goal alone. But it turned out that the more effective scene was: two worker agents from two opposite teams stayed to learn overnight ;) In the latest version the trained model worker have littel glitch: it sometimes stays near top border.
 
I wrote earlier in my [post in UnityConnect](https://connect.unity.com/p/battle-for-something) that the aggressor's training had good start. I was wrong. The aggressors attacked trained workers very bad, despite that they are similar to the SoccerTwo's agent. To overturn workers was a very difficult goal for this agent. The aggressor was sometimes highly accelerated and bumped into the crowd of workers, but this wasn't a good result. Also imitation learning didn' help. In addition, I start to get tired of this project and decided to find an easier solution. After that, the aggressor got two trigger at his endings. The triggers force workers to jump and to overturn, when they contact the aggressor's trigger. I simplify the scene a bit (the workers slower run). After two days my agent learned to find and attack workers. Now the battle for something looked like a real battle! Judging by the tensorboard schedule, max_steps could be greatly reduced, but after a successful result I didn't want to experiment anymore.

In this repository, folder "pythonBFS" is required to train the working agent and "pythonAg" for the aggressor agent.

Unity Machine Learning Agents allows researchers and developers to create games and simulations using the Unity Editor which serve as environments where intelligent agents can be trained using reinforcement learning, neuroevolution, or other machine learning methods through a simple-to-use Python API. For more information, see the [documentation page](https://github.com/c-barron/ml-agents/tree/master/docs). For a walkthrough on how to train an agent in one of the provided example environments, start [here](https://github.com/c-barron/ml-agents/blob/master/docs/Getting-Started-with-Balance-Ball.md).


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

## Битва за что-либо


Как вы видите в [видеоролике](https://www.youtube.com/watch?v=9Kk-oHBFylg) этого эксперимента, два вида агентов преследуют разные цели: работник толкает цель до противоположных ворот, а агрессор опрокидывает оппонентов (только рабочих). Идея для этого эксперимента была вдохновлена [SoccerTwos](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Learning-Environment-Examples.md#soccer-twos). Рабочий и агрессор имеют принципиально разные наблюдения и действия. И обучалать их пришлось отдельно друг от друга и в разных условиях. В начале, обучение работника, как будто бы не давало никакого результата. И я решенил максимально упростить сцену, а затем заняться настройкой гиперпараметров обучения. Сначала я убрал агрессоров, потом всех остальных рабочих, а в конце почти убрал трение гайки об поверхность стола, чтобы один рабочий мог свободно довести её до цели. Оказалось, эффективнее оставить двух рабочих играть друг против друга на всю ночь ;) Однако они до сих пор иногда немного тупят и просто стоят у верхней границы поля.

Перед тем, как завершить проект я писал в [unity connect](https://connect.unity.com/p/battle-for-something), что агрессор почти сразу чему-то научился - это было заблужение. С обученными опонентами боротся у него никак не получалось. Даже не смотря на то, что он похож на агента из SoccerTwos, научиться опрокидывать рабочих была для него очень сложная задача. Хотя переодически ему приходило в голову разгонятся и врезаться в толпу рабочих, но это сложно было назвать достойным результатом. Не помогло также имитационное обучение. К тому же я устал от этого проекта и стал искать более простой способ решения проблемы агрессора. Поэтому я поставил ему на концы триггеры, которые при контакте с рабочими заставляли их подпрыгивать и переварачиваться. И буквально через день, также упростив немного сцену (уменьшил скорость работникам), агрессор научился целенаправленно искать и атаковать рабочих. А битва за что-то стала действительно похоа на битву! Судя по графику tensorboard, max_steps можно было сильно уменьшить, однако после удачного результата я не хотел больше эксперементировать.

Unity Machine Learning Agents позволяет исследователям и разработчикам создавать игры и моделирование с использованием редактора Unity, которые служат средами, где интеллектуальные агенты могут обучаться с использованием подкрепленного обучения, нейроэволюции или других методов машинного обучения с помощью простого в использовании API Python. Для получения дополнительной информации см. [документацию](https://github.com/c-barron/ml-agents/tree/master/docs).

В этом репозитории папка 'pythonBFS' нужна для обучения рабочего агента, а 'pythonAg' для агента агрессора. Для ознакомления с обучением агентов в одной из примеров сред и подробными инструкциями, зайдите [сюда](https://github.com/c-barron/ml-agents/blob/master/docs/Getting-Started-with-Balance-Ball.md).



* Цель:

 * Агрессор: перевернуть, тем самым отключить работника.

 * Работник: дотолкать гайку до ворот противника.

* Агенты: На поле находятся 18 агентов по ровну в двух командах, двое из них имеют один brain (aggressor) и остальные имеют другой (worker).

* Поощрения агентов:

  * Агрессор: 
 
    -0.0003 кажный кадр.
 
    +1 когда опрокидан работник из враждебной команды.
 
    -1 когда опрокидан работник из своей команды.
 
  * Рабочий:
 
    -0.0003 кажный кадр.
 
    -0.1 когда работник двигается от гайки.
 
    +0.1 когда работник двигается к гайке.
 
    -0.4 когда гайка двигается по направлению к своим воротам.
 
    +0.4 когда гайка двигается по направлению к вражеским воротам.
 
    +0.7 когда работник соприкасается с гайкой.
 
    -1 когда гайка вошла в свои ворота.
 
    +1 когда гайка вошла во вражеские ворота.
 
    -1 когда act[0] > 1 или act[0] < -1

* Brains: оба мозга имеют observation/action:

* Vector Observation space (Continuous):

  * Агрессор: 89 содержащиеся в 12 лучах, расходящиеся в разных направлениях вдоль плоскости игрового стола.

  * Рабочий: 16.

* Vector Action space:

  * Агрессор (Discrete): 4.

  * Рабочий (Continuous): 1.

* Visual Observations: Отсутствует.

**TensorBoard Рабочего**

![GitHub Logo](https://github.com/dimakuzov/Battle-for-something/blob/master/TensorBoardWorker.png)

**TensorBoard Агрессора**

![GitHub Logo](https://github.com/dimakuzov/Battle-for-something/blob/master/TensorBoardAggressor.png)
