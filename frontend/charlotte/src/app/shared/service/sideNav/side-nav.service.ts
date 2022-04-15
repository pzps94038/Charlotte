import { BehaviorSubject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SideNavService {

  private show$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true)

  hide(){
    this.show$.next(false)
  }

  show()
  {
    this.show$.next(true)
  }

  toggle(){
    this.show$.next(!this.show$.value)
  }

  getSideState(): Observable<boolean>{
    return this.show$.asObservable();
  }
}
