import { Subject, takeUntil, debounceTime, filter, map } from 'rxjs';
import { SignalRService } from './../../../shared/service/signalR/signal-r.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Chart } from 'angular-highcharts';
import { DashbordService } from 'src/app/shared/api/dashbord/dashbord.service';
import { ThisReceiver } from '@angular/compiler';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, OnDestroy {
  private destroy$: Subject<any> = new Subject();
  weekSaleChart = new Chart();
  registeredMemberChart = new Chart();
  monthSaleChart = new Chart();
  constructor(
    private signalRService: SignalRService,
    private dashbordService: DashbordService
  ) {
    this.signalRService.createConnection();
    this.getWeekSaleChart();
    this.getMonthSaleChart();
    this.getRegisteredMemberChart();
  }

  ngOnDestroy(): void {
    this.signalRService.closeConnection();
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  ngOnInit(): void {
    this.listenChannel();
  }

  getWeekSaleChart() {
    this.dashbordService
      .getWeekSale()
      .pipe(
        filter((res) => res.code === 200),
        map((res) => res.data),
        takeUntil(this.destroy$)
      )
      .subscribe((res) => (this.weekSaleChart = new Chart(res)));
  }

  getRegisteredMemberChart() {
    this.dashbordService
      .getRegisteredMember()
      .pipe(
        filter((res) => res.code === 200),
        map((res) => res.data),
        takeUntil(this.destroy$)
      )
      .subscribe((res) => (this.registeredMemberChart = new Chart(res)));
  }

  getMonthSaleChart() {
    this.dashbordService
      .getMonthSale()
      .pipe(
        filter((res) => res.code === 200),
        map((res) => res.data),
        takeUntil(this.destroy$)
      )
      .subscribe((res) => (this.monthSaleChart = new Chart(res)));
  }

  listenChannel() {
    this.signalRService
      .listenChannel('WeekSaleRefresh')
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => this.getWeekSaleChart());
    this.signalRService
      .listenChannel('MonthSaleRefresh')
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => this.getMonthSaleChart());
    this.signalRService
      .listenChannel('RegisteredMemberRefresh')
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => this.getRegisteredMemberChart());
  }
}
