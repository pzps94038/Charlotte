import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { TokenAuthGuard } from './auth/token-auth.guard';
import { LoginComponent } from './login/login.component';
import { SitemapComponent } from './sitemap/sitemap.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    data:{ title: '登入' }
  },
  {
    path: 'siteMap',
    component: SitemapComponent,
    loadChildren: () => import('./sitemap/sitemap.module').then(m => m.SitemapModule),
    canActivate: [TokenAuthGuard],
    canActivateChild: [TokenAuthGuard],
    data:{ title: '首頁' }
  },
  {
    path: '**', redirectTo:'/login', pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
