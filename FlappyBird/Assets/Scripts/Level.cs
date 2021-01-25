using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	private const float CAMERA_ORTHO_SIZE = 50f;
	private const float PIPE_WIDTH = 7.8f;
	private const float PIPE_HEAD_HEIGHT = 3.75f;
	private const float PIPE_MOVE_SPEED = 30f;
	private const float PIPE_DESTROY_X_POSITION = -100f;
	private const float PIPE_SPAWN_X_POSITION = 100f;
	private const float GROUND_DESTROY_X_POSITION = -200f;
	private const float CLOUD_DESTROY_X_POSITION = -160f;
	private const float CLOUD_SPAWN_X_POSITION = 130f;
	private const float CLOUD_SPAWN_Y_POSITION = 30f;
	private const float BIRD_X_POSITION = 0f;

	private static Level instance;

	public static Level GetInstance()
	{
		return instance;
	}

	private List<Transform> groundList;
	private List<Transform> cloudList;
	private float cloudSpawnTimer;
	private float cloudSpawnTimerMax;
	private List<Pipe> pipeList;
	private int pipesPassedCount;
	private int pipesSpawned;
	private float pipeSpawnTimer;
	private float pipeSpawnTimerMax;
	private float gapSize;
	private State state;

	public enum Difficulty
	{
		Easy,
		Medium,
		Hard,
		VeryHard,
		Impossible,
	}

	private enum State
	{
		WaitingToStart,
		Playing,
		BirdDead,
	}
	private void Awake()
	{
		instance = this;
		SpawnInitialClouds();
		SpawnInitialGround();
		pipeList = new List<Pipe>();
		pipeSpawnTimerMax = 1f;
		SetDifficulty(Difficulty.Easy);
		state = State.WaitingToStart;
	}
	private void Start()
	{
		Bird.Getinstance().OnDied += Bird_OnDied;
		Bird.Getinstance().OnStartedPlaying += Bird_OnStartedPlaying;
	}

	private void Bird_OnStartedPlaying(object sender, System.EventArgs e)
	{
		state = State.Playing;
	}

	private void Bird_OnDied(object sender, System.EventArgs e)
	{
		state = State.BirdDead;
	}

	private void Update()
	{
		if (state == State.Playing)
		{
			HandlePipeMovement();
			HandlePipeSpawning();
			HandleGround();
			HandleClouds();
		}
	}

	private void SpawnInitialClouds()
    {
		cloudList = new List<Transform>();
		Transform cloudTransfrom;
		cloudTransfrom = Instantiate(GetCloudPrefabTransform(), new Vector3(0, CLOUD_SPAWN_Y_POSITION, 0), Quaternion.identity);
		cloudList.Add(cloudTransfrom);
	}

	private Transform GetCloudPrefabTransform()
    {
        switch (Random.Range(0, 3))
        {
			default:
			case 0:
				return GameAssets.GetInstance().pfCloud_1;
			case 1:
				return GameAssets.GetInstance().pfCloud_2;
			case 2:
				return GameAssets.GetInstance().pfCloud_3;
		}
    }

	private void HandleClouds()
	{
		//Handle Cloud Spawning
		cloudSpawnTimerMax = 6f;
		cloudSpawnTimer -= Time.deltaTime;
		if (cloudSpawnTimer < 0)
		{
			//time to spawn another cloud
			cloudSpawnTimer = cloudSpawnTimerMax;
			Transform cloudTransfrom = Instantiate(GetCloudPrefabTransform(), new Vector3(CLOUD_SPAWN_X_POSITION, CLOUD_SPAWN_Y_POSITION, 0), Quaternion.identity);
			cloudList.Add(cloudTransfrom);
		}

		//Handle cloud moving
		for (int i = 0; i < cloudList.Count; i++)
		{
			Transform cloudTransform = cloudList[i];
			{
				//Move clouds by less speed than pipes for parallax
				cloudTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime * .7f;

				if (cloudTransform.position.x < CLOUD_DESTROY_X_POSITION)
				{
					//Cloud past destroy point, destroy self
					Destroy(cloudTransform.gameObject);
					cloudList.RemoveAt(i);
					i--;
				}
			}
		}
	}

	private void SpawnInitialGround()
	{
		groundList = new List<Transform>();
		Transform groundTransfrom;
		float groundY = -47.5f;
		float groundWidth = 192f;
		groundTransfrom = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(0, groundY, 0), Quaternion.identity);
		groundList.Add(groundTransfrom);
		groundTransfrom = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(groundWidth, groundY, 0), Quaternion.identity);
		groundList.Add(groundTransfrom);
		groundTransfrom = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(groundWidth * 2f, groundY, 0), Quaternion.identity);
		groundList.Add(groundTransfrom);
	}
	private void HandleGround()
	{
		foreach (Transform groundTransform in groundList)
		{
			groundTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;

			if (groundTransform.position.x < GROUND_DESTROY_X_POSITION)
			{
				//Ground passed the left side, relocate on the right side
				//find rightmost X position
				float rightMostXPosition = -100f;
				for (int i = 0; i < groundList.Count; i++)
				{
					if (groundList[i].position.x > rightMostXPosition)
					{
						rightMostXPosition = groundList[i].position.x;
					}
				}
				//Place ground on the right most position
				float groundWidth = 192f;
				groundTransform.position = new Vector3(rightMostXPosition + groundWidth, groundTransform.position.y, groundTransform.position.z);
			}
		}
	}

	private void HandlePipeSpawning()
	{
		pipeSpawnTimer -= Time.deltaTime;
		if (pipeSpawnTimer < 0)
		{
			//Time to Spawn Another pipe
			pipeSpawnTimer += pipeSpawnTimerMax;

			float heightEdgeLimit = 10f;
			float minHeight = gapSize * .5f;
			float totalHeight = CAMERA_ORTHO_SIZE * 2f;
			float maxHeight = totalHeight - gapSize * .5f - heightEdgeLimit;

			float height = UnityEngine.Random.Range(minHeight, maxHeight);
			CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POSITION);
		}

	}
	private void HandlePipeMovement()
	{
		for (int i = 0; i < pipeList.Count; i++)
		{
			Pipe pipe = pipeList[i];
			bool isToTheRightOfBird = pipe.GetXPosition() > BIRD_X_POSITION;
			pipe.Move();
			if (isToTheRightOfBird && pipe.GetXPosition() <= BIRD_X_POSITION && pipe.IsBottom())
			{
				//Pipe passed bird
				pipesPassedCount++;
				SoundManager.PlaySound(SoundManager.Sound.Score);
			}
			if (pipe.GetXPosition() < PIPE_DESTROY_X_POSITION)
			{
				//Destroy pipe
				pipe.DestorySelf();
				pipeList.Remove(pipe);
				i--;
			}
		}
	}

	private void SetDifficulty(Difficulty difficulty)
	{
		switch (difficulty)
		{
			case Difficulty.Easy:
				gapSize = 40f;
				pipeSpawnTimerMax = 1.6f;
				break;

			case Difficulty.Medium:
				gapSize = 35f;
				pipeSpawnTimerMax = 1.4f;
				break;

			case Difficulty.Hard:
				gapSize = 30f;
				pipeSpawnTimerMax = 1.2f;
				break;

			case Difficulty.VeryHard:
				gapSize = 25f;
				pipeSpawnTimerMax = 1.1f;
				break;


			case Difficulty.Impossible:
				gapSize = 20f;
				pipeSpawnTimerMax = 1f;
				break;
		}
	}

	private Difficulty GetDifficulty()
	{
		if (pipesSpawned >= 40) return Difficulty.Impossible;
		if (pipesSpawned >= 30) return Difficulty.VeryHard;
		if (pipesSpawned >= 20) return Difficulty.Hard;
		if (pipesSpawned >= 10) return Difficulty.Medium;
		return Difficulty.Easy;
	}

	private void CreateGapPipes(float gapY, float gapSize, float xPosition)
	{
		CreatePipe(gapY - gapSize * .5f, xPosition, true);
		CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize * .5f, xPosition, false);
		pipesSpawned++;
		SetDifficulty(GetDifficulty());
	}

	private void CreatePipe(float height, float xPosition, bool createBottom)
	{
		//Set up Pipe head
		Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
		float pipeHeadYPosition;
		if (createBottom)
		{
			pipeHeadYPosition = -CAMERA_ORTHO_SIZE + height - PIPE_HEAD_HEIGHT * .5f;
		}
		else
		{
			pipeHeadYPosition = +CAMERA_ORTHO_SIZE - height + PIPE_HEAD_HEIGHT * .5f;
		}
		pipeHead.position = new Vector3(xPosition, pipeHeadYPosition);

		//Set up Pipe body
		Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
		float pipeBodyYPoisition;
		if (createBottom)
		{
			pipeBodyYPoisition = -CAMERA_ORTHO_SIZE;
		}
		else
		{
			pipeBodyYPoisition = +CAMERA_ORTHO_SIZE;
			pipeBody.localScale = new Vector3(1, -1, 1);
		}
		pipeBody.position = new Vector3(xPosition, pipeBodyYPoisition);

		SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
		pipeBodySpriteRenderer.size = new Vector2(PIPE_WIDTH, height);

		BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
		pipeBodyBoxCollider.size = new Vector2(PIPE_WIDTH, height);
		pipeBodyBoxCollider.offset = new Vector2(0f, height * .5f);

		Pipe pipe = new Pipe(pipeHead, pipeBody, createBottom);
		pipeList.Add(pipe);
	}

	public int GetPipesSpawned()
	{
		return pipesSpawned;
	}

	public int GetPipesPassedCount()
	{
		return pipesPassedCount;
	}
	//Represents a single pipe
	private class Pipe
	{
		private Transform pipeHeadTransform;
		private Transform pipeBodyTransform;
		private bool isBottom;

		public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform, bool createBottom)
		{
			this.pipeHeadTransform = pipeHeadTransform;
			this.pipeBodyTransform = pipeBodyTransform;
			this.isBottom = createBottom;
		}

		public void Move()
		{
			pipeHeadTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
			pipeBodyTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
		}
		public float GetXPosition()
		{
			return pipeHeadTransform.position.x;
		}

		public bool IsBottom()
		{
			return isBottom;
		}
		public void DestorySelf()
		{
			Destroy(pipeHeadTransform.gameObject);
			Destroy(pipeBodyTransform.gameObject);
		}
	}
}
