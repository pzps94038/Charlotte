import { Component, OnInit, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import {
  ActivatedRoute,
  NavigationCancel,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
  RouterEvent,
  RouterOutlet,
} from '@angular/router';
import { filter, map, mergeMap, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  destroy$ = new Subject();

  constructor(
    private titleService: Title,
    private router: Router,
    private activeRoute: ActivatedRoute
  ) {
    this.setTitle();
  }
  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  setTitle() {
    this.router.events
      .pipe(
        filter((event) => event instanceof NavigationEnd),
        map(() => this.activeRoute),
        map((route) => {
          while (route.firstChild) {
            route = route.firstChild;
          }
          return route;
        }),
        mergeMap((route) => route.data),
        takeUntil(this.destroy$)
      )
      .subscribe((data) => {
        this.titleService.setTitle(data['title']);
      });
  }
}
