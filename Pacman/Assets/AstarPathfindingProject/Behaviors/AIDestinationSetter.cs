using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		
		[Header("Targets")]
		public Transform target;
		[SerializeField] Transform newTarget;
		[SerializeField] Transform tempTarget;
		[SerializeField] Transform frightenedTarget;
		[SerializeField] Transform baseTarget;
		[SerializeField] Transform leftPortalEntry;
		[SerializeField] Transform rightPortalEntry;

		[Header("Time Controller")]
		public float changeTargetTime = 0f;
		public float frightenedEndTime;

		[SerializeField] Transform[] waypoints;
		int waypointIndex = 0;

		Animator myAnimator;
		CircleCollider2D myBodyCollider;

		IAstarAI ai;

		public void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		public void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

        public void Start()
        {
			frightenedTarget = waypoints[Random.Range(waypointIndex, waypoints.Length)];
			myBodyCollider = GetComponent<CircleCollider2D>();
			myAnimator = GetComponent<Animator>();
			tempTarget = target;
			
		}

        /// <summary>Updates the AI's destination every frame</summary>
        void Update ()  
		{
			if (FindObjectOfType<Enemy>().isScattering == false && FindObjectOfType<Enemy>().isChasing == true && FindObjectOfType<Pacman>().enemyFrightened == false && FindObjectOfType<Enemy>().isEaten == false)
			{
				frightenedEndTime = 0f;
				myAnimator.SetBool("isFrightened", false);
				changeTargetTime += Time.deltaTime;
				if (changeTargetTime > 0)
				{
					ai.destination = target.position;                       //chasing Pacman
					if (changeTargetTime >= 20f)
					{
						target = newTarget;
						ai.destination = target.position;                   //Getting Back to initial position for next scatter
						if (ai.reachedDestination)
						{
							changeTargetTime = 0f;
							FindObjectOfType<Enemy>().scatterTime = 0f;
							FindObjectOfType<Enemy>().chaseTime = 0f;
							FindObjectOfType<Enemy>().isChasing = false;
							target = tempTarget;                            //Target reset to Pacman for next chase
						}
					}
				}
			}
			else if (FindObjectOfType<Pacman>().enemyFrightened == true)	//when enemy is in frightened mode
			{
				myAnimator.SetBool("isFrightened", true);
				frightenedEndTime += Time.deltaTime;
				myAnimator.SetFloat("frightenedEndTime", frightenedEndTime);
				ai.destination = frightenedTarget.position;
				if (ai.reachedDestination)
				{
					Frightened();
				}
			}

			else if (FindObjectOfType<Enemy>().isEaten == true && FindObjectOfType<Pacman>().enemyFrightened == false)
            {
				target = baseTarget;
				ai.destination = target.transform.position;
				if (ai.reachedDestination)
                {
					gameObject.layer = 11;                                  //Layer is changed to Enemy
					myAnimator.SetBool("isEaten", false);
					target = tempTarget;
					FindObjectOfType<Enemy>().isEaten = false;
					FindObjectOfType<Enemy>().isScattering = true;
					FindObjectOfType<Enemy>().isChasing = false;
				}
			}
		}
			
		public void Frightened()
		{
			frightenedTarget = waypoints[Random.Range(waypointIndex, waypoints.Length)];
			ai.destination = frightenedTarget.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Left Portal"))
            {
				target = leftPortalEntry;
				ai.destination = target.transform.position;
            }
			else if (other.CompareTag("Right Portal"))
            {
				target = rightPortalEntry;
				ai.destination = target.transform.position;
			}
        }
    }
}
