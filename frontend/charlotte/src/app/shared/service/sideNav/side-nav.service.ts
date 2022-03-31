import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SideNavService {

  show$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true)

  hide(){
    this.show$.next(false)
  }

  show()
  {
    this.show$.next(true)
  }
  
  toggle(){
    const nextState = this.show$.value
    this.show$.next(!nextState)
  }
}
