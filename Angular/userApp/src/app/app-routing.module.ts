import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PhotoComponent } from './photo/photo.component';
import { SpeakersComponent } from './speakers/speakers.component';
import { ProblemComponent } from './problem/problem.component';

const routes: Routes = [
  {path: 'photo', component: PhotoComponent},
  {path: 'speakers', component: SpeakersComponent},
  {path: 'problem', component: ProblemComponent},
  {path: '**', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
