using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorTemplateTemplate
{
	int SendDamage();
	void TakeDamage(int incomingDamage);
	void Die();

	// The last method in our interface is ActorStats, which takes a type of SOActorModel. SOActorModel is a scriptable object that we are going to explain and create in the next section.
	  void ActorStats(SOActorModel actorModel);
}

  

