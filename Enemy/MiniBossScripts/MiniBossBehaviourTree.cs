using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
namespace TheKiwiCoder
{
	public class MiniBossBehaviourTree : MonoBehaviour
	{
		[SerializeField] private BehaviourTree phase1Tree;

		// The main behaviour tree asset
		private BehaviourTree tree;

		// Storage container object to hold game object subsystems
		Context context;

		// Start is called before the first frame update
		void Start()
		{
			tree = phase1Tree;

			context = CreateBehaviourTreeContext();
			tree = tree.Clone();
			tree.Bind(context);
			tree.blackboard.attachedObject = this.gameObject;
		}

		public void ReplaceTree(BehaviourTree tree) 
		{
			this.tree = tree;
			this.tree.blackboard.attachedObject = this.gameObject;
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

			BehaviourTree.Traverse(tree.rootNode, (n) =>
			{
				if (n.drawGizmos)
				{
					n.OnDrawGizmos();
				}
			});
		}
	}
}
