import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared-module';
import { LoginComponent } from './login/login.component';
import { SitemapComponent } from './sitemap/sitemap.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Interceptor } from './shared/api/interceptor';
import { HeaderComponent } from './sitemap/header/header.component';
import { LayoutModule } from '@angular/cdk/layout';
import { SideNavComponent } from './sitemap/side-nav/side-nav.component';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SitemapComponent,
    HeaderComponent,
    SideNavComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    LayoutModule,
    SharedModule,
  ],
  providers: [Title, { provide: HTTP_INTERCEPTORS, useClass: Interceptor, multi: true }],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
