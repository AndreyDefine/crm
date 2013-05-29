using UnityEngine;
using System.Collections;

public abstract class BaseNotifierController : Abstract {
	ArrayList notifiersQueue = new ArrayList();
	protected ArrayList notifiersInProgress = new ArrayList();
	
	public virtual void Restart(){
		for(int i=0;i<notifiersQueue.Count;i++){
			((BaseNotifier)notifiersQueue[i]).DestroyNotifier();	
		}
		notifiersQueue.Clear();
		for(int i=0;i<notifiersInProgress.Count;i++){
			((BaseNotifier)notifiersInProgress[i]).DestroyNotifier();
		}
		notifiersInProgress.Clear();
	}
	
	protected void AddNotifier(BaseNotifier notifier){
		notifier.singleTransform.parent = singleTransform;
		notifier.singleTransform.localPosition = GetOutNotifierPlace(0);
		notifiersQueue.Add (notifier);
		notifier.SetNotifierController(this);
	}
	
	protected virtual void Update(){
		if(notifiersInProgress.Count<GetMaxNotifiersToShow()){
			if(notifiersQueue.Count>0)
			{
				
				BaseNotifier notifier = (BaseNotifier)notifiersQueue[0];
				int i=0;
				for(;i<notifiersInProgress.Count;i++){
					if(((BaseNotifier)notifiersInProgress[i]).priority>notifier.priority){
						break;
					}
				}
				int position = i;
				notifier.singleTransform.localPosition = GetOutNotifierPlace(position);
				notifiersInProgress.Insert(position,notifier);
				notifiersQueue.Remove(notifier);
				notifier.FlyIn(GetInNotifierPlace(position));
				ChangePlaces(position+1);
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
	
	public virtual void ChangePlaces(int startPlace = 0){
		for(int i=startPlace;i<notifiersInProgress.Count;i++){
			BaseNotifier inProgressNotifier = (BaseNotifier)notifiersInProgress[i];
			inProgressNotifier.FlyPlace(GetInNotifierPlace(i));
		}	
	}
}
