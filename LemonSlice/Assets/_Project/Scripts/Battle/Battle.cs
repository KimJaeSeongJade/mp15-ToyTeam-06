using UnityEngine;

	public struct Damage
	{
		public int amount;

		public Damage(int damage)
		{
			amount = damage;
		}
	}

	public struct Owner
	{
		public GameObject AttackerObject;

		public Owner(GameObject attacker)
		{
			AttackerObject = attacker;
		}
	}


	/*public struct Target
	{
		public IDamageable damageableTarget;

		public Target(IDamageable target)
		{
			this.damageableTarget = target;
		}
	}*/
