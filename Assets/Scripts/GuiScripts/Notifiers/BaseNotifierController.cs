using UnityEngine;
using System.Collections;

public abstract class BaseNotifierController : Abstract {
	ArrayList notifiersQueue = new ArrayList();
	protected ArrayList notifiersInProgress = new ArrayList();
	
	protected void AddNotifier(BaseNotifier notifier){
		notifier.singleTransform.parent = singleTransform;
		notifier.singleTransform.localPosition = GetOutNotifierPlace(0);
		notifiersQueue.Add (notifier);
		notifier.SetNotifierController(this);
	}
	
	void Update(){
		if(notifiersInProgress.Count<GetMaxNotifiersToShow()){
			if(notifiersQueue.Count>0)
			{
				BaseNotifier notifier = (BaseNotifier)notifiersQueue[0];
				int position = notifiersInProgress.Count;
				notifier.singleTransform.localPosition = GetOutNotifierPlace(position);
				notifiersInProgress.Add(notifier);
				notifiersQueue.Remove(notifier);
				notifier.FlyIn(GetInNotifierPlace(position));
			}
		}
	}
	protected abstract int GetMaxNotifiersToShow();
	
	protected abstract Vector3 GetOutNotifierPlace(int position);
	
	protected abstract Vector3 GetInNotifierPlace(int position);
	
	public virtual void NotifierWantsToFlyOut(BaseNotifier notifier){
		int position = notifiersInProgress.IndexOf(notifier);
		notifiersInProgress.Remove(notifier);
		notifier.FlyOut(GetOutNotifierPlace(position));
		ChangePlaces();
	}
	
	public virtual void NotifierFlyOutEnd(BaseNotifier notifier){
		Destroy(notifier.gameObject);
	}
	
	public virtual void ChangePlaces(){
		for(int i=0;i<notifiersInProgress.Count;i++){
			BaseNotifier inProgressNotifier = (BaseNotifier)notifiersInProgress[i];
			inProgressNotifier.FlyPlace(GetInNotifierPlace(i));
		}	
	}
}