import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { PostComponent } from './post/post.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
	{ path: 'home/:id', component: HomeComponent },
	{ path: 'register', component: RegisterComponent },
	{ path: 'newpost/:id', component: PostComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
