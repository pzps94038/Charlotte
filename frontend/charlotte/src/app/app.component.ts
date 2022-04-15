import { Component, OnInit, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router, RouterEvent, RouterOutlet } from '@angular/router';
import { filter, map, mergeMap, Subject, takeUntil } from 'rxjs';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy{
  destroy$ = new Subject()
  loading : boolean = false
  constructor(
    private titleService: Title,
    private router: Router,
    private activeRoute: ActivatedRoute,
    )
  {
      this.setTitle()
      this.setProgressBar()
  }
  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.destroy$.next(null)
    this.destroy$.complete()
  }

  setTitle(){
    this.router.events.pipe(
      takeUntil(this.destroy$),
      filter(event=> event instanceof NavigationEnd),
      map(()=>this.activeRoute),
      map((route)=>{
        while(route.firstChild) { route = route.firstChild }
        return route;
      }),
      mergeMap(route => route.data)
    ).subscribe(data=>{
      this.titleService.setTitle(data['title'])
    })
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
