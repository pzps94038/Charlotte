import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { filter, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-progress-bar',
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.scss']
})
export class ProgressBarComponent implements OnInit, OnDestroy {
  destroy$ = new Subject()
  loading : boolean = false
  constructor(
    private router: Router,
  ) { }

  ngOnDestroy(): void {
    this.destroy$.next(null)
    this.destroy$.complete()
  }

  ngOnInit(): void {
    this.setProgressBar()
  }
  setProgressBar(){
    this.router.events.pipe(
      takeUntil(this.destroy$),
      filter(event=>
      event instanceof NavigationStart ||
      event instanceof NavigationEnd ||
      event instanceof NavigationCancel ||
      event instanceof NavigationError
    )).subscribe(event=>{
      if(event instanceof NavigationStart)this.loading = true
      else this.loading = false
    })
  }
}
