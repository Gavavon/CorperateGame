using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
	public class AiBehaviourTreeHandler : MonoBehaviour
	{
		[SerializeField] private BehaviourTree defaultTree;
		[SerializeField] private BehaviourTree attackerTree;
		[SerializeField] private BehaviourTree defenderTree;
		[SerializeField] private BehaviourTree chargerTree;
		// The main behaviour tree asset
		private BehaviourTree tree;

		// Storage container object to hold game object subsystems
		Context context;

		// Start is called before the first frame update
		void Start()
		{
			try 
			{
				switch (GetComponent<AiAgent>().config.enemyBehaviors)
				{
					case AiAgentConfig.AiBehaviors.ProvokableWorkers:
						tree = attackerTree;
						break;
					case AiAgentConfig.AiBehaviors.Attackers:
						tree = attackerTree;
						break;
					case AiAgentConfig.AiBehaviors.Defenders:
						tree = defenderTree;
						break;
					case AiAgentConfig.AiBehaviors.Chargers:
						tree = chargerTree;
						break;
					case AiAgentConfig.AiBehaviors.Unique:
						tree = defaultTree;
						break;
				}
			}
			catch 
			{
				tree = defaultTree;
			}
			


			context = CreateBehaviourTreeContext();
			tree = tree.Clone();
			tree.Bind(context);
			tree.blackboard.attachedObject = this.gameObject;
		}

		// Update is called once per frame
		void Update()
		{
			if (tree)
			{
				tree.Update();
			}
		}

		Context CreateBehaviourTreeContext()
		{
			return Context.CreateFromGameObject(gameObject);
		}

		private void OnDrawGizmosSelected()
		{
			if (!tree)
			{
				return;
			}

			BehaviourTree.Traverse(tree.rootNode, (n) => {
				if (n.drawGizmos)
				{
					n.OnDrawGizmos();
				}
			});
		}
	}
}
