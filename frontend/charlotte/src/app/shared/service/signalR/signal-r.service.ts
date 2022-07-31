import { ApiUrl } from './../../api/api.url';
import { Injectable, OnDestroy } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class SignalRService implements OnDestroy {
  private connection: signalR.HubConnection | undefined;
  private destroy$: Subject<any> = new Subject<any>();
  constructor() {}

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  createConnection() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(ApiUrl.dashbordHub)
      .build();
    this.connection
      .start()
      .then(() => console.log('Connection started!'))
      .catch((err) => console.log('Error while establishing connection :('));
  }

  closeConnection() {
    this.connection?.stop().then(() => console.log('Connection stop!'));
  }

  listenChannel<T = null>(channelName: string) {
    return new Observable<T>((observer) => {
      this.connection?.on(channelName, (callback: T) => {
        observer.next(callback);
      });
    });
  }
}
